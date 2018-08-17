using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class IMEI
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ImEI { get; set; }

        public bool inStock { get; set; } = true;

        [Required]
        public int purchesDetailId { set; get; }

        [ForeignKey(nameof(purchesDetailId))]
        public PurchasDetail PurchasDetail { get; set; }

    }
}
