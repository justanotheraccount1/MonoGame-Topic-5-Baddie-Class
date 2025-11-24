using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGame_Topic_5___Baddie_Class
{
    enum Screen
    {
        Title,
        House,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        Rectangle window;
        List<Texture2D> ghostTextures = new List<Texture2D>();
        Texture2D bgTexture, titleTexture, playerTexture, endTexture;
        MouseState mouseState;
        KeyboardState keyboardState;
        Random generator = new Random();
        Ghost ghost1;
        List<Ghost> ghosts = new List<Ghost>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            base.Initialize();
            for (int i = 0; i < 21; i++)
            {
                ghosts.Add(new Ghost(ghostTextures, new Rectangle(generator.Next(window.Width - 40), generator.Next(window.Height - 40), 40, 40)));
            }


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ghostTextures.Add(Content.Load<Texture2D>("Images/boo-stopped"));
            for (int i = 1; i <= 8; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>($"Images/boo-move-{i}"));
            }
            bgTexture = Content.Load<Texture2D>("Images/haunted-background");
            titleTexture = Content.Load<Texture2D>("Images/haunted-title");
            endTexture = Content.Load<Texture2D>("Images/haunted-end-screen");
            playerTexture = Content.Load<Texture2D>("Images/mario");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.House;
                }
            }
            if (screen == Screen.House)
            {
                for (int i = 0; i < ghosts.Count; i++)
                {
                    ghosts[i].Update(gameTime, mouseState);
                    if (ghosts[i].Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Released)
                    {
                        screen = Screen.End;
                    }
                }
                
            }
          
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleTexture, window, Color.Red);
            }
            else if(screen == Screen.House)
            {
                _spriteBatch.Draw(bgTexture, window, Color.White);
                _spriteBatch.Draw(playerTexture, new Rectangle(mouseState.X, mouseState.Y, 30, 30), Color.White);
                for (int i = 0; i < ghosts.Count; i++)
                {
                    ghosts[i].Draw(_spriteBatch);
                }
            }
            else
            {
                _spriteBatch.Draw(endTexture, window, Color.White);
            }





                _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
