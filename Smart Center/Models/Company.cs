using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Email { get; set; }


        [ForeignKey("companyId")]
        public ICollection<Purchas> Purchases { get; set; }

        [ForeignKey("Company_id")]
        public ICollection<DebtPay> DebtPays { get; set; }

        [ForeignKey("Company_Id")]
        public ICollection<Debt> Debts { get; set; }
    }
}
