using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IHikayeÖzeti:IRepository<HikayeÖzeti>
{
  void Update(HikayeÖzeti obj);  
}