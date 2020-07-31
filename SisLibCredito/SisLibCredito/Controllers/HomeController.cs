using SisLibCredito.BLL;
using SisLibCredito.DTO;
using SisLibCredito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisLibCredito.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<SelectListItem> listaTipoCredito = new List<SelectListItem>();
            CreditoBLL creditoBLL = new CreditoBLL();
            foreach (var item in creditoBLL.ListarTipoCredito())
            {
                listaTipoCredito.Add(new SelectListItem { Value = item.TipoId.ToString(), Text = item.Tipo});
            }
            //var listaTipoCredito = new[]
            //{
            //    new SelectListItem { Value = "1", Text = "Credito Direto" },
            //    new SelectListItem { Value = "2", Text = "Credito Consignado" },
            //    new SelectListItem { Value = "3", Text = "Credito Pessoa Jurídica" },
            //    new SelectListItem { Value = "4", Text = "Credito Pessoa Física" },
            //    new SelectListItem { Value = "5", Text = "Credito Imobiliário"},
            //};

            ViewBag.listaTipoCredito = new SelectList(listaTipoCredito, "Value", "Text");

            return View();
        }

        [HttpPost]
        public ActionResult Index(CreditoEntradaModel creditoEntradaModel)
        {
            CreditoBLL creditoBLL = new CreditoBLL();
            if (ModelState.IsValid)
            {
                // Fazendo DE/PARA Model -> DTO
                CreditoEntradaDTO creditoEntradaDTO = new CreditoEntradaDTO();
                creditoEntradaDTO.Valor = creditoEntradaModel.Valor;
                creditoEntradaDTO.TipoId = creditoEntradaModel.TipoId;
                creditoEntradaDTO.QtdParcelas = creditoEntradaModel.QtdParcelas;
                creditoEntradaDTO.DtPrimeiroVencimento = creditoEntradaModel.DtPrimeiroVencimento;

                CreditoResultadoDTO creditoResultadoDTO = creditoBLL.CalculaJuros(creditoEntradaDTO);

                // Fazendo DE/PARA DTO -> Model
                CreditoResultadoModel creditoResultadoModel = new CreditoResultadoModel();
                creditoResultadoModel.Status = creditoResultadoDTO.Status;
                creditoResultadoModel.ValorTotalComJuros = creditoResultadoDTO.ValorTotalComJuros;
                creditoResultadoModel.ValorDoJuros = creditoResultadoDTO.ValorDoJuros;

                return View("Resultado", creditoResultadoModel);
            }
            List<SelectListItem> listaTipoCredito = new List<SelectListItem>();
            foreach (var item in creditoBLL.ListarTipoCredito())
            {
                listaTipoCredito.Add(new SelectListItem { Value = item.TipoId.ToString(), Text = item.Tipo });
            }

            ViewBag.listaTipoCredito = new SelectList(listaTipoCredito, "Value", "Text");
            return View(creditoEntradaModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}