using SisLibCredito.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisLibCredito.BLL
{
    public class CreditoBLL
    {
        private bool ValidaCreditoEntrada(CreditoEntradaDTO creditoEntradaDTO)
        {
            // O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00
            if (creditoEntradaDTO.Valor > 1000000)
            {
                return false;
            }
            //A quantidade de parcelas máximas é de 72x e a mínima é de 5x
            if (creditoEntradaDTO.QtdParcelas < 5 || creditoEntradaDTO.QtdParcelas > 72){
                return false;
            }
            //Para o crédito de pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00
            if (creditoEntradaDTO.Tipo == 3 && creditoEntradaDTO.Valor < 15000)
            {
                return false;
            }
            //A data do primeiro vencimento sempre será no mínimo D+15 (Dia atual + 15 dias), e no máximo, D+40 (Dia atual + 40 dias)
            if (creditoEntradaDTO.DtPrimeiroVencimento.Date < DateTime.Now.AddDays(15).Date || creditoEntradaDTO.DtPrimeiroVencimento.Date > DateTime.Now.AddDays(40).Date)
            {
                return false;
            }
            return true;
        }

        public CreditoResultadoDTO CalculaJuros(CreditoEntradaDTO creditoEntradaDTO)
        {
            CreditoResultadoDTO creditoResultadoDTO = new CreditoResultadoDTO();
            if (ValidaCreditoEntrada(creditoEntradaDTO))
            {
                creditoResultadoDTO.Status = "Aprovado";
                //Calcular aqui os juros
            }
            else
            {
                creditoResultadoDTO.Status = "Recusado";
            }
            return creditoResultadoDTO;
        }
    }
}