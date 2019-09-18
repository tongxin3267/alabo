using System;

namespace Alabo.Cache
{
    public interface ICacheContext : IDisposable
    {
        object Instance { get; }
    }
}