using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Services
{
    public interface IHashService
    {
        string CreateHash(string @string);
        string CreateHash(string @string, string hashAlgorithm);
    }
}
