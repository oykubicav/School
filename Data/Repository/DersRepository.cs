using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;

public class DersRepository: Repository<Ders>, IDers
{
    private readonly ApplicationDbContext _context;

    public DersRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Ders obj)
    {
        _context.Dersler.Update(obj);
    }

 
}