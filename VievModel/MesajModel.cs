using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.VievModel
{
    public class MesajModel
    {
        public int mesajId { get; set; }
        public int mesajSohbetId { get; set; }
        public int mesajUserId { get; set; }
        public string icerik { get; set; }
        public System.DateTime gonderimTarihi { get; set; }

        public UserModel userBilgi { get; set; }
        public SohbetModel sohbetBilgi { get; set; }
    }
}