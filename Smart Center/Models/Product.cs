﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Disc { get; set; }

        [Required]
        public string Barcode { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public bool isFavorate { get; set; }

        [Required]
        public float bye_price { get; set; }

        [Required]
        public float sales_price { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public bool isHaveIMEI { get; set; }

        
        public int categoryId { set; get; }

        [ForeignKey(nameof(categoryId))]
        public Category Category { get; set; }
    }
}