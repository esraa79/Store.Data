﻿using Store.Data.Entities.IdentityEntities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}
