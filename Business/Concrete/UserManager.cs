using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var claims = _userDal.GetClaims(user);
            return claims;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return  _userDal.Get(u => u.Email == email);
            
         
        }
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new Result(true, "user deleted");
        }

        public void Update(User user)
        {
            _userDal.UpDate(user);
        }

        public IDataResult<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
