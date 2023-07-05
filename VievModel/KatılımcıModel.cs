using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.VievModel
{
    public class KatılımcıModel
    {
        public int katilimciId { get; set; }
        public int katilimciSohbetId { get; set; }
        public int katilimciUserId { get; set; }

        public UserModel userBilgi { get; set; }
        public SohbetModel sohbetBilgi { get; set; }
    }
}