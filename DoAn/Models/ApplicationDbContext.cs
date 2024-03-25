using DoAnWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Image1> Images { get; set; }
        public DbSet<ThoiKy> thoiKies { get; set; }
        public DbSet<SuKien> SuKien { get; set; }
        public DbSet<NhanVatLS> NhanVatLs { get; set; }
        public DbSet<GuestUser> GuestUsers { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}