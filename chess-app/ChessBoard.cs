using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_app
{
	public class ChessBoard
	{
		private ChessPiece[,] board;

		public ChessBoard()
		{
			board = new ChessPiece[8, 8];
			InitializeBoard();
		}

		private void InitializeBoard()
		{
			//пешки
			for(int i = 0; i < 8; i++)
			{
				board[1, i] = new Pawn(true);
				board[6, i] = new Pawn(false);
			}

			//ладбя
			board[0, 0] = new Rook(true);
			board[0, 7] = new Rook(true);
			board[7, 0] = new Rook(false);
			board[7, 7] = new Rook(false);

			//конь
			board[0, 1] = new Knight(true);
			board[0, 6] = new Knight(true);
			board[7, 1] = new Knight(false);
			board[7, 6] = new Knight(false);

			//слон
			board[0, 2] = new Bishop(true);
			board[0, 5] = new Bishop(true);
			board[7, 2] = new Bishop(false);
			board[7, 5] = new Bishop(false);

			//королева
			board[0, 3] = new Queen(true);
			board[7, 3] = new Queen(false);

			//король
			board[0, 4] = new King(true);
			board[7, 4] = new King(false);
		}

		public bool MovePiece(Position from, Position to, bool isWhiteTurn)
		{
			ChessPiece piece = board[from.Row, from.Col];
			if (piece == null || 
				piece.IsWhite != isWhiteTurn || 
				!piece.IsValidMove(from, to, this))
			{
				return false;
			}

			board[to.Row, to.Col] = piece;
			board[from.Row, from.Col] = null;
			return true;
		}

		public ChessPiece GetPiece(Position position)
		{
			return board[position.Row, position.Col];
		}

		public void PrintBoard()
		{
			Console.WriteLine("  abcdefgh");
			for(int i = 7; i >= 0; i--)
			{
				Console.Write(i + 1 + " ");
				for(int j = 0; j < 8; j++)
				{
					ChessPiece piece = board[i, j];
					Console.Write(piece == null ? "." : piece.GetSymbol());
					Console.Write("");
				}
				Console.WriteLine();
			}
		}
	}
}
