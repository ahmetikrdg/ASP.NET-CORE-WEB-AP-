using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelFinder.DataAccess
{
    public class HotelDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=.;database=HotelDb;integrated security=true;");
        }
        public DbSet<Hotel> Hotels { get; set; }//DbSet diyince update delete add gibi metodlara erişmiş olursun istersen dbsete git bak
    }
}
