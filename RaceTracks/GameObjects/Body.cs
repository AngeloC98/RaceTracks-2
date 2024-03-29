﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class Body : RotatingSpriteGameObject
    {
        protected float radius;
        private Vector2 acceleration = Vector2.Zero;
        private float invMass = 1.0f; //set indirectly by setting 'mass'
        private Vector2 force;
        private float drag = 30;
                
        /// <summary>Creates a physics body</summary>
        public Body(Vector2 position, string assetName) : base(assetName)
        {
            Vector2 size = new Vector2(Width, Height); //circle collider will fit either width or height
            radius = Math.Max(Width, Height) / 2.0f;
            Mass = (radius * radius); //mass approx.
            origin = Center;
            this.position = position;
        }

        /// <summary>Updates this Body</summary>        
        public override void Update(GameTime gameTime)
        {
            velocity += Acceleration;
            Force = Vector2.Zero;

            base.Update(gameTime);
        }

        /// <summary>Returns closest point on this shape</summary>        
        public Vector2 GetClosestPoint(Vector2 point)
        {
            Vector2 delta = point - position;
            if (delta.LengthSquared() > radius * radius)
            {
                delta.Normalize();
                delta *= radius;
            }
            return position + delta;
        }

        /// <summary>Sets the invMass to 0.0</summary>        
        protected void MakeStatic()
        {
            invMass = 0.0f; //making mass infinite
        }

        /// <summary>Get or set the invMass through setting the mass</summary>        
        private float _mass = 1.0f;
        public float Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                _mass = value;
                invMass = 1.0f / value; //note: float div. by zero will give infinite invMass
            }
        }                
        
        /// <summary>Get or set the angle by getting/setting the forward Vec2</summary>
        public Vector2 Forward
        {
            get
            {
                return new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)); //angle to polar
            }

            set
            {
                Angle = (float)Math.Atan2(value.Y, value.X); //polar to angle
            }
        }

        /// <summary>Get or set the angle by getting/setting the right Vec2</summary>
        public Vector2 Right
        {
            get
            {
                return new Vector2((float)-Math.Sin(Angle), (float)Math.Cos(Angle)); //angle to polar
            }

            set
            {
                Angle = (float)Math.Atan2(-value.X, value.Y); //polar to angle
            }
        }

       public Vector2 Force
       {
            get
            {
                return force * Forward;
            }
            set
            {
                force = value;
            }
       }

        public Vector2 Drag
        {
            get
            {
                return -velocity * drag;
            }     
        }

        public Vector2 TotalForce
        {
            get
            {
                return Force + Drag;
            }
        }

        public Vector2 Acceleration
        {
            get
            {
                return TotalForce / Mass;
            }
            
        }
        public void CollisionHandling(Body body)
        {
            Vector2 normal = body.position - position;
            normal.Normalize();

            float distance = Vector2.Distance(position, body.position) - (radius + body.radius);
            Vector2 resizedDifferenceVector = normal * distance; 

            if (distance < 0)
            {
                position += 0.5f * resizedDifferenceVector;
                body.position -= 0.5f * resizedDifferenceVector;
            }

            Vector2 velocityDif = velocity - body.velocity;
            Vector2 parallelComponent = Vector2.Dot(velocityDif, normal) * normal;
            float totalMass = invMass + body.invMass;

            parallelComponent /= totalMass;

            velocity -= parallelComponent * invMass;
            body.velocity += parallelComponent * body.invMass;
        }
    }
}
