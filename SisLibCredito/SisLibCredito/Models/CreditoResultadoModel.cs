using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisLibCredito.Models
{
    public class CreditoResultadoModel
    {

        public string Status { get; set; }
        public decimal ValorTotalComJuros { get; set; }
        public decimal ValorDoJuros { get; set; }
    }
}