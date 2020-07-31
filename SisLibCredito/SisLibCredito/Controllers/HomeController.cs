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

            var listaTipoCredito = new[]
            {
                new SelectListItem { Value = "1", Text = "Credito Direto" },
                new SelectListItem { Value = "2", Text = "Credito Consignado" },
                new SelectListItem { Value = "3", Text = "Credito Pessoa Jurídica" },
                new SelectListItem { Value = "4", Text = "Credito Pessoa Física" },
                new SelectListItem { Value = "5", Text = "Credito Imobiliário"},
            };

            ViewBag.listaTipoCredito = new SelectList(listaTipoCredito.ToList(), "Value", "Text");

            return View();
        }

        [HttpPost]
        public ActionResult Index(CreditoEntradaModel creditoModel)
        {
            if (ModelState.IsValid)
            {
                CreditoResultadoModel creditoResultadoModel = new CreditoResultadoModel();
                creditoResultadoModel.Status = "Aprovado";
                creditoResultadoModel.ValorTotalComJuros = 1000;
                creditoResultadoModel.ValorDoJuros = 200;

                //chamada da BLL
                return View("Resultado", creditoResultadoModel);
            }
            var listaTipoCredito = new[]
            {
                new SelectListItem { Value = "1", Text = "Credito Direto" },
                new SelectListItem { Value = "2", Text = "Credito Consignado" },
                new SelectListItem { Value = "3", Text = "Credito Pessoa Jurídica" },
                new SelectListItem { Value = "4", Text = "Credito Pessoa Física" },
                new SelectListItem { Value = "5", Text = "Credito Imobiliário"},
            };

            ViewBag.listaTipoCredito = new SelectList(listaTipoCredito.ToList(), "Value", "Text");
            return View(creditoModel);
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