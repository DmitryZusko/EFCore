using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseModel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(16,3)")]
        public decimal Amount { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}