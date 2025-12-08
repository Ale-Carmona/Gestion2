using Gestion2.Data;
using Gestion2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Gestion2.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }


        // Acción para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Acción para recibir los datos del formulario de creación (POST)
        [HttpPost]
        public IActionResult Create(MemoModel memo)
        {
            var ultimo = _context.Memos
              .Where(x => x.Año == DateTime.Now.Year)
              .OrderByDescending(x => x.Folio)
              .FirstOrDefault();

            memo.Folio = ultimo == null ? 1 : ultimo.Folio + 1;
            memo.Año = DateTime.Now.Year;
            memo.FechaRegistro = DateTime.Now;
            memo.Estatus = "Activo";

            memo.UsuarioRegistro = HttpContext.Session.GetString("NumEmpleado") ?? "Sistema";

            _context.Memos.Add(memo);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = memo.Id });
        }

        public IActionResult Details(int id)
        {
            var memo = _context.Memos
                .Include(m => m.Cancelaciones) // Opcional, si quieres ver cancelaciones
                .FirstOrDefault(m => m.Id == id);

            if (memo == null)
                return NotFound();

            return View(memo);
        }


        // Acción para mostrar cancelaciones (puedes adaptar según tu lógica)
        public IActionResult Cancelaciones()
        {
            var cancelados = _context.Cancelaciones
              .Include(c => c.Memo)
              .ToList();

            return View(cancelados);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
