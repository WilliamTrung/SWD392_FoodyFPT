﻿using ApplicationCore.Models;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IRoleService : IBaseService<DTO.Role, Role>
    {
    }
}
