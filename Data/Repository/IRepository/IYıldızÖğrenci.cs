using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IYıldızÖğrenci: IRepository<YıldızÖğrenci> 
{
    void Update(TestIdentityApp.Models.YıldızÖğrenci obj);
}