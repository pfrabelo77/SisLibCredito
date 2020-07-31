using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisLibCredito.DTO
{
    public class CreditoEntradaDTO
    {
        public decimal Valor { get; set; }
        public int TipoId { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DtPrimeiroVencimento { get; set; }

    }
}