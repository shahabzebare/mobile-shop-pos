using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class DebtPay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public int Company_id { set; get; }

        [ForeignKey(nameof(Company_id))]
        public Company Company { get; set; }

        [Required]
        public float Pay { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
