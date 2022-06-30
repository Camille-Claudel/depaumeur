using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Depaumer.WifiPositioning
{
    /// <summary>
    /// Gives information on the previous, and current position
    /// </summary>
    public interface IPositionUpdateArgs
    {
        /// <summary>
        /// Returns the last position calculated, can be null if this is the first position update
        /// </summary>
        public Vector2? Previous { get; }
        /// <summary>
        /// Returns the new position
        /// </summary>
        public Vector2 Current { get; }

        /// <summary>
        /// Returns the time at which the last position update happened (before this one)
        /// </summary>
        public DateTime LastUpdateTime { get; }

    }
}