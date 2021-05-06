using System;

namespace Clean.Boundary.Framework.Dependency
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}