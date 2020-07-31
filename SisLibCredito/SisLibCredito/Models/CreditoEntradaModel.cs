using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisLibCredito.Models
{
    public class CreditoEntradaModel
    {
        //[DataType(DataType.Currency)]
        [Required]
        public decimal Valor { get; set; }

        [Display(Name = "Tipo de Crédito")]
        [Required]
        //{ Value = "1", Text = "Credito Direto" },
        //{ Value = "2", Text = "Credito Consignado" },
        //{ Value = "3", Text = "Credito Pessoa Jurídica" },
        //{ Value = "4", Text = "Credito Pessoa Física" },
        //{ Value = "5", Text = "Credito Imobiliário"},
        public int Tipo { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Qtd de parcelas deve ser um valor inteiro")]
        public int QtdParcelas { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DtPrimeiroVencimento { get; set; }

    }
}