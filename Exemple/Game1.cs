using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.GUI; //Import MyMonoGame

namespace Exemple
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ContentManager content;

        private int ScreenWidth = 1080;
        private int ScreenHeight = 720;

        private Manager GUI = new Manager(); //Gui manager

        //InGame Values
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

            GUI.content.Initialise(content, GraphicsDevice);

            GUI.content.AddFont("basic"); //Load "Fonts/basic"

            GUI.content.AddBox("Box", "0"); //Load all files in "Textures/0/"
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || GUI.keyboard.IsPressed(Keys.Escape))
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
                GUI.Label(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 4), "MyMonoGame", GUI.content.GetFont("basic"), new MyMonoGame.Colors(Color.Black, Color.Green), Manager.textAlign.centerCenter);
                if (GUI.TextField(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 2), ref Github, GUI.content.GetFont("basic"), new MyMonoGame.Colors(Color.White, Color.WhiteSmoke, Color.LightGray), Manager.textAlign.centerCenter, "Search on Github"))
                {
                    System.Diagnostics.Process.Start("https://github.com/search?q=" + Github);
                    Github = null;
                }
                showAbout = GUI.Button(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight * 3 / 4 + 50, 200, 40), GUI.content.GetBox("Box"), "About", GUI.content.GetFont("basic"), null, new MyMonoGame.Colors(Color.Black, Color.Green));
            }
            else
            {
                GUI.Box(new Rectangle(200, 100, ScreenWidth - 400, ScreenHeight - 200), GUI.content.GetBox("Box"), new MyMonoGame.Colors(Color.LightGray, Color.White));
                GUI.Label(new MyMonoGame.Vector(ScreenWidth / 2, ScreenHeight / 4), "By Sheychen", GUI.content.GetFont("basic"), new MyMonoGame.Colors(Color.Red, Color.OrangeRed), Manager.textAlign.centerCenter);
                if (GUI.Button(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight / 2 - 100, 200, 40), GUI.content.GetBox("Box"), "My website", GUI.content.GetFont("basic")))
                {
                    System.Diagnostics.Process.Start("https://sheychen.shost.ca");
                }
                if (GUI.Button(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight / 2 - 50, 200, 40), GUI.content.GetBox("Box"), "Show on GitHub", GUI.content.GetFont("basic")))
                {
                    System.Diagnostics.Process.Start("https://github.com/sheychen290/MyMonoGame");
                }
                showAbout = !GUI.Button(new Rectangle(ScreenWidth / 2 - 100, ScreenHeight / 2, 200, 40), GUI.content.GetBox("Box"), "Back", GUI.content.GetFont("basic"));
            }

            spriteBatch.DrawString(GUI.content.GetFont("basic"), "\\", new Vector2(GUI.mouse.X, GUI.mouse.Y), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}