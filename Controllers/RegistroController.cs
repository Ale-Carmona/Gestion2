using Gestion2.Data;
using Gestion2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;


namespace GestionAvanzadas.Controllers
{
    public class RegistroController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegistroController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region--index
        public IActionResult Index(string buscar)
        {
            var query = _context.Memos.AsQueryable();
            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(m => m.Asunto.Contains(buscar));
            }
            var lista = query.OrderBy(m => m.Folio).ToList();
            ViewBag.Buscar = buscar;
            return View(lista);
        }
        #endregion

        #region--preview
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
        #endregion

        #region--Cancel
        public IActionResult Cancel(int id)
        {
            var memo = _context.Memos.Find(id);
            if (memo == null) return NotFound();
            return View(memo);
        }
        #endregion

        #region-- Cancel Post
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
        #endregion

        #region-- Delete
        public IActionResult Delete(int id)
        {
            var memo = _context.Memos.Find(id);

            if (memo == null)
                return NotFound();

            _context.Remove(memo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region-- Export to Excel
        public IActionResult ExportToExcel()
        {
            var memos = _context.Memos
                .Include(m => m.Cancelaciones)
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Memos");

                // Cabecera
                ws.Cell(1, 1).Value = "Folio";
                ws.Cell(1, 2).Value = "Año";
                ws.Cell(1, 3).Value = "De";
                ws.Cell(1, 4).Value = "Para";
                ws.Cell(1, 5).Value = "Fecha Registro";
                ws.Cell(1, 6).Value = "Asunto";
                ws.Cell(1, 7).Value = "Contenido";
                ws.Cell(1, 8).Value = "Estatus";
                ws.Cell(1, 9).Value = "Usuario Registro";
                ws.Cell(1, 10).Value = "Usuario Canceló";
                ws.Cell(1, 11).Value = "Motivo Cancelación";
                ws.Cell(1, 12).Value = "Fecha Cancelación";

                int row = 2;

                foreach (var memo in memos)
                {
                    ws.Cell(row, 1).Value = memo.Folio;
                    ws.Cell(row, 2).Value = memo.Año;
                    ws.Cell(row, 3).Value = memo.De;
                    ws.Cell(row, 4).Value = memo.Para;
                    ws.Cell(row, 5).Value = memo.FechaRegistro.ToString("dd/MM/yyyy");
                    ws.Cell(row, 6).Value = memo.Asunto;
                    ws.Cell(row, 7).Value = memo.Contenido;
                    ws.Cell(row, 8).Value = memo.Estatus;
                    ws.Cell(row, 9).Value = memo.UsuarioRegistro; // Siempre usuario registro

                    // CANCELACIÓN SOLO SI EXISTE
                    if (memo.Estatus == "Cancelado" && memo.Cancelaciones != null && memo.Cancelaciones.Any())
                    {
                        var cancel = memo.Cancelaciones
                            .OrderByDescending(c => c.FechaCancelacion)
                            .First();

                        ws.Cell(row, 10).Value = cancel.UsuarioCancelo ?? "";
                        ws.Cell(row, 11).Value = cancel.MotivoCancelacion ?? "";
                        ws.Cell(row, 12).Value = cancel.FechaCancelacion.ToString("dd/MM/yyyy HH:mm") ?? "";
                    }
                    else
                    {
                        // Si no está cancelado, deja columnas vacías
                        ws.Cell(row, 10).Value = "";
                        ws.Cell(row, 11).Value = "";
                        ws.Cell(row, 12).Value = "";
                    }

                    row++;
                }

                ws.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var fileName = $"Memos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }
        #endregion
    }
}
