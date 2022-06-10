using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Summative_Assignment_Part_6
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D basketballTexture;
        Texture2D scoreTexture;
        SpriteFont titleFont;
        SoundEffect bounce;
        MouseState mouseState;
        Texture2D ballTexture;
        Vector2 ballSpead;
        Rectangle ballRect;
        bool playedBounce = false;
        //float seconds = 3;
        //bool game = false;
        Screen currtentScreen;
        enum Screen
        {
            Intro,
            Game,
            End,
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currtentScreen = Screen.Intro;
            base.Initialize();
            ballSpead = new Vector2(-2, 0);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            basketballTexture = Content.Load<Texture2D>("basketball");
            bounce = Content.Load<SoundEffect>("bounce");
            titleFont = Content.Load<SpriteFont>("title");
            scoreTexture = Content.Load<Texture2D>("score1");
            ballTexture = Content.Load<Texture2D>("ball");
            ballRect = new Rectangle(600, 75, 75, 75);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (mouseState.LeftButton == ButtonState.Pressed)
                currtentScreen = Screen.Game;
            if (currtentScreen == Screen.Intro && !playedBounce)
            {
                bounce.Play();
                playedBounce = true;
            }
            if (currtentScreen == Screen.Game)
            {
                ballRect.X += (int)ballSpead.X;
                ballRect.Y += (int)ballSpead.Y;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (currtentScreen == Screen.Intro)
            {
                _spriteBatch.Draw(basketballTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(titleFont, ("Click to go to the next screen"), new Vector2(100, 25), Color.Black);
            }
            if (currtentScreen == Screen.Game)
            {
                _spriteBatch.Draw(scoreTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(ballTexture, ballRect, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
