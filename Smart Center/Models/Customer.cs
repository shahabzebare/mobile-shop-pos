using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [ForeignKey("CustomerId")]
        public ICollection<Customer> Customers { get; set; }

        [ForeignKey("Customer_Id")]
        public ICollection<MyDebt> MyDebts { get; set; }

        [ForeignKey("Customer_id")]
        public ICollection<MyDebtPay> MyDebtPays { get; set; }

        [ForeignKey("CustomerID")]
        public ICollection<Premiums> Premiums { get; set; }
    }
}
