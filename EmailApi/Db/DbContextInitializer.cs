using System.Threading.Tasks;

namespace EmailApi.Db
{
    public class DbContextInitializer : IDbContextInitializer
    {
        private readonly EmailContext _context;

        public DbContextInitializer(EmailContext context)
        {
            _context = context;
        }

        public bool EnsureCreated()
        {
            return _context.Database.EnsureCreated();
        }

        public async Task Seed()
        {
            
        }
    }
}