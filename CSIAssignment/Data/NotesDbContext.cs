using Microsoft.EntityFrameworkCore;
using CSIAssignment.Models;

namespace CSIAssignment.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}
