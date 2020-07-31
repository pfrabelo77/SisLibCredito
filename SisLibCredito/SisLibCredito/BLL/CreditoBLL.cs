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
            if (creditoEntradaDTO.QtdParcelas < 5 || creditoEntradaDTO.QtdParcelas > 72)
            {
                return false;
            }
            //Para o crédito de pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00
            if (creditoEntradaDTO.TipoId == 3 && creditoEntradaDTO.Valor < 15000)
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
                CreditoBLL creditoBLL = new CreditoBLL();

                decimal J;
                decimal i = creditoBLL.ObterTipoCredito(creditoEntradaDTO.TipoId).TaxaJurosMes;
                int t = creditoEntradaDTO.QtdParcelas;
                decimal C = creditoEntradaDTO.Valor;
                J = C * i/100 * t; // (fórmula dos juros simples)

                creditoResultadoDTO.ValorDoJuros = J;
                creditoResultadoDTO.ValorTotalComJuros = creditoEntradaDTO.Valor + J;


            }
            else
            {
                creditoResultadoDTO.Status = "Recusado";
            }
            return creditoResultadoDTO;
        }

        public List<TipoCreditoDTO> ListarTipoCredito()
        {
            List<TipoCreditoDTO> listaTipoCredito = new List<TipoCreditoDTO>() {
                new TipoCreditoDTO() { TipoId = 1, Tipo = "Credito Direto", TaxaJurosMes = 2 },
                new TipoCreditoDTO() { TipoId = 2, Tipo = "Credito Consignado", TaxaJurosMes = 1 },
                new TipoCreditoDTO() { TipoId = 3, Tipo = "Credito Pessoa Jurídica", TaxaJurosMes = 5 },
                new TipoCreditoDTO() { TipoId = 4, Tipo = "Credito Pessoa Física", TaxaJurosMes = 3 },
                new TipoCreditoDTO() { TipoId = 5, Tipo = "Credito Imobiliário", TaxaJurosMes = (decimal)0.75 } //Juros do ano dividido por 12
            };
            return listaTipoCredito;
        }

        public TipoCreditoDTO ObterTipoCredito(int tipoId)
        {
            return ListarTipoCredito().Where(x => x.TipoId == tipoId).SingleOrDefault();
        }
    }
}