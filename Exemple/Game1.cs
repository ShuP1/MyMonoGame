using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        MyMonoGame.GUI.boxSprites boxSprite;

        private MyMonoGame.GUI.ElementLink Home;
        private MyMonoGame.GUI.ElementLink About;

        private MyMonoGame.GUI.ElementLink Github;

        private int ScreenWidth = 1080;
        private int ScreenHeight = 720;

        MyMonoGame.GUI.Manager GuiManager = new MyMonoGame.GUI.Manager();

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
            GuiManager.Initialise();
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

            // TODO: add Elements there or in Update and Buttons Handler

            Home = GuiManager.AddElement(new MyMonoGame.GUI.Element());
            GuiManager.AddElement(new MyMonoGame.GUI.Label(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 4), "MyMonoGame", basicFont, new MyMonoGame.Colors(Color.Black, Color.Green), MyMonoGame.GUI.Label.textAlign.centerCenter, true, Home));
            Github = GuiManager.AddElement(new MyMonoGame.GUI.TextField(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 2), null, basicFont, new MyMonoGame.Colors(Color.White, Color.WhiteSmoke, Color.LightGray), MyMonoGame.GUI.Label.textAlign.centerCenter, "Search on Github", SearchGithub, true, Home));
            GuiManager.AddElement(new MyMonoGame.GUI.BoxLabelButton(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight * 3 / 4 + 50, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "About", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White), MyMonoGame.GUI.Label.textAlign.centerCenter, GoAbout, true, Home));

            About = GuiManager.AddElement(new MyMonoGame.GUI.Box(new Rectangle(200, 100, ScreenWidth - 400, ScreenHeight - 200), boxSprite, new MyMonoGame.Colors(Color.LightGray, Color.White), false));
            GuiManager.AddElement(new MyMonoGame.GUI.Label(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 4), "By Sheychen", basicFont, new MyMonoGame.Colors(Color.Red, Color.OrangeRed), MyMonoGame.GUI.Label.textAlign.centerCenter, true, About));
            GuiManager.AddElement(new MyMonoGame.GUI.BoxLabelButton(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight  /2 - 100, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "My Website", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White), MyMonoGame.GUI.Label.textAlign.centerCenter, OpenWebsite, true, About));
            GuiManager.AddElement(new MyMonoGame.GUI.BoxLabelButton(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight  /2 - 50, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "Show on GitHub", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White), MyMonoGame.GUI.Label.textAlign.centerCenter, OpenGithub, true, About));
            GuiManager.AddElement(new MyMonoGame.GUI.BoxLabelButton(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight  /2, 200, 40), boxSprite, new MyMonoGame.Colors(Color.White, Color.LightGray, Color.DarkGray), "Back", basicFont, new MyMonoGame.Colors(Color.Black, Color.Black, Color.White), MyMonoGame.GUI.Label.textAlign.centerCenter, Back, true, About));
        }

        private void SearchGithub(object sender, EventArgs e){ System.Diagnostics.Process.Start("https://github.com/search?q=" + (GuiManager.GetElement(Github) as MyMonoGame.GUI.TextField).output); }

        private void OpenWebsite(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://sheychen.shost.ca"); }

        private void OpenGithub(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://github.com/sheychen290/MyMonoGame"); }

        private void GoAbout(object sender, EventArgs e)
        {
            GuiManager.SetEnable(Home, false);
            GuiManager.SetEnable(About, true);
        }

        private void Back(object sender, EventArgs e)
        {
            GuiManager.SetEnable(Home, true);
            GuiManager.SetEnable(About, false);
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

            GuiManager.Update();

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

            GuiManager.Draw(spriteBatch);

            spriteBatch.DrawString(basicFont, "\\", new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
