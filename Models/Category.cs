using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace udemy.Models;

public class Category
{
    
    // [Key] if Id is not CategoryId then you need to add
    // if CategoryId you dont need key
    [Key]
    public int Id { get; set; }
    
    [Required]
    [DisplayName("Category Name")]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [DisplayName("Display Order")]
    [Range(0, 100, ErrorMessage = "Ne valja broj")] 
    public int DisplayOrder { get; set; }
    
}