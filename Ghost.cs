using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Topic_5___Baddie_Class
{
    public class Ghost
    {
        private List<Texture2D> _textures;
        private Vector2 _speed;
        private Rectangle _location;
        private int _textureIndex;
        private SpriteEffects _direction;

        public Ghost(List<Texture2D> textures, Rectangle location)
        {
            _textures = textures;
            _speed = Vector2.Zero;
            _location = location;
            _textureIndex = 0;
            _direction = SpriteEffects.None;
        }

        public void Update(MouseState mouseState)
        {
            if (mouseState.Position.X > _location.X)
            {
                _direction = SpriteEffects.None;
            }
            else
                _direction = SpriteEffects.FlipHorizontally;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null, Color.White, 0f, Vector2.Zero, _direction, 1f);
        }
    }

}
