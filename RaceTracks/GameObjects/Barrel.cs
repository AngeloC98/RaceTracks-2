using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class Barrel : Body
    {
        public Barrel(Vector2 position) : base(position, "barrel_blue")
        {
            Mass = float.MaxValue;
        }
    }
}
