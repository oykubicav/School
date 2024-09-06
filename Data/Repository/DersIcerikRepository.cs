using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data;
using System.Linq.Expressions;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Repository;

public class DersIcerikRepository: Repository<DersIcerik>, IDersIcerik
{
    private readonly ApplicationDbContext _context;

    public DersIcerikRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(DersIcerik obj)
    {
        _context.DersIcerikler.Update(obj);
    }

 
}