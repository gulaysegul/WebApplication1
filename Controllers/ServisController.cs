using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.VievModel;

namespace WebApplication1.Controllers
{
    public class ServisController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        //Kullanıcıları Listeleme

        [HttpGet]
        [Route("api/userliste")]

        public List<UserModel> UsersListe()
        { 

           List<UserModel> liste = db.Users.Select(x => new UserModel()
           {
            UserId = x.UserId,
            KullaniciAdi = x.KullaniciAdi,
            Sifre = x.Sifre,
            AdiSoyadi = x.AdiSoyadi,
            Email = x.Email,
           
           }).ToList();
            return liste;
        }

        //Bir Kullanıcıyı Getirme
        
        [HttpGet]
        [Route("api/userbyid/{UserId}")]

        public UserModel UserById(int UserId)
        {
            UserModel kayit = db.Users.Where(s => s.UserId == UserId).Select(x => new UserModel()
            {
                UserId = x.UserId,
                KullaniciAdi = x.KullaniciAdi,
                Sifre = x.Sifre,
                AdiSoyadi = x.AdiSoyadi,
                Email = x.Email,
            
            }).FirstOrDefault();
            return kayit;
        }

        //Yeni Bir Kullanıcı Oluşturma
        [HttpPost]
        [Route("api/userekle")]

        public SonucModel UserEkle(UserModel model)
        {
            if (db.Users.Count(s=>s.KullaniciAdi== model.KullaniciAdi)>0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Seçtiğiniz Kullanıcı Adı Kullanımdadır!";
                return sonuc;
            }

            Users yeni = new Users();
            yeni.UserId = model.UserId;
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.Sifre = model.Sifre;
            yeni.AdiSoyadi = model.AdiSoyadi;
            yeni.Email = model.Email;

            db.Users.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Başarıyla Kaydedildi";

            return sonuc;
        }

        //Kullanıcı Silme

        [HttpDelete]
        [Route("api/usersil/{UserId}")]
        public SonucModel KullaniciSil(int UserId)
        {
            Users user = db.Users.Where(s => s.UserId == UserId).SingleOrDefault();

            if(user==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }

            if (db.Sohbetler.Count(s => s.olusturanId == UserId) > 0)
            if (db.Mesajlar.Count(s => s.mesajUserId == UserId) > 0) 
            if (db.Katilimcilar.Count(s => s.katilimciUserId == UserId) > 0) 
            if (db.Gruplar.Count(s => s.groupUserId == UserId) > 0)

            {
              sonuc.islem = false;
              sonuc.mesaj = "Kullanıcı Silinemez!";
              return sonuc;
            }
            db.Users.Remove(user);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Silindi";
            return sonuc;


        }

        //Sohbetleri Idlerine Göre Listeleme

        [HttpGet]
        [Route("api/sohbetbyid/{sohbetId}")]

        public SohbetModel SohbetById(int sohbetId)
        {
            SohbetModel kayit = db.Sohbetler.Where(s => s.sohbetId == sohbetId).Select(x => new SohbetModel()
            {
                sohbetId = x.sohbetId,
                olusturanId = x.olusturanId,
                olusturmaTarihi = x.olusturmaTarihi,
                sohbetTipi = x.sohbetTipi

            }).FirstOrDefault();
            return kayit;
        }

        //Kullanıcıya Göre Sohbetleri Listeleme

        [HttpGet]
        [Route("api/usersohbetliste/{UserId}")]

        public List<SohbetModel> UserSohbetListe (int UserId)
        {

            List<SohbetModel> liste = db.Sohbetler.Where(s=> s.olusturanId == UserId).Select(x => new SohbetModel()
            {
                sohbetId = x.sohbetId,
                olusturanId = x.olusturanId,
                olusturmaTarihi = x.olusturmaTarihi,
                sohbetTipi = x.sohbetTipi,

            }).ToList();
            foreach (var user in liste)
            {
                user.userBilgi = UserById(user.olusturanId);
            }

            return liste;
        }
        //Mesajları Kullanıcıya Göre Listeleme 

        [HttpGet]
        [Route("api/usermesajliste/{UserId}")]

        public List<MesajModel> UserMesajListe(int UserId)
        {

            List<MesajModel> liste = db.Mesajlar.Where(s => s.mesajUserId == UserId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajSohbetId = x.mesajSohbetId,
                mesajUserId = x.mesajUserId,
                icerik = x.icerik,
                gonderimTarihi= x.gonderimTarihi,

            }).ToList();
            foreach (var user in liste)
            {
                user.userBilgi = UserById(user.mesajUserId);
            }

