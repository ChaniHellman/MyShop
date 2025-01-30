using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DatabaseFixture
    {
        public MyShopContext Context { get; private set; }
        public DatabaseFixture() {
            var options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer("Server=DESKTOP-E0FAPSB\\SQLEXPRESS;Database=Tests;Trusted_Connection=True;TrustServerCertificate=True").Options;
            Context = new MyShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose() { 
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
