using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisLibCredito.Models
{
    public class CreditoEntradaModel
    {
        [Required]
        [Display(Name = "Valor do Crédito R$")]
        public decimal Valor { get; set; }

        [Display(Name = "Tipo de Crédito")]
        [Required]
        public int TipoId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Qtd de parcelas deve ser um valor inteiro")]
        [Display(Name = "Quantidade de Parcelas")]
        public int QtdParcelas { get; set; }


        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Data do Primeiro Vencimento")]
        public DateTime DtPrimeiroVencimento { get; set; }

    }
}