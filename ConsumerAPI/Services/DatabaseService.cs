using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerAPI.Data;
using ConsumerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsumerAPI.Services
{
    public class DatabaseService
    {
        private readonly TransferOrderContext _context;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(TransferOrderContext context, ILogger<DatabaseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveTransferOrdersAsync(List<TransferOrder> transferOrders)
        {
            try
            {
                // Filtrar pedidos que não estão presentes no banco de dados
                var newOrders = transferOrders
                    .Where(order => !_context.TransferOrders
                        .Any(t => t.Pedido == order.Pedido))
                    .ToList();

                _logger.LogInformation($"Distintos pedidos a serem salvos: {newOrders.Count}");

                // Adicionar pedidos filtrados ao contexto e salvar alterações
                _context.TransferOrders.AddRange(newOrders);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Alterações salvas no banco de dados.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao salvar alterações no banco de dados: {ex.Message}");
            }
            //// Remover duplicados da lista de transferOrders antes de inserir
            //var distinctOrders = transferOrders
            //    .GroupBy(o => new { o.Pedido, o.OrderDate })
            //    .Select(g => g.First())
            //    .ToList();

            //_logger.LogInformation($"Distintos pedidos a serem salvos: {distinctOrders.Count}");

            //foreach (var order in distinctOrders)
            //{
            //    try
            //    {
            //        // Adicionar logs detalhados
            //        _logger.LogInformation($"Verificando existência do pedido {order.Pedido} com data {order.OrderDate}.");

            //        // Verificar se o pedido já existe no banco de dados
            //        var exists = await _context.TransferOrders
            //            .AnyAsync(t => t.Pedido == order.Pedido && t.OrderDate == order.OrderDate);

            //        if (exists)
            //        {
            //            _logger.LogInformation($"Pedido {order.Pedido} já existe no banco de dados.");
            //        }
            //        else
            //        {
            //            _context.TransferOrders.Add(order);
            //            _logger.LogInformation($"Pedido {order.Pedido} adicionado ao banco de dados.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError($"Erro ao verificar/adicionar pedido {order.Pedido}: {ex.Message}");
            //    }
            //}

            //try
            //{
            //    // Salvar alterações
            //    await _context.SaveChangesAsync();
            //    _logger.LogInformation("Alterações salvas no banco de dados.");
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Erro ao salvar alterações no banco de dados: {ex.Message}");
            //}
        }
    }

}
