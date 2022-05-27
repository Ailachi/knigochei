using Knigochei.Models;

namespace Knigochei.ViewModels
{
    public class CartItemViewModel
    {
        public CartItem CartItem { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
