using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class PurchasDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string Barcode { set; get; }

        [ForeignKey(nameof(Barcode))]
        public Product Product { get; set; }


        [Required]
        public int purchasId { set; get; }

        [ForeignKey(nameof(purchasId))]
        public Purchas Purchas { get; set; }


        //static qte
        [Required]
        public int Qte { get; set; }

        //changed Qte
        [Required]
        public int QteCh { get; set; }


        [Required]
        public float ByePrice { get; set; }

        [Required]
        public float SalesPrice { get; set; }

        [Required]
        public float TotalAmount { get; set; }

        [ForeignKey("purchesDetailId")]
        public ICollection<IMEI> IMEIs { get; set; }

    }
}
