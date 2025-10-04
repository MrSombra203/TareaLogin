using Microsoft.AspNetCore.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class AuthController : Controller
    {
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, NombreUsuario = "admin", Contraseña = "123456", NombreCompleto = "Administrador" },
            new Usuario { Id = 2, NombreUsuario = "usuario", Contraseña = "pass123", NombreCompleto = "Usuario Normal" }
        };

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string nombreUsuario, string contraseña)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Por favor ingrese usuario y contraseña";
                return View();
            }

            var usuario = usuarios.FirstOrDefault(u =>
                u.NombreUsuario == nombreUsuario &&
                u.Contraseña == contraseña);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
            HttpContext.Session.SetString("NombreCompleto", usuario.NombreCompleto);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
