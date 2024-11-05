using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Biblioteca.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<CreateBookModel> Books { get; set; }
        public DbSet<RegisterModel> Registers { get; set; }
        public DbSet<LoginModel> Users { get; set; }
    }
}
