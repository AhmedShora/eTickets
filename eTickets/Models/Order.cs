using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Order
    {
        public Order()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }


    }
}
