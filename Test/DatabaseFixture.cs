using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DatabaseFixture: IDisposable
    {
        public MyShopContext Context { get; private set; }
        public DatabaseFixture() {
            var options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer("Server=SRV2\\PUPILS;Database=Tests_328177589;Trusted_Connection=True;TrustServerCertificate=True").Options;
            Context = new MyShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose() { 
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
