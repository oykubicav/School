namespace TestIdentityApp.Data.Repository.IRepository;

public interface IUnitOfWork
{
    IÖdev Ödev { get; }
    IYıldızÖğrenci YıldızÖğrenci { get; }
    IHikayeÖzeti HikayeÖzeti { get; }
    IDers Ders { get; }
    IÖğretmen Öğretmen { get; }
    IÖğrenci Öğrenci { get; }
    

    void Save();
}