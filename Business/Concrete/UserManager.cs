using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {

        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        
        public User GetById(int id)
        {
            return _userDal.Get(x => x.Id == id);
        }

        public User GetUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new User { Username = null, Password = null };
            }
            else
            {
                return _userDal.Get(x => x.Username == username && x.Password == password);
            }
        }
    }
}
