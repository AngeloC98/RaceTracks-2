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
        private float carForce = 10000;
        private float turnSpeed = 0.3f;

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

            Vector2 wpDirection = target - position;
            wpDirection.Normalize();

            float direction = Vector2.Dot(Forward, wpDirection);
            Forward = Vector2.SmoothStep(Forward, wpDirection, turnSpeed);

            Force = new Vector2(carForce);
        }
        
    }
}
