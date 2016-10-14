using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.GUI;

namespace Exemple
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;

        SpriteFont basicFont;
        boxSprites boxSprite;

        private int ScreenWidth = 1080;
        private int ScreenHeight = 720;

        Manager GUI = new Manager();
        private string Github = null;
        private bool showAbout = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            content = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GUI.Initialise();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            basicFont = content.Load<SpriteFont>("basic");

            boxSprite.topLeft = content.Load<Texture2D>("Textures/topLeft");
            boxSprite.topCenter = content.Load<Texture2D>("Textures/topCenter");
            boxSprite.topRight = content.Load<Texture2D>("Textures/topRight");
            boxSprite.centerLeft = content.Load<Texture2D>("Textures/centerLeft");
            boxSprite.centerCenter = content.Load<Texture2D>("Textures/centerCenter");
            boxSprite.centerRight = content.Load<Texture2D>("Textures/centerRight");
            boxSprite.bottomLeft = content.Load<Texture2D>("Textures/bottomLeft");
            boxSprite.bottomCenter = content.Load<Texture2D>("Textures/bottomCenter");
            boxSprite.bottomRight = content.Load<Texture2D>("Textures/bottomRight");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            GUI.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            GUI.Draw(spriteBatch);

            if (!showAbout)
            {
                GUI.Label(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 4), "MyMonoGame", basicFont, new MyMonoGame.Colors(Color.Black, Color.Green), Manager.textAlign.centerCenter);
                if(GUI.TextField(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 2), ref Github, basicFont, new MyMonoGame.Colors(Color.White, Color.WhiteSmoke, Color.LightGray), Manager.textAlign.centerCenter, "Search on Github"))
                {
                    System.Diagnostics.Process.Start("https://github.com/search?q=" + Github);
                    Github = null;
                }
                showAbout = GUI.ButtonBoxLabel(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight * 3 / 4 + 50, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "About", basicFont, new MyMonoGame.Colors(Color.Black, Color.Green));
            }
            else
            {
                GUI.Box(new Rectangle(200, 100, ScreenWidth - 400, ScreenHeight - 200), boxSprite, new MyMonoGame.Colors(Color.LightGray, Color.White));
                GUI.Label(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 4), "By Sheychen", basicFont, new MyMonoGame.Colors(Color.Red, Color.OrangeRed),Manager.textAlign.centerCenter);
                if(GUI.ButtonBoxLabel(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight / 2 - 100, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "My website", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White)))
                {
                    System.Diagnostics.Process.Start("https://sheychen.shost.ca");
                }
                if(GUI.ButtonBoxLabel(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight / 2 - 50, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "Show on GitHub", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White)))
                {
                    System.Diagnostics.Process.Start("https://github.com/sheychen290/MyMonoGame");
                }
                showAbout = !GUI.ButtonBoxLabel(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight / 2, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "Back", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White));
            }

            spriteBatch.DrawString(basicFont, "\\", new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
