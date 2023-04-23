using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using tienda.Interface;
using tienda.Models;

namespace tienda.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly IRepositorioFabricante repositorioFabricante;

        public FabricanteController(IRepositorioFabricante repositorioFabricante)
        {
            this.repositorioFabricante = repositorioFabricante;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await repositorioFabricante.Obtener();
            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(fabricante fabricante)
        {
            if (!ModelState.IsValid)
            {
                return View(fabricante);
            }

            await repositorioFabricante.Crear(fabricante);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var fabricante = await repositorioFabricante.ObtenerPorId(id);
            if (fabricante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(fabricante);
        }

        [HttpPost]
        public async Task<IActionResult> Borrar(int id)
        {
            var fabricante = await repositorioFabricante.ObtenerPorId(id);
            if (fabricante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioFabricante.Borrar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var tipoFabricante = await repositorioFabricante.ObtenerPorId(id);

            if (tipoFabricante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");

            }
            return View(tipoFabricante);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(fabricante fabricante)
        {
            var tipoExiste = await repositorioFabricante.ObtenerPorId(fabricante.id);

            if (tipoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioFabricante.Actualizar(fabricante);
            return RedirectToAction("Index");
        }

        public IActionResult Detalle()
        {
            return View();
        }

    }
}
