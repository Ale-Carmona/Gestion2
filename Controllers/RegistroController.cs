using Gestion2.Data;
using Gestion2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GestionAvanzadas.Controllers
{
    public class RegistroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistroController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Memos.ToList();
            return View(lista);
        }

        public IActionResult Preview(int id)
        {
            // Traer memo con sus cancelaciones
            var memo = _context.Memos
                .Include(m => m.Cancelaciones) // Incluimos las cancelaciones
                .FirstOrDefault(m => m.Id == id);

            if (memo == null)
                return NotFound();

            // Si está cancelado, tomar la última cancelación y pasarla a la vista
            if (memo.Estatus == "Cancelado" && memo.Cancelaciones != null && memo.Cancelaciones.Any())
            {
                var cancelacion = memo.Cancelaciones
                    .OrderByDescending(c => c.FechaCancelacion)
                    .First();

                ViewBag.MotivoCancelacion = cancelacion.MotivoCancelacion;
                ViewBag.UsuarioCancelo = cancelacion.UsuarioCancelo;
                ViewBag.FechaCancelacion = cancelacion.FechaCancelacion;
            }

            return View(memo);
        }

        public IActionResult Cancel(int id)
        {
            var memo = _context.Memos.Find(id);
            if (memo == null) return NotFound();
            return View(memo);
        }

        [HttpPost]
        public IActionResult Cancel(int id, string motivo)
        {
            var memo = _context.Memos.Find(id);
            if (memo == null) return NotFound();

            // Cambiar estatus del memo
            memo.Estatus = "Cancelado";

            // Registrar cancelación en tabla separada
            var cancelacion = new CancelacionModel
            {
                MemoId = memo.Id,
                UsuarioCancelo = HttpContext.Session.GetString("NumEmpleado") ?? "Sistema",
                MotivoCancelacion = motivo,
                FechaCancelacion = DateTime.Now
            };

            _context.Cancelaciones.Add(cancelacion);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var memo = _context.Memos.Find(id);

            if (memo == null)
                return NotFound();

            _context.Remove(memo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}