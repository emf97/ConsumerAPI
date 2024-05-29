using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsumerAPI.Models;

namespace ConsumerAPI.Data
{
    public class TransferOrderContext : DbContext
    {
        public TransferOrderContext(DbContextOptions<TransferOrderContext> options) : base(options)
        {

        }

        public DbSet<TransferOrder> TransferOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransferOrder>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TransferOrder>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
