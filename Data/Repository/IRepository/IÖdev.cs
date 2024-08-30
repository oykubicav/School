using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IÖdev:IRepository<Ödev>
{
    void Update(Models.Ödev obj);
}