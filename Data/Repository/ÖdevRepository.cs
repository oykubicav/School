using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;

public class ÖdevRepository:Repository<Ödev>, IÖdev
{
    private readonly ApplicationDbContext _context;

    public ÖdevRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Models.Ödev obj)
    {
        _context.Ödevler.Update(obj);
    }

 
}