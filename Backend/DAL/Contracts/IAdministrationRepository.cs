namespace DAL.Contracts;
public interface IAdministrationRepository
{
    void BackupDatabase(string savePath);
}
