using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace udemy.Models;

public class OrderDetail
{
    [Key] public int Id { get; set; }

    [Required]
    public int OrderHeaderId { get; set; }

    [ValidateNever]
    [ForeignKey("OrderHeaderId")]
    public OrderHeader OrderHeader { get; set; }

    [Required]
    public int ProductId { get; set; }

    [ValidateNever]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    public int Count { get; set; }
    public Double Price { get; set; }
}