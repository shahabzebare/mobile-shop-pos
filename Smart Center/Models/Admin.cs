using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Center.Models
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="الأسم مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "الأسم المستخدم مطلوب")]
        public string Username { get; set; }

        [Required(ErrorMessage = "الرقم السري مطلوب"),
            Range(6,20,ErrorMessage ="الرقم السري يكون ما بين 6 الي 20")]
        public string Password { get; set; }

        public Type Type { get; set; }


        [ForeignKey("adminId")]
        public ICollection<Purchas> Purchases { get; set; }

    }

    public enum Type
    {
        FullAdmin=1,
        NormalAdmin=2
    }
}
