using Common.Utils.Enums;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IPermissionServices
    {
        bool ValidatePermissionByUser(Enums.Permission permission, int idUser);
    }
}
