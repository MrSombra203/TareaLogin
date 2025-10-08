using Microsoft.AspNetCore.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class AuthController : Controller
    {
        
        public static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario
            {
                Id = 1,
                NombreUsuario = "admin",
                Contraseña = "123456",
                NombreCompleto = "Administrador del Sistema",
                Email = "admin@sistema.com",
                Rol = "Administrador",
                FechaRegistro = DateTime.Now.AddMonths(-6),
                Activo = true
            },
            new Usuario
            {
                Id = 2,
                NombreUsuario = "usuario",
                Contraseña = "pass123",
                NombreCompleto = "Usuario Normal",
                Email = "usuario@sistema.com",
                Rol = "Usuario",
                FechaRegistro = DateTime.Now.AddMonths(-3),
                Activo = true
            },
            new Usuario
            {
                Id = 3,
                NombreUsuario = "maria",
                Contraseña = "maria123",
                NombreCompleto = "María García",
                Email = "maria@sistema.com",
                Rol = "Usuario",
                FechaRegistro = DateTime.Now.AddMonths(-1),
                Activo = true
            }
        };

        public static int siguienteId = 4;

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
