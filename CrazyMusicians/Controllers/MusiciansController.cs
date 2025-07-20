using System.Security.Cryptography.X509Certificates;
using CrazyMusicians.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    //Api contrtolller oldugunu tanimlar
    [ApiController]
    [Route("api/[controller]")]//controllerdeki tum end pointler /api/musicians ile baslar
    public class MusiciansController : ControllerBase
    {
        public static List<Musician> musicians = new List<Musician>()
        { // goresledeki gibi 10 adet muzikci tanimlanmistir
            new Musician { Id = 1, Name = "Ahmet Çalgı", Occupation = "Ünlü Çalgı Çalar", FunnyTrait = "Her zaman yanlış çalar, ama çok eğlenceli", IsDeleted = false },
            new Musician { Id = 2, Name = "Zeynep Melodi", Occupation = "Popüler Melodi Yazarı", FunnyTrait = "Şarkıları yanlış anlaşılır ama çok popüler", IsDeleted = false },
            new Musician { Id = 3, Name = "Cemil Akor", Occupation = "Çılgın Akorist", FunnyTrait = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli", IsDeleted = false },
            new Musician { Id = 4, Name = "Fatma Nota", Occupation = "Sürpriz Nota Üreticisi", FunnyTrait = "Nota üretirken sürekli sürprizler hazırlar", IsDeleted = false },
            new Musician { Id = 5, Name = "Hasan Ritim", Occupation = "Ritim Canavarı", FunnyTrait = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir", IsDeleted = false },
            new Musician { Id = 6, Name = "Elif Armoni", Occupation = "Armoni Ustası", FunnyTrait = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır", IsDeleted = false },
            new Musician { Id = 7, Name = "Ali Perde", Occupation = "Perde Uygulayıcı", FunnyTrait = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir", IsDeleted = false },
            new Musician { Id = 8, Name = "Ayşe Rezonans", Occupation = "Rezonans Uzmanı", FunnyTrait = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır", IsDeleted = false },
            new Musician { Id = 9, Name = "Murat Ton", Occupation = "Tonlama Meraklısı", FunnyTrait = "Tonlamalardaki farklılıkları bazen komik, ama oldukça ilginç", IsDeleted = false },
            new Musician { Id = 10, Name = "Selin Akor", Occupation = "Akor Sihirbazı", FunnyTrait = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır", IsDeleted = false }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Musician>> GetAllMusicians()
        {
            // Tüm muzisyenleri dondurur
            return Ok(musicians.Where(m => !m.IsDeleted).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Musician> GetMusicianById(int id)
        {
            // muszisyeni Idye gore bul ve silinmemis oldugundan emin ol 
            var musician = musicians.FirstOrDefault(m => m.Id == id && !m.IsDeleted);
            //Eger muzisyen bulunamazsa veya soft delete edilmisse 404 http hatası dondurulur
            if (musician == null)
            {
                return NotFound();
            }
            //Eger muzisyen bulunursa 200 http kodu ile dondurulur
            return Ok(musician);
        }

        //----------------------------------------------------

        [HttpPost] // listeye yeni bir musizyen ekler
        public IActionResult AddMusician([FromBody] Musician newMusician)
        {
            // Model validasyonunun başarılı olup olmadığını kontrol eder.
            // Eğer [Required] gibi attribute'lar ile belirtilen kurallara uyulmazsa, ModelState.IsValid false döner.
            if (!ModelState.IsValid)
            {
                // Validasyon hataları varsa, 400 Bad Request ile birlikte hata detaylarını döndürür.
                return BadRequest(ModelState);
            }
            //yeni olusturlan muzisyene benzersiz bir Id tanimlar Mevcut Idlerden en buyugu hangisi ise onu alir ve 1 ekler
            newMusician.Id = musicians.Any() ? musicians.Max(m => m.Id) + 1 : 1;
            //yeni muzisdeni listeye ekler
            musicians.Add(newMusician);

            //Yeni muzisyeni ekledikten sonra, 201 Created ile birlikte yeni muzisyenin detaylarını döndürür.
            return CreatedAtAction(nameof(GetMusicianById), new { id = newMusician.Id }, newMusician);

        }

        //----------------------------------------------------
        [HttpPut("{id}")] // var olan bir musizyenin bilgilerini gunceller
        public IActionResult UpdateMusician(int id, [FromBody] Musician updatedMusician)
        {
            // Model validasyonunun başarılı olup olmadığını kontrol eder.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Id'ye göre muzisyeni bulur ve silinmemiş olduğundan emin olur
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            if(id != updatedMusician.Id)//idler uyusmuyorsa
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }
            // Muzisyeni günceller
            musician.Name = updatedMusician.Name;
            musician.Occupation = updatedMusician.Occupation;
            musician.FunnyTrait = updatedMusician.FunnyTrait;
            // Güncellenmiş muzisyeni döndürür
            return Ok(musician);
        }
        //----------------------------------------------------
        [HttpDelete("{id}")] // var olan bir musizyenin bilgilerini siler
        public IActionResult DeleteMusician(int id) 
        {
            // Silinecek müzisyeni listede bul
            var musician = musicians.FirstOrDefault(m => m.Id == id && !m.IsDeleted);

            if (musician == null)
            {
                return NotFound();
            }

            musician.IsDeleted = true;
            return Ok(musician); // Silme işlemi başarılı, 200 OK döndürür


        }

        //----------------------------------------------------
        // Müzisyenleri adlarına, mesleklerine veya eğlenceli özelliklerine göre sorgu dizesine göre arar.

        [HttpGet("search")] // /api/musicians/search
        public ActionResult<List<Musician>> SearchMusicians([FromQuery]string query) 
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(musicians.Where(m => !m.IsDeleted).ToList());
            }
            var lowerCaseQuery = query.ToLowerInvariant();            // Bu, büyük/küçük harf duyarsız arama sağlar.

             // Sadece soft-delete edilmemiş müzisyenleri getir.
            var results = musicians.Where(m => !m.IsDeleted &&
                                                (m.Name.ToLowerInvariant().Contains(lowerCaseQuery) ||
                                                 m.Occupation.ToLowerInvariant().Contains(lowerCaseQuery) ||
                                                 m.FunnyTrait.ToLowerInvariant().Contains(lowerCaseQuery)))
                                     .ToList();
            return Ok(results);


        }












    }
}
