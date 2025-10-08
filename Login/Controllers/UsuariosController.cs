using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Login.Filters;

namespace Login.Controllers
{
    [AuthFilter]
    public class UsuariosController : Controller
    {
        private List<Usuario> ObtenerUsuarios()
        {
            return AuthController.usuarios;
        }

        public IActionResult Index()
        {
            var usuarios = ObtenerUsuarios().OrderByDescending(u => u.FechaRegistro).ToList();
            return View(usuarios);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = ObtenerUsuarios().FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ObtenerUsuarios().Any(u => u.NombreUsuario.ToLower() == usuario.NombreUsuario.ToLower()))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya existe");
            }

            if (ObtenerUsuarios().Any(u => u.Email.ToLower() == usuario.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "El email ya está registrado");
            }

            if (ModelState.IsValid)
            {
                usuario.Id = AuthController.siguienteId++;
                usuario.FechaRegistro = DateTime.Now;
                ObtenerUsuarios().Add(usuario);

                TempData["Mensaje"] = "Usuario creado exitosamente";
                TempData["TipoMensaje"] = "success";

                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = ObtenerUsuarios().FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ObtenerUsuarios().Any(u => u.NombreUsuario.ToLower() == usuario.NombreUsuario.ToLower() && u.Id != id))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya existe");
            }

            if (ObtenerUsuarios().Any(u => u.Email.ToLower() == usuario.Email.ToLower() && u.Id != id))
            {
                ModelState.AddModelError("Email", "El email ya está registrado");
            }

            if (ModelState.IsValid)
            {
                var usuarioExistente = ObtenerUsuarios().FirstOrDefault(u => u.Id == id);

                if (usuarioExistente == null)
                {
                    return NotFound();
                }

                usuarioExistente.NombreUsuario = usuario.NombreUsuario;
                usuarioExistente.Contraseña = usuario.Contraseña;
                usuarioExistente.NombreCompleto = usuario.NombreCompleto;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Rol = usuario.Rol;
                usuarioExistente.Activo = usuario.Activo;

                TempData["Mensaje"] = "Usuario actualizado exitosamente";
                TempData["TipoMensaje"] = "info";

                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = ObtenerUsuarios().FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (id == 1)
            {
                TempData["Mensaje"] = "No se puede eliminar el usuario administrador";
                TempData["TipoMensaje"] = "warning";
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 1)
            {
                TempData["Mensaje"] = "No se puede eliminar el usuario administrador";
                TempData["TipoMensaje"] = "warning";
                return RedirectToAction(nameof(Index));
            }

            var usuario = ObtenerUsuarios().FirstOrDefault(u => u.Id == id);

            if (usuario != null)
            {
                ObtenerUsuarios().Remove(usuario);
                TempData["Mensaje"] = "Usuario eliminado exitosamente";
                TempData["TipoMensaje"] = "danger";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ToggleStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = ObtenerUsuarios().FirstOrDefault(u => u.Id == id);

            if (usuario != null)
            {
                usuario.Activo = !usuario.Activo;
                TempData["Mensaje"] = $"Usuario {(usuario.Activo ? "activado" : "desactivado")} exitosamente";
                TempData["TipoMensaje"] = "info";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
