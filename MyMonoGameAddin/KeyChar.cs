using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MyMonoGameAddin
{
	public static class KeyChar
	{
		public enum Layout { QWERTY, AZERTY }

		static readonly Dictionary<Keys, CharPair>[] Data = new Dictionary<Keys, CharPair>[2]
		{
			new Dictionary<Keys, CharPair>() //QWERTY
			{
				{Keys.OemTilde, new CharPair('`','~')},
				{Keys.D1, new CharPair('1','!')},
				{Keys.D2, new CharPair('2','@')},
				{Keys.D3, new CharPair('3','#')},
				{Keys.D4, new CharPair('4','$')},
				{Keys.D5, new CharPair('5','%')},
				{Keys.D6, new CharPair('6','^')},
				{Keys.D7, new CharPair('7','&')},
				{Keys.D8, new CharPair('8','*')},
				{Keys.D9, new CharPair('9','(')},
				{Keys.D0, new CharPair('0',')')},
				{Keys.OemMinus, new CharPair('-','_')},
				{Keys.OemPlus, new CharPair('=','+')},

				{Keys.Q, new CharPair('q','Q')},
				{Keys.W, new CharPair('w','W')},
				{Keys.E, new CharPair('e','E')},
				{Keys.R, new CharPair('r','R')},
				{Keys.T, new CharPair('t','T')},
				{Keys.Y, new CharPair('y','Y')},
				{Keys.U, new CharPair('u','U')},
				{Keys.I, new CharPair('i','i')},
				{Keys.O, new CharPair('o','O')},
				{Keys.P, new CharPair('p','P')},
				{Keys.OemOpenBrackets, new CharPair('[','{')},
				{Keys.OemCloseBrackets, new CharPair(']','}')},
				{Keys.OemPipe, new CharPair('\\','|')},

				{Keys.A, new CharPair('a','A')},
				{Keys.S, new CharPair('s','S')},
				{Keys.D, new CharPair('d','D')},
				{Keys.F, new CharPair('f','F')},
				{Keys.G, new CharPair('g','G')},
				{Keys.H, new CharPair('h','H')},
				{Keys.J, new CharPair('j','J')},
				{Keys.K, new CharPair('k','K')},
				{Keys.L, new CharPair('l','L')},
				{Keys.OemSemicolon, new CharPair(';',':')},
				{Keys.OemQuotes, new CharPair('\'','"')},

				{Keys.Z, new CharPair('z','Z')},
				{Keys.X, new CharPair('x','X')},
				{Keys.C, new CharPair('c','C')},
				{Keys.V, new CharPair('v','V')},
				{Keys.B, new CharPair('b','B')},
				{Keys.N, new CharPair('n','N')},
				{Keys.M, new CharPair('m','M')},
				{Keys.OemComma, new CharPair(',','<')},
				{Keys.OemPeriod, new CharPair('.','>')},
				{Keys.OemQuestion, new CharPair('/','?')},

				{Keys.NumPad1, new CharPair('1')},
				{Keys.NumPad2, new CharPair('2')},
				{Keys.NumPad3, new CharPair('3')},
				{Keys.NumPad4, new CharPair('4')},
				{Keys.NumPad5, new CharPair('5')},
				{Keys.NumPad6, new CharPair('6')},
				{Keys.NumPad7, new CharPair('7')},
				{Keys.NumPad8, new CharPair('8')},
				{Keys.NumPad9, new CharPair('9')},
				{Keys.NumPad0, new CharPair('0')},
				{Keys.Add, new CharPair('+')},
				{Keys.Divide, new CharPair('/')},
				{Keys.Multiply, new CharPair('*')},
				{Keys.Subtract, new CharPair('-')},
				{Keys.Decimal, new CharPair('.')},

				{Keys.Space, new CharPair(' ')}
			},

			new Dictionary<Keys, CharPair>() //AZERTY
			{
				{Keys.OemTilde, new CharPair('²')},
				{Keys.D1, new CharPair('&','1')},
				{Keys.D2, new CharPair('é','2','~')},
				{Keys.D3, new CharPair('"','3','#')},
				{Keys.D4, new CharPair('\'','4','\'')},
				{Keys.D5, new CharPair('(','5','[')},
				{Keys.D6, new CharPair('-','6','|')},
				{Keys.D7, new CharPair('è','7','`')},
				{Keys.D8, new CharPair('_','8','\\')},
				{Keys.D9, new CharPair('ç','9','^')},
				{Keys.D0, new CharPair('à','0','@')},
				{Keys.OemMinus, new CharPair(')','°',']')},
				{Keys.OemPlus, new CharPair('=','+','}')},

				{Keys.Q, new CharPair('a','A')},
				{Keys.W, new CharPair('z','Z')},
				{Keys.E, new CharPair('e','E')},
				{Keys.R, new CharPair('r','R')},
				{Keys.T, new CharPair('t','T')},
				{Keys.Y, new CharPair('y','Y')},
				{Keys.U, new CharPair('u','U')},
				{Keys.I, new CharPair('i','i')},
				{Keys.O, new CharPair('o','O')},
				{Keys.P, new CharPair('p','P')},
				{Keys.OemOpenBrackets, new CharPair('^','"')},
				{Keys.OemCloseBrackets, new CharPair('$','£','ø')},

				{Keys.A, new CharPair('q','Q')},
				{Keys.S, new CharPair('s','S')},
				{Keys.D, new CharPair('d','D')},
				{Keys.F, new CharPair('f','F')},
				{Keys.G, new CharPair('g','G')},
				{Keys.H, new CharPair('h','H')},
				{Keys.J, new CharPair('j','J')},
				{Keys.K, new CharPair('k','K')},
				{Keys.L, new CharPair('l','L')},
				{Keys.OemSemicolon, new CharPair('m','M')},
				{Keys.OemQuotes, new CharPair('ù','%')},
				{Keys.OemPipe, new CharPair('*','µ')},

				{Keys.Z, new CharPair('w','W')},
				{Keys.X, new CharPair('x','X')},
				{Keys.C, new CharPair('c','C')},
				{Keys.V, new CharPair('v','V')},
				{Keys.B, new CharPair('b','B')},
				{Keys.N, new CharPair('n','N')},
				{Keys.M, new CharPair(',','?')},
				{Keys.OemComma, new CharPair(';','.')},
				{Keys.OemPeriod, new CharPair(':','/')},
				{Keys.OemQuestion, new CharPair('!','§')},

				{Keys.NumPad1, new CharPair('1')},
				{Keys.NumPad2, new CharPair('2')},
				{Keys.NumPad3, new CharPair('3')},
				{Keys.NumPad4, new CharPair('4')},
				{Keys.NumPad5, new CharPair('5')},
				{Keys.NumPad6, new CharPair('6')},
				{Keys.NumPad7, new CharPair('7')},
				{Keys.NumPad8, new CharPair('8')},
				{Keys.NumPad9, new CharPair('9')},
				{Keys.NumPad0, new CharPair('0')},
				{Keys.Add, new CharPair('+')},
				{Keys.Divide, new CharPair('/')},
				{Keys.Multiply, new CharPair('*')},
				{Keys.Subtract, new CharPair('-')},
				{Keys.Decimal, new CharPair('.')},

				{Keys.Space, new CharPair(' ')}
			}
		};

		class CharPair
		{
			public CharPair(char normalChar, Nullable<char> shiftChar = null, Nullable<char> altChar = null)
			{
				NormalChar = normalChar;
				ShiftChar = shiftChar;
				AltChar = altChar;
			}

			public char NormalChar;
			public Nullable<char> ShiftChar;
			public Nullable<char> AltChar;
		}

		public static bool KeyToChar(Keys key, bool shitKeyPressed, out Nullable<char> character, Layout layout = Layout.QWERTY, bool altKeyPressed = false)
		{
			bool result = false;
			character = null;
			CharPair charPair;

			if (Data[(int)layout].TryGetValue(key, out charPair))
			{
				if (shitKeyPressed)
				{
					if (!altKeyPressed)
					{
						if (charPair.ShiftChar.HasValue)
						{
							character = charPair.ShiftChar.Value;
							result = true;
						}
					}
				}
				else
				{
					if (altKeyPressed)
					{
						if (charPair.AltChar.HasValue)
						{
							character = charPair.AltChar.Value;
							result = true;
						}
					}
					else
					{
						character = charPair.NormalChar;
						result = true;
					}
				}
			}

			return result;
		}
	}
}
