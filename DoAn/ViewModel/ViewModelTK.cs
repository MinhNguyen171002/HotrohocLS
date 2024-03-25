using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn.ViewModel
{
    public class ViewModelTK
    {
        public int IdThoiKy { get; set; }

        public string TenThoiKy { get; set; }

        public string NamBatDau { get; set; }

        public string NamKetThuc { get; set; }

        public string TomTatTK { get; set; }


    }
}