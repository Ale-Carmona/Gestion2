using Gestion2.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion2.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<MemoModel> Memos { get; set; }
        public DbSet<CancelacionModel> Cancelaciones { get; set; }

    }
}
