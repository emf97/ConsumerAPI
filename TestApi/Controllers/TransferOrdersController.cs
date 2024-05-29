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
                new TransferOrder {Id = 1, Pedido = "TO1234", OrderDate = DateTime.Now},
                new TransferOrder {Id = 2, Pedido = "T05678", OrderDate= DateTime.Now},
            };

            return Ok(transferOrders);
        }
    }
}
