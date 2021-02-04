using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Url
    {
        public int Id { get; set; }

        /// <summary>
        ///  Class ile aynı olamaz diye iUrl olarak tanımlandı
        /// </summary>
        public string iUrl { get; set; }
        public int Count { get; set; }
    }
}
