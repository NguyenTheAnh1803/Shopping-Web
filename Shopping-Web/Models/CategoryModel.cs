using System.ComponentModel.DataAnnotations;

namespace Shopping_Web.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap ten danh muc")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap mo ta danh muc")]
        public string Description { get; set; }
        [Required]
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
