using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Core.Extensions;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _httpContextAccessor;
        private EfVerificationCodeDal _validationcodeDal;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper,IHttpContextAccessor httpContextAccessor
            , EfVerificationCodeDal validationcodeDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
            _validationcodeDal = validationcodeDal;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                EmailControl = false
                
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult RegisterEmailSendCode()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.ClaimEmail();
            var code = new Random().Next(10000);
            _validationcodeDal.Add(new Core.Entities.Concrete.VerificationCode()
            {
                UserEmail = userEmail,
                Code = code,
            });
            SentMail(userEmail,code).GetAwaiter().GetResult();

            return new SuccessResult();
        }

        public IResult RegisterControlEmailCode(int code)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.ClaimEmail();

            var DbCode = _validationcodeDal.Get(c => c.UserEmail == userEmail).Code;

            if (code == DbCode)
            {
               var userToUpdate =  _userService.GetByMail(userEmail);
               userToUpdate.EmailControl = true;
                _userService.Update(userToUpdate);
                return new SuccessResult();
            }

            return new ErrorResult();

        }



        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }


        private async Task SentMail(string email,int verificationCode)
        {

            string body = $"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n   " +
                $" <title>Account Verification - ${MailRules.WebsiteName}</title>\r\n</head>\r\n<body>\r\n    " +
                $"<h1>Hello {email} ,</h1>\r\n    <p>\r\n        " +
                $"We have sent you a unique verification code to confirm your account. " +
                $"Please use the verification code below to activate your account:\r\n    </p>\r\n    <p>\r\n        " +
                $"<strong>Verification Code:</strong> {verificationCode}\r\n    </p>\r\n    <p>\r\n        " +
                $"Enter this code into the required field to verify your account and complete the process. If you did not create this account, " +
                $"please contact our customer service before sharing the verification code with anyone.\r\n    </p>\r\n    <p>\r\n       " +
                $" <strong>The {MailRules.WebsiteName} Team wishes you enjoyable shopping.</strong>\r\n    </p>\r\n    <p>\r\n        Best regards,<br>\r\n        " +
                $"{MailRules.WebsiteName} Support Team\r\n    </p>\r\n</body>\r\n</html>\r\n" ;

            try
            {
                // SmtpClient ve MailMessage nesnelerini oluşturun
                using (MailMessage mailMessage = new MailMessage(MailRules.senderEmail, email, MailRules.subject, body))
                {

                    mailMessage.IsBodyHtml = true;
                    //mailMessage.Attachments.Add(new Attachment("C:\\file.zip"));


                    using (SmtpClient smtpClient = new SmtpClient(MailRules.smtpServer, MailRules.smtpPort))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(MailRules.senderEmail, MailRules.appPassword);

                        smtpClient.Send(mailMessage);
                        Console.WriteLine("E-posta başarıyla gönderildi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderirken bir hata oluştu: " + ex.Message);
            }

        }




        }
    }
