using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_app
{
	public class Position
	{
		public int Row { get; private set; }
		public int Col { get; private set; }

		public Position(int row, int col)
		{
			Row = row;
			Col = col;
		}

		public static Position FromChessNotation(string chessNotation)
		{
			if (chessNotation.Length != 2)
			{
				throw new ArgumentException("Неверная шахматная позиция");
			}

			int col = chessNotation[0] - 'a';
			int row = chessNotation[1] - '1';

			if (col < 0 || col > 7 || row < 0 || row > 7)
			{
				throw new ArgumentOutOfRangeException("Позиция вне доски");
			}

			return new Position(row, col);
		}
	}
}
