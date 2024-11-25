using System.ComponentModel.DataAnnotations;

namespace udemy.Models;

public class Category
{
    
    // [Key] if Id is not CategoryId then you need to add
    // if CategoryId you dont need key
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public int DisplayOrder { get; set; }
    
}