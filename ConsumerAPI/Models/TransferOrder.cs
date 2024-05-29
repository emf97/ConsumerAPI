using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumerAPI.Models
{
    public class TransferOrder
    {
        public int Id { get; set; }

        public string Pedido { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
