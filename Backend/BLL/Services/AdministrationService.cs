using BLL.Contracts;
using DAL.Contracts;

namespace BLL.Services;
public class AdministrationService : IAdministrationService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdministrationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void BackupDatabase(string savePath)
    {
        _unitOfWork.Administration.Value.BackupDatabase(savePath);
    }
}
