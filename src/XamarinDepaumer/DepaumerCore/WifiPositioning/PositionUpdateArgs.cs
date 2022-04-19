using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Depaumer.WifiPositioning
{
    public class PositionUpdateArgs : IPositionUpdateArgs
    {
        public Vector2? Previous { get; }

        public Vector2 Current { get; }

        public DateTime LastUpdateTime { get; }

        public PositionUpdateArgs(Vector2? previous, Vector2 current, DateTime lastUpdateTime)
        {
            Previous = previous;
            Current = current;
            LastUpdateTime = lastUpdateTime;
        }

    }
}
