using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;

public class ÖğretmenRepository: Repository<Öğretmen>, IÖğretmen
{
    private readonly ApplicationDbContext _context;

    public ÖğretmenRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Öğretmen obj)
    {
        _context.Öğretmenler.Update(obj);
    }

 
}