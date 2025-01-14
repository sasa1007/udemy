using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace udemy.Models;

public class Product
{
    [Key] public int Id { get; set; }

    [MaxLength(50)] public string Name { get; set; }

    [MaxLength(200)] public string Description { get; set; }

    [MaxLength(50)] public string Isbn { get; set; }

    [MaxLength(50)] public string Author { get; set; }


    [Display(Name = "List Price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }


    [Display(Name = "Price for 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }


    [Display(Name = "Price for 50+")]
    [Range(1, 1000)]
    public double Price50 { get; set; }


    [Display(Name = "Price for 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }

    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")] public Category Category { get; set; }

    public string ImageUrl { get; set; }
}