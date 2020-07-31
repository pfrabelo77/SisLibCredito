using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisLibCredito.Models
{
    public class CreditoResultadoModel
    {

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Valor Total c/ Juros R$")]
        public decimal ValorTotalComJuros { get; set; }

        [Display(Name = "Valor do Juros R$")]
        public decimal ValorDoJuros { get; set; }
    }
}