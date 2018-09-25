using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Premiums
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CustomerID { set; get; }

        [ForeignKey(nameof(CustomerID))]
        public Customer Customer { get; set; }


        [Required]
        public int OrderID { set; get; }

        [ForeignKey(nameof(OrderID))]
        public Order Order { get; set; }

        [Required]
        public DateTime Prem_time { get; set; }

        public String  Note { get; set; }

        [Required]
        public int WorkId { set; get; }

        [ForeignKey(nameof(WorkId))]
        public Work Work { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public DateTime first_Pay { get; set; }


        [ForeignKey("PremiumID")]
        public ICollection<PremiumsDetail> premiumsDetails { get; set; }




    }
}
