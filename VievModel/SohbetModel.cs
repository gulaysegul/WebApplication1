using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.VievModel
{
    public class SohbetModel
    {
        public int sohbetId { get; set; }
        public int olusturanId { get; set; }
        public System.DateTime olusturmaTarihi { get; set; }
        public string sohbetTipi { get; set; }

        public UserModel userBilgi { get; set; }


    }
}