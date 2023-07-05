using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.VievModel
{
    public class GrupModel
    {
        public int grupmesajId { get; set; }
        public string grupAdi { get; set; }
        public System.DateTime olusturmaZamani { get; set; }
        public string icerik { get; set; }
        public int grupSohbetId { get; set; }
        public int groupUserId { get; set; }

        public UserModel userBilgi { get; set; }
        public SohbetModel sohbetBilgi { get; set; }
    }
}