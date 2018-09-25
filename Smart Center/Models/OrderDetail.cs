using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Barcode { set; get; }

        [ForeignKey(nameof(Barcode))]
        public Product Product { get; set; }


        [Required]
        public int OrderId { set; get; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        //static qte
        [Required]
        public int Qte { get; set; }

        [Required]
        public float ByePrice { get; set; }

        [Required]
        public float SalesPrice { get; set; }

        [Required]
        public float Discount { get; set; }

        [Required]
        public float Bin { get; set; }

        [Required]
        public Mode Mode { get; set; }

        public string IMEIOrders { get; set; }

        [Required]
        public int PurchesDetailId { get; set; }

        [ForeignKey(nameof(PurchesDetailId))]
        public PurchasDetail PurchasDetail { get; set; }

        [Required]
        public float TotalAmount { get; set; }

    }

    public enum Mode
    {
        Single = 1,
        Multi = 2
    }

}
