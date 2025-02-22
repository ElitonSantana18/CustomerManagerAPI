using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Interface.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email);
    }
}
