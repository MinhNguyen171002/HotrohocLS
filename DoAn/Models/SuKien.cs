using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DoAn.Models;

namespace DoAnWeb.Models
{
    public class SuKien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNoiDung { get; set; }
        public string TenNoiDung { get; set; }
        public string NoiDungSK { get; set; }

        public string TomTatSK { get; set; }

        public int? IdThoiKy { get; set; }
        public virtual ThoiKy ThoiKy { get; set; }

        public virtual ICollection<NhanVatLS> NV { get; set; } = new HashSet<NhanVatLS>();

        public string IdNv { get; set; }

        public virtual ICollection<Image1> Images { get; set; }

    }
}