            return liste;
        }

        //Mesajları Sohbetlere Göre Listeleme 

        [HttpGet]
        [Route("api/sohbetmesajliste/{sohbetId}")]

        public List<MesajModel> SohbetMesajListe(int sohbetId)
        {

            List<MesajModel> liste = db.Mesajlar.Where(s => s.mesajSohbetId == sohbetId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajSohbetId = x.mesajSohbetId,
                mesajUserId = x.mesajUserId,
                icerik = x.icerik,
                gonderimTarihi = x.gonderimTarihi,

            }).ToList();
            foreach (var mesaj in liste)
            {
                mesaj.sohbetBilgi = SohbetById(mesaj.mesajSohbetId);
                mesaj.userBilgi = UserById(mesaj.mesajUserId);
            }

            return liste;
        }

        //Mesaj Gönderme

        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {

            Mesajlar yeni = new Mesajlar();
            yeni.mesajId = model.mesajId;
            yeni.mesajSohbetId = model.mesajSohbetId;
            yeni.mesajUserId = model.mesajUserId;
            yeni.icerik = model.icerik;
            yeni.gonderimTarihi = model.gonderimTarihi;

            db.Mesajlar.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Gönderildi";

            return sonuc;

        }

        //Mesajı User'a Göre Silme

        [HttpDelete]
        [Route("api/usermesajsil/{mesajUserId}")]

        public SonucModel MesajSil(int mesajUserId)
        {
            Mesajlar mesaj = db.Mesajlar.Where(s => s.mesajUserId == mesajUserId).SingleOrDefault();

             if (db.Mesajlar.Count(s => s.mesajUserId == mesajUserId) > 0)


             {
                db.Mesajlar.Remove(mesaj);
                db.SaveChanges();
                sonuc.islem = true;
                sonuc.mesaj = "Mesaj Silindi";
                return sonuc;
              
             }

            sonuc.islem = false;
            sonuc.mesaj = "Mesaj Silinemez!";
            return sonuc;
        }

        //Grup Tablosunu Listeleme

        [HttpGet]
        [Route("api/grupliste")]

        public List<GrupModel> GrupListe()
        {

            List<GrupModel> liste = db.Gruplar.Select(x => new GrupModel()
            {
                grupmesajId = x.grupmesajId,
                grupAdi = x.grupAdi,
                olusturmaZamani = x.olusturmaZamani,
                icerik= x.icerik,
                grupSohbetId = x.grupSohbetId,
                groupUserId= x.grupSohbetId

            }).ToList();
            return liste;
        }

        //Grup Adına Göre Listeleme
        [HttpGet]
        [Route("api/grupbygrupadi/{grupAdi}")]

        public GrupModel ByGrupAdi(string grupAdi)
        {
            GrupModel kayit = db.Gruplar.Where(s => s.grupAdi == grupAdi).Select(x => new GrupModel()
            {
                grupmesajId = x.grupmesajId,
                grupAdi = x.grupAdi,
                olusturmaZamani = x.olusturmaZamani,
                icerik = x.icerik,
                grupSohbetId = x.grupSohbetId,
                groupUserId = x.grupSohbetId

            }).FirstOrDefault();
            return kayit;
        }

        //Yeni Grup Oluşturma

        [HttpPost]
        [Route("api/grupkle")]
        public SonucModel GrupEkle(GrupModel model)
        {

            Gruplar yeni = new Gruplar();
            yeni.grupmesajId = model.grupmesajId;
            yeni.grupAdi = model.grupAdi;
            yeni.olusturmaZamani = model.olusturmaZamani;
            yeni.icerik = model.icerik;
            yeni.grupSohbetId = model.grupSohbetId;
            yeni.groupUserId = model.groupUserId;

            db.Gruplar.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yeni Grup Oluşturuldu";

            return sonuc;

        }

        //Grup Düzenleme

        [HttpPut]
        [Route("api/grupduzenle")]

        public SonucModel GrupDuzenle(GrupModel model)
        {
            Gruplar kayit = db.Gruplar.Where(s => s.grupmesajId == model.grupmesajId).FirstOrDefault();
             
            if(kayit== null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı!";
                return sonuc;
            }

            kayit.grupmesajId = model.grupmesajId;
            kayit.grupAdi = model.grupAdi;
            kayit.olusturmaZamani = model.olusturmaZamani;
            kayit.icerik = model.icerik;
            kayit.grupSohbetId = model.grupSohbetId;
            kayit.groupUserId = model.groupUserId;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Düzenlendi ";

            return sonuc;

        }

        //Grup Silme

        [HttpDelete]
        [Route("api/grupsil/{grupmesajId")]

        public SonucModel GrupSil(int grupmesajId)
        {
            Gruplar grup = db.Gruplar.Where(s => s.grupmesajId == grupmesajId).FirstOrDefault();

            if (grup == null);
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı!";
                return sonuc;
            }

            db.Gruplar.Remove(grup);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Silindi";

            return sonuc;

        }











    }
}
