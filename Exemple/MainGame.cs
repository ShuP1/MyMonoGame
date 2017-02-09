using System;
using MyMonoGameAddin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Exemple
{
	public class MainGame : Game
	{
		enum GameState { Menu, Credit }

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		ResourcesManager resources;
		InputsManager inputs;
		UIManager ui;

		GameState state = GameState.Menu;

		string Github;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);

			Window.AllowUserResizing = true;
			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += delegate
			{
				graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
				graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
				graphics.ApplyChanges();
			};

			Content.RootDirectory = "Content";

			inputs = new InputsManager();
			resources = new ResourcesManager(Content, GraphicsDevice);
		}

		protected override void Initialize()
		{
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
			graphics.IsFullScreen = false;
			graphics.ApplyChanges();

			IsMouseVisible = true;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			resources.LoadFont("Normal");
			resources.LoadFont("Title");
			//resources.LoadSound("bip");
			resources.LoadTexture("Background");
			resources.LoadBox("Button");
			ui = new UIManager(spriteBatch, inputs, new Skin(resources.Boxes["Button"], resources.Fonts["Normal"], new Colors(Color.White, Color.LightGray, Color.DarkGray), new Colors(Color.Black, Color.Black, Color.White)));
		}

		protected override void Update(GameTime gameTime)
		{
			inputs.Update();
			ui.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.DarkGray);

			spriteBatch.Begin();
			ui.Texture(new Rectangle(10, 10, 200, 150), resources.Textures["Background"]);
			switch (state)
			{
				case GameState.Credit:
					ui.Box(new Rectangle(200, 100, graphics.PreferredBackBufferWidth - 400, graphics.PreferredBackBufferWidth - 200), new Colors(Color.LightGray, Color.White));
					ui.Label(new Vector(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 4), "By Sheychen", new Colors(Color.Red, Color.OrangeRed), null, UIManager.textAlign.centerCenter);
					if (ui.Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 100, 200, 40), "My website"))
					{
						Process.Start("https://sheychen.shost.ca");
					}
					if (ui.Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2 - 50, 200, 40), "Show on GitHub"))
					{
						Process.Start("https://github.com/sheychen290/MyMonoGame");
					}
					if (ui.Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight / 2, 200, 40), "Back"))
					{
						state = GameState.Menu;
					}
					break;

				case GameState.Menu:
					ui.Label(new Vector(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 4), "MyMonoGameAddin", new Colors(Color.Black, Color.Green), resources.Fonts["Title"], UIManager.textAlign.centerCenter);
					if (ui.TextField(new Vector(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), ref Github, new Colors(Color.White, Color.WhiteSmoke, Color.LightGray), null, UIManager.textAlign.centerCenter, "Search on Github"))
					{
						Process.Start("https://github.com/search?q=" + Github);
						Github = null;
					}
					if (ui.Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight * 3 / 4 + 50, 200, 40), "About", null, new Colors(Color.Black, Color.Green)))
					{
						state = GameState.Credit;
					}
					break;
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			Process.GetCurrentProcess().Kill();
			base.OnExiting(sender, args);
		}
	}
}
