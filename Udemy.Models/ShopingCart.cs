using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace udemy.Models;

public class ShopingCart
{
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Count { get; set; }

        public string ApplicationUserUserId { get; set; }

        [ValidateNever]
        [ForeignKey("ApplicationUserUserId")]
        public ApplicationUser ApplicationUser { get; set; }


}