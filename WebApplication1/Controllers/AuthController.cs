using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                //_authService.RegisterEmailSendCode();
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("sendcode")]
        public ActionResult SendVerificationCode()
        {
            var result = _authService.RegisterEmailSendCode();


            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("controlcode")]
        public ActionResult ControlCode(object code)
        {
            // VerificationCodeNumber _code = JsonConvert.DeserializeObject<VerificationCodeNumber>(code);

            string res = code.ToString();
           

            string jsonData = res;
            
            using (JsonDocument document = JsonDocument.Parse(jsonData))
            {
                JsonElement root = document.RootElement;
                int codeValue = root.GetProperty("code").GetInt32();
                var result  = _authService.RegisterControlEmailCode(codeValue);
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }


            

            

        }
    }
}
