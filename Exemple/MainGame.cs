using System;
using MyMonoGameAddin;
using MyMonoGameAddin.UI;
using MyMonoGameAddin.Inputs;
using Microsoft.Xna.Framework;
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
		ResourcesManager resources;
		InputsManager inputs = new InputsManager();

		Layout canvas = new Layout();
		Image pointer;

		const int ScreenWidth = 800;
		const int ScreenHeight = 600;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = ScreenWidth;
			graphics.PreferredBackBufferHeight = ScreenHeight;
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
			resources = new ResourcesManager(Content, GraphicsDevice);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
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

			//resources.LoadFont("basic");
			//resources.LoadSound("bip");
			resources.LoadTexture("Background");
			resources.LoadBox("Button");


			BuildCanvas();
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			inputs.Update();
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			pointer.Transform.Position = inputs.Mouse.Position;

			graphics.GraphicsDevice.Clear(Color.Black);

			//TODO: Add your drawing code here
			spriteBatch.Begin();

			canvas.Draw(spriteBatch, new Transform());

			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void BuildCanvas()
		{
			canvas.Children.Add(new Box(resources.Boxes["Button"], new Vector(10, 10), new Vector(100, 20), Color.White));
			pointer = new Image(resources.Textures["Background"], new Vector(0, 0), new Vector(10, 10), Color.White);
			canvas.Children.Add(pointer);
		}
	}
}
