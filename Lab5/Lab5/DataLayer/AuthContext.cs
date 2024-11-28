using Lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab5.DataLayer
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options){ }
        public DbSet<User> users { get; set; }
    }
}
