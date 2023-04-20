using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthManager
    {
        public Task<string> GetToken();
        public Task<bool> ValidateUser(string username, string password);
    }
}
