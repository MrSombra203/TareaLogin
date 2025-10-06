using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Login.Filters;

namespace Login.Controllers
{
    [AuthFilter] // IMPORTANTE: Proteger todo el controlador
    public class ProductosController : Controller
    {
        // Simulación de base de datos en memoria
        private static List<Producto> productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop HP", Descripcion = "Laptop HP 15.6 pulgadas", Precio = 899.99m, Stock = 10, FechaCreacion = DateTime.Now.AddDays(-30) },
            new Producto { Id = 2, Nombre = "Mouse Logitech", Descripcion = "Mouse inalámbrico", Precio = 25.50m, Stock = 50, FechaCreacion = DateTime.Now.AddDays(-20) },
            new Producto { Id = 3, Nombre = "Teclado Mecánico", Descripcion = "Teclado RGB", Precio = 75.00m, Stock = 30, FechaCreacion = DateTime.Now.AddDays(-15) },
            new Producto { Id = 4, Nombre = "Monitor Samsung", Descripcion = "Monitor 24 pulgadas Full HD", Precio = 199.99m, Stock = 15, FechaCreacion = DateTime.Now.AddDays(-10) },
            new Producto { Id = 5, Nombre = "Webcam Logitech", Descripcion = "Webcam HD 1080p", Precio = 79.99m, Stock = 25, FechaCreacion = DateTime.Now.AddDays(-5) }
        };

        // Variable para generar IDs únicos
        private static int siguienteId = 6;

        // GET: Productos (LISTAR - READ)
        public IActionResult Index()
        {
            return View(productos.OrderByDescending(p => p.FechaCreacion).ToList());
        }

        // GET: Productos/Details/5 (DETALLES - READ)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create (MOSTRAR FORMULARIO - CREATE)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create (GUARDAR - CREATE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Id = siguienteId++;
                producto.FechaCreacion = DateTime.Now;
                productos.Add(producto);

                TempData["Mensaje"] = "Producto creado exitosamente";
                TempData["TipoMensaje"] = "success";

                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5 (MOSTRAR FORMULARIO - UPDATE)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Edit/5 (ACTUALIZAR - UPDATE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var productoExistente = productos.FirstOrDefault(p => p.Id == id);

                if (productoExistente == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades
                productoExistente.Nombre = producto.Nombre;
                productoExistente.Descripcion = producto.Descripcion;
                productoExistente.Precio = producto.Precio;
                productoExistente.Stock = producto.Stock;

                TempData["Mensaje"] = "Producto actualizado exitosamente";
                TempData["TipoMensaje"] = "info";

                return RedirectToAction(nameof(Index));
            }

            return View(producto);
        }

        // GET: Productos/Delete/5 (CONFIRMAR - DELETE)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5 (ELIMINAR - DELETE)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto != null)
            {
                productos.Remove(producto);
                TempData["Mensaje"] = "Producto eliminado exitosamente";
                TempData["TipoMensaje"] = "danger";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}