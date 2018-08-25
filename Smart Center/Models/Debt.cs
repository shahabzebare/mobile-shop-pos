using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Debt
    {
      
        [Required]
        public int Company_Id { set; get; }

        [ForeignKey(nameof(Company_Id))]
        public Company Company { get; set; }


        [Required]
        [ForeignKey(nameof(Purchas))]
        public int Purchas_Id { set; get; }

        public Purchas Purchas { get; set; }


        [Required]
        public float Pay { get; set; }

        [Required]
        public float Rem { get; set; }

    }
}
