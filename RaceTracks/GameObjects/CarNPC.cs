using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class CarNPC : Body
    {       
        private Waypoints waypoints;
        private float offset;
        private float speed;
        
        /// <summary>Creates a waypoint driven Car</summary>        
        public CarNPC(Vector2 position, float speed, float offset) : base(position, "car2")
        {
            offsetDegrees = -90;
            waypoints = new Waypoints();
            this.offset = offset;
            this.speed = speed;
        }
        
        /// <summary>Updates this Car</summary>        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 target = waypoints.GetTarget(position); //read from 'Tiled' data
            target.Y += offset; //so cars don't follow same track
        }
        
    }
}
