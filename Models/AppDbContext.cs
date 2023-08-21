using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MSSQLStoreAPI.Models
{
    public class AppDbContext: DbContext //inherit ได้ Class เดียว / implement ได้หลาย class มาใส่ในตัวมัน
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){


        }
        public DbSet<Category> Categories {get; set;}
        public DbSet<Products> Products {get; set;}
    }
}