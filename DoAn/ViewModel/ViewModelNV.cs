using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn.ViewModel
{
    public class ViewModelNV
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNV { get; set; }
        public string TenNhanVat { get; set; }

        public string NgaySinh { get; set; }

        public string NgayMat { get; set; }
        public string ImageUrl { get; set; }

        public string NoiDungTomTatNVUrl { get; set; }
        public int IdThoiKy { get; set; }
        public IEnumerable<ThoiKy> ThoiKies { get; set; }
    }
}