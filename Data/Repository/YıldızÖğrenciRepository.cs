using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;

public class YıldızÖğrenciRepository:Repository<YıldızÖğrenci>, IYıldızÖğrenci    
{
    private readonly ApplicationDbContext _context;

    public YıldızÖğrenciRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(TestIdentityApp.Models.YıldızÖğrenci obj)
    {
        _context.YıldızÖğrenciler.Update(obj);
    }

 
}