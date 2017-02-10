using System;
using Microsoft.Xna.Framework;

namespace MyMonoGameAddin
{
	public class ScreenManager
	{
		public Vector size = Vector.Zero;
		public GraphicsDeviceManager graphics;
		Game _game;

		public ScreenManager(Game game)
		{
			_game = game;
			graphics = new GraphicsDeviceManager(game);
		}

		public void Initialize(Vector buffersize, bool fullScreen, bool resizeing, bool mouse)
		{
			size = buffersize;
			graphics.PreferredBackBufferWidth = size.X;
			graphics.PreferredBackBufferHeight = size.Y;
			graphics.IsFullScreen = fullScreen;
			graphics.ApplyChanges();

			_game.Window.AllowUserResizing = resizeing;
			if (resizeing)
				_game.Window.ClientSizeChanged += Window_ClientSizeChanged;

			_game.IsMouseVisible = mouse;
		}

		public void Clear(Color color)
		{
			graphics.GraphicsDevice.Clear(color);
		}

		bool WindowSizeIsBeingChanged = false;
		void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			WindowSizeIsBeingChanged = !WindowSizeIsBeingChanged;
			if (WindowSizeIsBeingChanged)
			{
				size.X = _game.Window.ClientBounds.Width;
				size.Y = _game.Window.ClientBounds.Height;
				graphics.PreferredBackBufferWidth = size.X;
				graphics.PreferredBackBufferHeight = size.Y;
				graphics.ApplyChanges();
			}
		}
	}
}
