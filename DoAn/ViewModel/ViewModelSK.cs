using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.ViewModel
{
    public class ViewModelSK
    {
        public int IdNoiDung { get; set; }
        public string TenNoiDung { get; set; }
        public string NoiDungSK { get; set; }

        public int IdThoiKy { get; set; }
        public IEnumerable<ThoiKy> thoiKy { get; set; }
        public string TomTatSK { get; set; }
        public int[] IdNV { get; set; }
        public List<NhanVatLS> nhanVat { get;set; }

    }
}