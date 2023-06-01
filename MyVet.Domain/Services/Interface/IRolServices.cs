using Infraestructure.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Domain.Services.Interface
{
    public interface IRolServices
    {
        List<RolEntity> GetAll();
    }
}
