using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Models;

namespace ApiExample.Interfaces
{
    public interface ITokenServices
    {
        public string createToken(User user);
    }
}