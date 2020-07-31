using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisLibCredito.DTO
{
    public class TipoCreditoDTO
    {
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public decimal TaxaJurosMes { get; set; }
    }
}