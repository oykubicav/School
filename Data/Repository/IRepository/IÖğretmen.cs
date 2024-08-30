using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IÖğretmen : IRepository<Öğretmen>
{
    void Update(Models.Öğretmen öğretmen);
}