using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IUserService
    {
        User GetById(int id);

        User GetUser(string username, string password);

    }
}
