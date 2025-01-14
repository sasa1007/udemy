using Microsoft.AspNetCore.Mvc.Rendering;

namespace udemy.Models.ViewModels;



public class ProductVm
{
    public Product Product { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}