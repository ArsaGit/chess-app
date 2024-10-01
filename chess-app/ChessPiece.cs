using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_app
{
	public abstract class ChessPiece
	{
		public string Name { get; }
		public bool IsWhite { get; private set; }

		public ChessPiece(string name, bool isWhite)
		{
			Name = name;
			IsWhite = isWhite;
		}

		public abstract bool IsValidMove(Position from, Position to, ChessBoard board);
		public abstract char GetSymbol();
	}

	public class King : ChessPiece
	{
		public King(bool isWhite) : base("King", isWhite) { }

		public override bool IsValidMove(Position from, Position to, ChessBoard board)
		{
			int dRow = Math.Abs(from.Row - to.Row);
			int dCol = Math.Abs(from.Col - to.Col);
			return dRow <= 1 && dCol <= 1;
		}

		public override char GetSymbol()
		{
			return IsWhite ? 'K' : 'k';
		}
	}

	public class Queen : ChessPiece
	{
		public Queen(bool isWhite) : base("Queen", isWhite) { }

		public override bool IsValidMove(Position from, Position to, ChessBoard board)
		{
			return new Rook(IsWhite).IsValidMove(from, to, board) || new Bishop(IsWhite).IsValidMove(from, to, board);
		}

		public override char GetSymbol()
		{
			return IsWhite ? 'Q' : 'q';
		}
	}

	public class Rook : ChessPiece
	{
		public Rook(bool isWhite) : base("Rook", isWhite) { }

		public override bool IsValidMove(Position from, Position to, ChessBoard board)
		{
			//проверка на вертикальное или горизантальное передвижение
			if(from.Row == to.Row || from.Col == to.Col)
			{
				return true;
			}

			return false;
		}

		public override char GetSymbol()
		{
			return IsWhite ? 'R' : 'r';
		}
	}

	public class Bishop : ChessPiece
	{
		public Bishop(bool isWhite) : base("Bishop", isWhite) { }

		public override bool IsValidMove(Position from, Position to, ChessBoard board)
		{
			return Math.Abs(from.Row - to.Row) == Math.Abs(from.Col - to.Col);
		}

		public override char GetSymbol()
		{
			return IsWhite ? 'B' : 'b';
		}
	}

	public class Knight : ChessPiece
	{
		public Knight(bool isWhite) : base("Knight", isWhite) { }

		public override bool IsValidMove(Position from, Position to, ChessBoard board)
		{
			int dRow = Math.Abs(from.Row - to.Row);
			int dCol = Math.Abs(from.Col - to.Col);
			return (dRow == 2 && dCol == 1) || (dRow == 1 && dCol == 2);
		}

		public override char GetSymbol()
		{
			return IsWhite ? 'N' : 'n';
		}
	}

	public class Pawn : ChessPiece
	{
		public Pawn(bool isWhite) : base("Pawn", isWhite) { }

		public override bool IsValidMove(Position from, Position to, ChessBoard board)
		{
			//простое перемещение
			int direction = IsWhite ? 1 : -1;
			if (to.Col == from.Col && board.GetPiece(to) == null && to.Row == from.Row + direction)
			{
				return true;
			}

			//взятие по диагонали
			if(Math.Abs(to.Col - from.Col) == 1 && to.Row == from.Row + direction &&  board.GetPiece(to) != null)
			{
				return true;
			}

			return false;
		}

		public override char GetSymbol()
		{
			return IsWhite ? 'P' : 'p';
		}
	}
}
