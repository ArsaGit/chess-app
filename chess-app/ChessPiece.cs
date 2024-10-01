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
			//проверка на вертикальное или горизантальное передвижение и на помеху
			if(from.Row == to.Row)
			{
				int direction = to.Col > from.Col ? 1 : -1;
				for(int col = from.Col + direction; col != to.Col; col += direction)
				{
					if(board.GetPiece(new Position(from.Row, col)) != null)
					{
						return false;	//есть фигура на пути
					}
				}
			}
			else if(from.Col == to.Col)
			{
				int direction = to.Row > from.Row ? 1 : -1;
				for (int row = from.Row + direction; row != to.Row; row += direction)
				{
					if (board.GetPiece(new Position(row, from.Col)) != null)
					{
						return false;   //есть фигура на пути
					}
				}
			}
			else
			{
				return false;	//движение не по прямой линии
			}

			return true;	//путь свободен
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
			//проверка на горизонтальное движение
			if (Math.Abs(from.Row - to.Row) == Math.Abs(from.Col - to.Col))
			{
				return false;	//не горизонтальное
			}

			int rowDirection = to.Row > from.Row ? 1 : -1;
			int colDirection = to.Col > from.Col ? 1 : -1;

			int checkRow = from.Row + rowDirection;
			int checkCol = from.Col + colDirection;

			while(checkRow != to.Row && checkCol != to.Col)
			{
				if (board.GetPiece(new Position(checkRow, checkCol)) != null)
				{
					return false;	//путь не свободен
				}
				checkRow += rowDirection;
				checkCol += colDirection;
			}
			return true;	//путь свободен
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
