using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;

namespace Racetracks {
    class RaceState : GameObjectList{
        private Car player;
        private GameObjectList bodies; 

        /// <summary>
        /// RaceState constructor which adds the different gameobjects and lists in the correct order of drawing.
        /// </summary>
        public RaceState() {
            //create background
            Add(new SpriteGameObject("road"));

            //create a list for collidable objects
            Add(bodies = new GameObjectList());

            //create user controlled car
            bodies.Add(player = new Car(new Vector2(920, 1400))); //hardcoded position data from 'Tiled'

            //create four opponents
            for (int i = 0; i < 4; i++) {
                float x = 1300 - i * 130; //hardcoded position data from 'Tiled'
                float y = 1400 + i * 50;
                float speed = 6 - i * 0.5f;
                float offset = (i - 1.5f) * 20;
                bodies.Add(new CarNPC(new Vector2(x, y), speed, offset));
            }

            //load scenery
            new LevelLoader().LoadLevel(this);
        }

        /// <summary>
        /// Updates the RaceState.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            //reposition screen to follow the player with 'camera'
            position = GameEnvironment.Screen.ToVector2() * 0.5f - player.Position;
        }
    }
}
