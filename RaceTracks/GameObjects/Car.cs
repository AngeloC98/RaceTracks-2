using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class Car : Body
    {
        private float carForce = 25000;
        private float turnSpeed = 0.04f;
        private float maxSpeed = 300;


        /// <summary>Creates a user controlled Car</summary>        
        public Car(Vector2 position) : base(position, "car")
        {
            offsetDegrees = -90;
        }

        /// <summary>Updates this Car</summary>        
        public override void Update(GameTime gameTime)
        {
            if (velocity.Length() > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }
            base.Update(gameTime);
        }

        /// <summary>Handle user input for this Car</summary>        
        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.IsKeyDown(Keys.A))
            {
                Angle -= turnSpeed + velocity.Length() / carForce;
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                Angle += turnSpeed + velocity.Length() / carForce;
            }
            if (inputHelper.IsKeyDown(Keys.W))
            {
                Force = new Vector2(carForce);
            }
            if (inputHelper.IsKeyDown(Keys.S))
            {
                Force = new Vector2(-carForce);
            }
            base.HandleInput(inputHelper);
        }

    }
}
