using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoAn.Models;

namespace DoAnWeb.Models
{
    public class ThoiKy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdThoiKy { get; set; }

        [Required]
        [StringLength(250)]
        public string TenThoiKy { get; set; }
        [Required]

        public string NamBatDau { get; set; }

        public string NamKetThuc { get; set; }
        public string TomTatTK { get; set; }
        public virtual ICollection<NhanVatLS> NhanVatLs { get; set; }
        public virtual ICollection<SuKien> SuKien { get; set; } = new HashSet<SuKien>();

    }
}