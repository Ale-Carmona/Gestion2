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

        #region--index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region--privacy
        public IActionResult Privacy()
        {
            return View();
        }
        #endregion

        #region-- Consultas
        public IActionResult Consultas()
        {
            var activos = _context.Memos
                .Where(m => m.Estatus == "Activo")
                .OrderBy(m => m.Folio)
                .ToList();

            return View(activos);
        }
        #endregion

        #region--Cancelados
        public IActionResult Cancelar()
        {
            var cancelados = _context.Cancelaciones
                .Include(c => c.Memo)   // Para acceder a datos del memo
                .OrderByDescending(c => c.FechaCancelacion)
                .ToList();

            return View(cancelados);
        }
        #endregion

        #region-- create
        // Acción para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region-- - Create Post
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
        #endregion

        #region--details
        public IActionResult Details(int id)
        {
            var memo = _context.Memos
                .Include(m => m.Cancelaciones) // Opcional, si quieres ver cancelaciones
                .FirstOrDefault(m => m.Id == id);

            if (memo == null)
                return NotFound();

            return View(memo);
        }
        #endregion

        #region---- Cancelaciones
        public IActionResult Cancelaciones()
        {
            var cancelados = _context.Cancelaciones
              .Include(c => c.Memo)
              .ToList();

            return View(cancelados);
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
