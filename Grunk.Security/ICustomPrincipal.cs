using System;
namespace Grunk.Security
{
    public interface ICustomPrincipal
    {
        ICustomIdentity Identity { get; }
    }
}
