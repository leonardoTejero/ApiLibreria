﻿using Common.Utils.Enums;
using Infraestructure.Core.UnitOfWork.Interface;
using MyLibrary.Domain.Services.Interface;
using System.Linq;

namespace MyLibrary.Domain.Services
{
    public class PermissionServices : IPermissionServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public PermissionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public bool ValidatePermissionByUser(Enums.Permission permission, int idUser)
        {
            var result = _unitOfWork.RolUserRepository
                                    .FirstOrDefault(x => x.IdUser == idUser
                                                      && x.RolEntity.RolPermissionEntities
                                                                    .Any(p => p.IdPermission == permission.GetHashCode()));

            return result != null;
        }
        #endregion
    }
}
