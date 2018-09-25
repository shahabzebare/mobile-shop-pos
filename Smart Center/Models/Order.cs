using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Disc { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }

        public int AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        public Admin Admin { get; set; }

        public int CustomerId { set; get; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [ForeignKey("OrderId")]
        public ICollection<OrderDetail> OrderDetails  { get; set; }

        [ForeignKey("Order_Id")]
        public ICollection<MyDebt> myDebts { get; set; }

        [ForeignKey("OrderID")]
        public ICollection<Premiums> Premiums { get; set; }

    }
}
