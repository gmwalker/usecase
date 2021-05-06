using System;
using Clean.Boundary.Framework.Dependency;

namespace Dependency.Framework
{
    public class UtcClock : IClock
    {
        public DateTime Now
            => DateTime.UtcNow;
    }
}