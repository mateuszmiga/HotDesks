using HotDesks.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDesks.Api.Services
{
    public interface IAuthManager
    {
        public Task<string> GetToken();
        public Task<bool> ValidateUser(LoginUserDto userDto);
    }
}
