using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Display Name")]
        public string Name{ get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order Must be 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
