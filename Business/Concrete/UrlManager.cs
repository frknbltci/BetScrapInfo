using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
   public class UrlManager : IUrlService
    {
        private IUrlDal _urlDal;
        public UrlManager(IUrlDal urlDal)
        {
            _urlDal = urlDal;
        }

        public bool AddUrl(string Url)
        {
            if (!String.IsNullOrEmpty(Url))
            {
                try
                {
                    Url newUrl = new Url()
                    {
                        iUrl = Url,
                        Count = 0
                    };

                    _urlDal.Add(newUrl);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUrl(int Id)
        {
            if (Id > 0)
            {
                try
                {
                    var url = _urlDal.Get(x => x.Id == Id);
                    Url delUrl = new Url()
                    {
                        Id = url.Id,
                        Count = url.Count,
                        iUrl = url.iUrl
                    };
                    _urlDal.Delete(delUrl);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<Url> GetList()
        {
            return _urlDal.GetList();
        }

        public bool isExcept(string Url)
        {
            var data= _urlDal.GetList(x => x.iUrl == Url);
            if (data.Count==0 || data==null)
            {
                return true;
            }
            else
            {
              return false;      
            }
        }

        public bool UpdateCount(int ?Id, int Count)
        {
            if (Id==0 || Id==null)
            {
                return false;
            }
            else
            {
                try
                {
                    var oldData = _urlDal.Get(x => x.Id == Id);
                    Url upUrl = new Url()
                    {
                        Id= (int)Id,
                        Count = Count,
                        iUrl = oldData.iUrl,
                    };
                    _urlDal.Update(upUrl);
                    return true;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                    return false;
                } 
            }
        }
        public bool UpdateUrl(int Id, string Url)
        {
            if (Id != 0 || !String.IsNullOrEmpty(Url))
            {

                try
                {
                    var oldData=_urlDal.Get(x => x.Id == Id);
                    Url newUrl = new Url()
                    {
                        Id = Id,
                        iUrl = Url,
                        Count=oldData.Count
                    };

                    _urlDal.Update(newUrl);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
