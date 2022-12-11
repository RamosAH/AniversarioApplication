using AniversarioApplication.Application;
using AniversarioApplication.Database;
using AniversarioApplication.Entidade;
using Ger_Aniversario.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace AniversarioApplication.Controllers {
    public class HomeController : Controller {
        private AniversarioManager AniversarioManager { get; set; }

        public HomeController() {

            AniversarioManager = new AniversarioManager();
        }

        public IActionResult Index() {

            var result = AniversarioManager.ObterTodos();
            ViewBag.AniverHoje = AniversarioManager.AniversarioHoje(result);
            return View(result);
        }

        public IActionResult Delete(int amigoId) {

            var result = AniversarioManager.ObterPorId(amigoId);
            return View(result);
        }

        public IActionResult Buscar() {
           
            return View();
        }

        public IActionResult Editar(int amigoId) {

            var result = AniversarioManager.ObterPorId(amigoId);
            return View(result);
        }

        public IActionResult Excluir(int amigoId) {

            AniversarioManager.Excluir(amigoId);
            return RedirectToAction("Index");
        }

        public IActionResult Detalhes(int amigoId) {

            var result = AniversarioManager.ObterPorId(amigoId);
            ViewBag.Detalhes = AniversarioManager.FaltaParaAniversario(result);
            return View(result);
        }

        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Amigo model) {
            if (ModelState.IsValid == false)
                return View("Add");

            AniversarioManager.Salvar(model);

            return RedirectToAction("Index");
        }
        [HttpPost]
            public IActionResult SaveEdit(Amigo model, int amigoId) {
            if (ModelState.IsValid == false)
                return View("Editar");

            AniversarioManager.SalvarEdit(model, amigoId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Pesquisar(string part) {

            var lista = AniversarioManager.ObterTodos();
            var encontrados = AniversarioManager.Buscar(lista, part);
            return View("Buscar", encontrados);
        }
    }
}
