using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class MyDebt
    {
        [Required]
        public int Customer_Id { set; get; }

        [ForeignKey(nameof(Customer_Id))]
        public Customer Customer { get; set; }


        [Required]
        public int Order_Id { set; get; }

        [ForeignKey(nameof(Order_Id))]
        public Order Order { get; set; }


        [Required]
        public float Pay { get; set; }

        [Required]
        public float Rem { get; set; }
    }
}
