using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class MyDebtPay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public int Customer_id { set; get; }

        [ForeignKey(nameof(Customer_id))]
        public Customer Customer { get; set; }

        [Required]
        public float Pay { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
