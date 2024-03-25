using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoAnWeb.Models;

namespace DoAn.Models
{
    public class Image1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDImage{ get; set; } 

        public string UrlImage { get; set; }

        public virtual NhanVatLS NhanVatLS { get; set; }
        public int? IdNV { get; set; }
        public virtual SuKien SuKien { get; set; }
        public int? IdNoiDung { get; set; }


    }
}