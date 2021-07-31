using ASSIGMENT_Danh_Ba.Models;
using Microsoft.EntityFrameworkCore;

namespace ASSIGMENT_Danh_Ba.DBContext
{
    public class DB_CONTEXT_DanhBa:DbContext
    {
        public DbSet<Nguoi> Nguois { get; set; }
        public DbSet<DanhBa> DanhBas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-NVB7S6L; Initial Catalog=ASSIGRMENT_WINFORM; Persist Security Info=True; User ID=kieu96;Password=123");
        }
    }
}