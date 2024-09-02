using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;

public class HikayeÖzetiRepository: Repository<HikayeÖzeti>, IHikayeÖzeti
{
    private readonly ApplicationDbContext _context;

    public HikayeÖzetiRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(HikayeÖzeti obj)
    {
    _context.HikayeÖzetleri.Update(obj);
    }


 
}