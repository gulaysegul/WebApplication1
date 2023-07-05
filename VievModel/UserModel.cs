using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.VievModel
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string AdiSoyadi { get; set; }
        public string Email { get; set; }

        public SohbetModel sohbetBilgi { get; set; }
    }
}