using TestIdentityApp.Data.Repository.IRepository;

namespace TestIdentityApp.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IÖdev Ödev { get; private set; }
    public IYıldızÖğrenci YıldızÖğrenci { get; private set; }
    public IHikayeÖzeti HikayeÖzeti { get; private set; }
    public IDers Ders { get; private set; }
    public IÖğretmen Öğretmen { get; private set; }
    public IÖğrenci Öğrenci { get; private set; }
    

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Ödev = new ÖdevRepository(_context);
        YıldızÖğrenci = new YıldızÖğrenciRepository(_context);
        HikayeÖzeti = new HikayeÖzetiRepository(_context);
        Ders = new DersRepository(_context);
        Öğretmen = new ÖğretmenRepository(_context);
        Öğrenci = new ÖğrenciRepository(_context);

    }

    public void Save()
    {
        _context.SaveChanges();
    }
}