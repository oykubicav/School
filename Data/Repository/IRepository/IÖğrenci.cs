using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IÖğrenci:IRepository<Öğrenci>
{
    void Update(Models.Öğrenci obj);  
}