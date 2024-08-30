using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IDers:IRepository<Ders>
{
    void Update(Ders obj);  
}