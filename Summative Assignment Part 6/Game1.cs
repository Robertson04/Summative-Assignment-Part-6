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
        Texture2D celebrateTexture;
        Texture2D trophyTexture;
        Rectangle trophyRect;
        Rectangle celebrateRect;
        Texture2D busTexture;
        Rectangle busRect;
        Vector2 busSpead;
        Texture2D scoreTexture;
        SpriteFont titleFont;
        SoundEffect bounce;
        SoundEffectInstance bounceInstance;
        SoundEffect yaa;
        SoundEffect buzzer;
        SoundEffectInstance yaaInstance;
        SoundEffect crazy;
        SoundEffectInstance crazyInstance;
        MouseState mouseState;
        Texture2D ballTexture;
        Vector2 ballSpead;
        Rectangle ballRect;
        Rectangle rectRect;
        Texture2D scorboardTexture;
        Rectangle scorboardRect;
        SpriteFont scoreFont;
        Rectangle rect2Rect;
        Rectangle rect3Rect;
        Rectangle rect4Rect;
        SpriteFont timeFont;
        bool score = false;
        float startTime;
        float seconds;
        bool end;
        //float seconds = 3;
        //bool game = false;
        Screen currtentScreen;
        enum Screen
        {
            Intro,
            Game,
            celebrate,
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
            ballSpead = new Vector2(-2, -1);
            busSpead = new Vector2(-4, 0);
            rectRect = new Rectangle(375, 50, 20, 100);
            rect2Rect = new Rectangle(100, 75, 75, 300);
            rect3Rect = new Rectangle(-100, 300, 75, 300);
            rect4Rect = new Rectangle(-160, 0, 10, 480);
            ballRect = new Rectangle(700, 200, 75, 75);
            trophyRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            scorboardRect = new Rectangle(400, 50, 200, 100);
            busRect = new Rectangle(750, 240, 150, 150);
            celebrateRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            basketballTexture = Content.Load<Texture2D>("basketball");
            bounce = Content.Load<SoundEffect>("bounce");
            buzzer = Content.Load<SoundEffect>("buzzer");
            crazy = Content.Load<SoundEffect>("crazy");
            titleFont = Content.Load<SpriteFont>("title");
            scoreTexture = Content.Load<Texture2D>("court");
            ballTexture = Content.Load<Texture2D>("ball");
            scorboardTexture = Content.Load<Texture2D>("scorebord");
            scoreFont = Content.Load<SpriteFont>("score");
            timeFont = Content.Load<SpriteFont>("time");
            celebrateTexture = Content.Load<Texture2D>("celebrate");
            busTexture = Content.Load<Texture2D>("bus");
            trophyTexture = Content.Load<Texture2D>("trophy");
            yaa = Content.Load<SoundEffect>("yaa");
            bounceInstance = bounce.CreateInstance();
            yaaInstance = yaa.CreateInstance();
            crazyInstance = crazy.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                currtentScreen = Screen.Game;
                seconds = 0;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            else if (currtentScreen == Screen.Intro)
            {
                if (bounceInstance.State == SoundState.Stopped)
                    bounceInstance.Play();
            }
            else if (currtentScreen == Screen.Game)
            {
                ballRect.X += (int)ballSpead.X;
                ballRect.Y += (int)ballSpead.Y;
                bounceInstance.Stop();
            }
            if (rectRect.Intersects(ballRect))
            {
                ballSpead.X = -3;
                ballSpead.Y = 1;
            }
            if (rect2Rect.Intersects(ballRect))
            {
                score = true;
            }
            if (rect2Rect.Contains(ballRect))
                buzzer.Play();
            else if (rect3Rect.Intersects(ballRect))
                currtentScreen = Screen.celebrate;
            if (currtentScreen == Screen.celebrate)
            {
                busRect.X += (int)busSpead.X;
                busRect.Y += (int)busSpead.Y;
                if (yaaInstance.State == SoundState.Stopped)
                    yaaInstance.Play();
            }
            if (rect4Rect.Intersects(busRect))
                end = true;
            if (end == true) 
                currtentScreen = Screen.End;
            if (currtentScreen == Screen.End)
            {
                yaaInstance.Stop();
                if (crazyInstance.State == SoundState.Stopped)
                    crazyInstance.Play();
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
            if (currtentScreen == Screen.Game && score == false)
            {
                _spriteBatch.Draw(scoreTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(scorboardTexture, scorboardRect, Color.White);
                _spriteBatch.DrawString(scoreFont, 89.ToString("00"), new Vector2(408, 73), Color.White);
                _spriteBatch.DrawString(scoreFont, 91.ToString("00"), new Vector2(570, 73), Color.White);
                _spriteBatch.DrawString(timeFont, (4 - seconds).ToString("0  00"), new Vector2(478, 58), Color.White);
                _spriteBatch.Draw(ballTexture, ballRect, Color.White);
            }
            
            if (currtentScreen == Screen.Game && score == true)
            {
                _spriteBatch.Draw(scoreTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(scorboardTexture, scorboardRect, Color.White);
                _spriteBatch.DrawString(scoreFont, 92.ToString("00"), new Vector2(408, 73), Color.White);
                _spriteBatch.DrawString(scoreFont, 91.ToString("00"), new Vector2(570, 73), Color.White);
                _spriteBatch.DrawString(timeFont, 0.00.ToString("0  00"), new Vector2(478, 58), Color.White);
                _spriteBatch.Draw(ballTexture, ballRect, Color.White);
            }
            if (currtentScreen == Screen.celebrate)
            {
                _spriteBatch.Draw(celebrateTexture, celebrateRect, Color.White);
                _spriteBatch.Draw(busTexture, busRect, Color.White);
            }
            if (currtentScreen == Screen.End)
            {
                _spriteBatch.Draw(trophyTexture, trophyRect, Color.White);
                _spriteBatch.DrawString(titleFont, ("Buzzer beater"), new Vector2(100, 25), Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
