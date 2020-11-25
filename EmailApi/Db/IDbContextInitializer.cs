using System.Threading.Tasks;

namespace EmailApi.Db
{
    public interface IDbContextInitializer
    {
        bool EnsureCreated();
        Task Seed();   
    }
}