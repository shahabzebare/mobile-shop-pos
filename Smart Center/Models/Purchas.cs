using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Purchas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public string Number { get; set; }

        public string Disc { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }

        public int  adminId { get; set; }

        [ForeignKey(nameof(adminId))]
        public Admin Admin { get; set; }

        public int companyId { set; get; }

        [ForeignKey(nameof(companyId))]
        public Company Company { get; set; }

        [ForeignKey("purchasId")]
        public ICollection<PurchasDetail> purchasDetails { get; set; }


        [ForeignKey("Purchas_Id")]
        public ICollection<Debt> debt { get; set; }




    }
}
