using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tienda.Interface;
using tienda.Models;

namespace tienda.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepositorioProducto repositorioProducto;
        private readonly IRepositorioFabricante repositorioFabricante;
        private readonly IMapper mapper;

        public ProductoController(IRepositorioProducto repositorioProducto, IRepositorioFabricante repositorioFabricante
            ,IMapper mapper)
        {
            this.repositorioProducto = repositorioProducto;
            this.repositorioFabricante = repositorioFabricante;
            this.mapper = mapper;
        }

        // GET: ProductoController
        public async Task<ActionResult> Index()
        {
            var lista = await repositorioProducto.Obtener();
            return View(lista);
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var modelo = new ProductoVM();
            modelo.TipoFabricanttes = await ObtenerTipoFabricante();
            return View(modelo);
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductoVM productoVM)
        {
            var tipo = await repositorioFabricante.ObtenerPorId(productoVM.id_fabricante);

            if (tipo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            if (!ModelState.IsValid)
            {
                productoVM.TipoFabricanttes = await ObtenerTipoFabricante();
                return View(productoVM);
            }

            await repositorioProducto.Crear(productoVM);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerTipoFabricante()
        {
            var tipoFabricante = await repositorioFabricante.Obtener();
            return tipoFabricante.Select(x => new SelectListItem(x.nombre, x.id.ToString()));
        }
        // GET: ProductoController/Edit/5
       
        public async Task<ActionResult> Edit(int id)
        {
            var producto = await repositorioProducto.ObtenerPorId(id);
            if (producto is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            var modelo = mapper.Map<ProductoVM>(producto);

            modelo.TipoFabricanttes = await ObtenerTipoFabricante();
            return View(modelo);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductoVM productoVM)
        {
            var producto = await repositorioProducto.ObtenerPorId(productoVM.id);

            if (producto is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            var tipoProducto = await repositorioFabricante.ObtenerPorId(productoVM.id_fabricante);

            if (tipoProducto is null)
            {
                return RedirectToAction("NoEcontrado", "Home");
            }

            await repositorioProducto.Actualizar(productoVM);
            return RedirectToAction("Index");

        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
