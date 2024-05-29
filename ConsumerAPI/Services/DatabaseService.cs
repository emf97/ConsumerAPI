using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerAPI.Data;
using ConsumerAPI.Models;

namespace ConsumerAPI.Services
{
    public class DatabaseService
    {
        private readonly TransferOrderContext _context;

        public DatabaseService(TransferOrderContext context)
        {
            _context = context;
        }

        public async Task SaveTransferOrdersAsync(List<TransferOrder> transferOrders)
        {
            foreach (var order in transferOrders)
            {
                _context.TransferOrders.Add(new TransferOrder
                {
                    Pedido = order.Pedido,
                    OrderDate = order.OrderDate,

                });
            }

            await _context.TransferOrders.AddRangeAsync(transferOrders);
            await _context.SaveChangesAsync();
        }
    }

}
