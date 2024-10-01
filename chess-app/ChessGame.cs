using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_app
{
	public class ChessGame
	{
		private ChessBoard board;
		private bool isWhiteTurn;

		public ChessGame()
		{
			board = new ChessBoard();
			isWhiteTurn = true;
		}

		public void Start()
		{
			while (true)
			{
				Console.Clear();
				board.PrintBoard();
				Console.WriteLine(isWhiteTurn ? "Ход Белых" : "Ход Черных");

				Console.Write("Введите ход (пример: a2 a3): ");
				string move = Console.ReadLine();
				string[] parts = move.Split(' ');

				if(parts.Length != 2)
				{
					Console.WriteLine("Неверный формат хода!");
					continue;
				}

				Position from = Position.FromChessNotation(parts[0]);
				Position to = Position.FromChessNotation(parts[1]);

				if(from == null || to == null)
				{
					Console.WriteLine("Неверные позиции!");
					continue;
				}

				if(!board.MovePiece(from, to, isWhiteTurn))
				{
					Console.WriteLine("Неверный ход!");
					continue;
				}

				isWhiteTurn = !isWhiteTurn;
			}
		}
	}
}
