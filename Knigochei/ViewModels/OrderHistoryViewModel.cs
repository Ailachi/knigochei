using Knigochei.Models;

namespace Knigochei.ViewModels
{
    public class OrderHistoryViewModel
    {
        public List<Order> Orders { get; set; }
        public User User { get; set; }
    }
}
