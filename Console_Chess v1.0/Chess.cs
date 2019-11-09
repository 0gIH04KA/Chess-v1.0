using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Chess_v1._0
{
    public class Chess
    {
        Random random = new Random();
        
        public string fen { get; private set; }

        Board board;
        Moves moves;
        List<FigureMoving> allMoves;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") 
                         // fen - Начальная позиция шахматной партии
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        private Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
        }

        public Chess Move(string move)
        {
            FigureMoving figureMoving = new FigureMoving(move);

            if (!moves.IsCanMove(figureMoving))
            {
                Console.WriteLine("Данный ход не возможен!!");
                return this;
            }
            if (board.IsChekAfterMove(figureMoving))
            {
                Console.WriteLine("Данный ход не возможен!!");
                return this;
            }

            Board nextBoard = board.Move(figureMoving);
            Chess nextChess = new Chess(nextBoard);

            return nextChess;
        }

        public void My()
        {
            if (board.FenColor() == "w")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Сейчайс ход Белых!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Сейчайс ход Черных!");
                Console.ResetColor();
            }
        }

        public string myy(List<string> list)
        {
            string str = "";

            if (board.FenColor() == "b")
            {
                str = list[random.Next(list.Count + 1)];
            }

            return str;
        }


        public char GetFigureAt(int x, int y)
        {
            char newFigure;
            Square square = new Square(x, y);
            Figure figure = board.GetFigureAt(square);

            if (figure != Figure.none)
            {
                newFigure = (char)figure;
            }
            else
            {
                newFigure = '.';
            }

            return newFigure;
        }

        private void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();

            foreach (FigureOnSquare figureOnSquare in board.YieldFigure())
            {
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving figureMoving = new FigureMoving(figureOnSquare, to);

                    if (moves.IsCanMove(figureMoving))
                    {
                        if (!board.IsChekAfterMove(figureMoving))
                        {
                            allMoves.Add(figureMoving);
                        }
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();

            List<string> list = new List<string>();

            foreach (FigureMoving figureMoving in allMoves)
            {
                list.Add(figureMoving.ToString());
            }

            return list;
        }

        public bool IsChek()
        {
            return board.IsChek();
        }
    }
}
