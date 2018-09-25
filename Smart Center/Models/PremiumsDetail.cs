using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class PremiumsDetail
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PremiumID { set; get; }

        [ForeignKey(nameof(PremiumID))]
        public Premiums Premiums { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public float price_pay { get; set; }

        [Required]
        public DateTime payDate { get; set; }


    }
}
