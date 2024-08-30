using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;


public class ÖğrenciRepository:Repository<Öğrenci>, IÖğrenci
{
    private readonly ApplicationDbContext _context;

    public ÖğrenciRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(Öğrenci obj)
    {
        _context.Öğrenciler.Update(obj);
    }

 
}