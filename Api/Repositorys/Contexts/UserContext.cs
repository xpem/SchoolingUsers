using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.Contexts
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User>? Users { get; set; }
    }
}
