using Gestion2.Data;
using Microsoft.AspNetCore.Mvc;
using Gestion2.Models;

namespace Gestion2.Controllers
{
    public class MemoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar todos los memorándums
        //public IActionResult Index()
        //{
        //    var memos = _context.Memos.ToList(); // Suponiendo que tienes una tabla Memos
        //    return View(memos);
        //}

        //// Acción para mostrar el formulario de creación
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //// Acción para recibir los datos del formulario de creación (POST)
        //[HttpPost]
        //public IActionResult Create(MemoModel memo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Memos.Add(memo);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(memo);
        //}

        //// Acción para mostrar cancelaciones (puedes adaptar según tu lógica)
        //public IActionResult Cancelaciones()
        //{
        //    var cancelados = _context.Memos.Where(m => m.Estatus == "Cancelado").ToList();
        //    return View(cancelados);
        //}
    }
}

