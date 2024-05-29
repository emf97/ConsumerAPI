using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferOrdersController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            var transferOrders = new List<TransferOrder> {
                new TransferOrder {Pedido = "TO1234", OrderDate = DateTime.Now},
                new TransferOrder {Pedido = "T05678", OrderDate= DateTime.Now},
                new TransferOrder {Pedido = "TO1234", OrderDate = DateTime.Now},
                new TransferOrder {Pedido = "T05678", OrderDate= DateTime.Now},
                new TransferOrder {Pedido = "T09999", OrderDate= DateTime.Now},
            };

            return Ok(transferOrders);
        }
    }
}
