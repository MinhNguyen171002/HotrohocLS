using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoAn.Models;

namespace DoAnWeb.Models
{
    public class NhanVatLS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNV { get; set; }

        [Required]
        [StringLength(250)]
        public string TenNhanVat { get; set; }

        [Required]
        public string NgaySinh { get; set; }

        [Required]
        public string NgayMat { get; set; }

        public string NoiDungTomTatNVUrl { get; set; }
       
        public int? IdThoiKy { get; set; }

        public virtual ThoiKy ThoiKy { get; set; }
        public virtual ICollection<SuKien> SuKien { get; set; } = new HashSet<SuKien>();

        public virtual ICollection<Image1> Images { get; set; } 


    }
}