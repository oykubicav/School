using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Data.Repository.IRepository;

public interface IHikayeÖzeti:IRepository<HikayeÖzeti>
{
    public void Update(HikayeÖzeti obj);
}