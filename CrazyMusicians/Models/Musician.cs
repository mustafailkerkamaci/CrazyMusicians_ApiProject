using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

namespace CrazyMusicians.Models
{
    public class Musician
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Musician name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Musician occupation is required.")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Musician funny trait is required.")]
        public string FunnyTrait { get; set; }
        public bool IsDeleted { get; set; } // Soft delete islemi icin


    }
}
