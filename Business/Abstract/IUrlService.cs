using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IUrlService
    {


        List<Url> GetList();

        bool UpdateUrl(int Id, string Url);

        bool DeleteUrl(int Id);

        bool AddUrl(string Url);

        bool isExcept(string Url);

        Url GetById(int Id);

        bool UpdateCount(int? Id, int Count);

    }
}
