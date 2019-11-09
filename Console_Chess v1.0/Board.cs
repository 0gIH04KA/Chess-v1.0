using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Chess_v1._0
{
    class Board
    {   
    /// <summary>
    /// 
    /// этот класс описывает шахматною доску в целов
    /// 
    /// </summary>



        public string fen { get; private set; }
        /// <summary>
        /// фен - стандартная нотация записи шахматных диаграмм.
        /// </summary>

        Figure[,] figures; 
        public Color moveColor { get; private set; }
        public int moveNumber { get; private set; }

        public Board (string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        private void Init()
        { //"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") 
          // 0                    =/=                  0 1 2    3 4 5

          // 0- расположение фигур
          // 1 - чей первый ход
          // 2 - флаги рокировки
          // 3 - битое поле
          // 4 - количество ходов (для правила 50 ходов)
          // 5 - номер хода сейчас 

            string[] parts = fen.Split();
            if (parts.Length != 6)
            {
                return;
            }

            InitFigure(parts[0]);

            if (parts[1] == "b")
            {
                moveColor = Color.black;
            }
            if(parts[1] == "w")
            {
                moveColor = Color.white;
            }
            
            moveNumber = int.Parse(parts[5]);
        }

        private void InitFigure(string data)
        {
            for (int j = 8; j >= 2 ; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            data = data.Replace("1", ".");

            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (lines[7 - y][x] == '.')
                    {
                        figures[x, y] = Figure.none;
                    }
                    else
                    {
                        figures[x, y] = (Figure)lines[7 - y][x];
                    }
                    
                }
            }

        }

        public IEnumerable<FigureOnSquare> YieldFigure()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == moveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }
        }

        private void GenereteFen()
        {
            fen = FenFigure() + " " +
                FenColor() + " - - 0 " +
                moveNumber.ToString();
        }

        private string FenFigure()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (figures[x, y] == Figure.none)
                    {
                        stringBuilder.Append('1');
                    }
                    else
                    {
                        stringBuilder.Append((char)figures[x, y]);
                    }
                }

                if (y > 0)
                {
                    stringBuilder.Append('/');
                }
            }

            string eight = "11111111";

            for (int j = 8; j >= 2; j--)
            {
                stringBuilder.Replace(eight.Substring(0, j), j.ToString());
            }

            return stringBuilder.ToString();
        }

        public string FenColor()
        {
            string color = "";

            if (moveColor != Color.black)
            {
                color = "w";
            }
            else
            {
                color = "b";
            }
            
            return color;
        }

        public Figure GetFigureAt(Square square)
        {
            if (!square.OnBord())
            {
                return Figure.none;
            }

            return figures[square.x, square.y];
        }

        private void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBord())
            {
                figures[square.x, square.y] = figure;
            }
        }

        public Board Move(FigureMoving figureMoving)
        {
            Board next = new Board(fen);
            next.SetFigureAt(figureMoving.from, Figure.none);

            if (figureMoving.promotion == Figure.none)
            {
                next.SetFigureAt(figureMoving.to, figureMoving.figure);
            }
            else
            {
                next.SetFigureAt(figureMoving.to, figureMoving.promotion);
            }

            if (moveColor == Color.black)
            {
                next.moveNumber++;
            }

            next.moveColor = moveColor.FlipColor();
            next.GenereteFen();

            return next; 
        }

        public bool IsChek()
        {
            Board after = new Board(fen);
            after.moveColor = moveColor.FlipColor();

            return after.CanEatKing();
        }

        private bool CanEatKing ()
        {
            bool myBool = false;

            Square enemyKing = FindEnemyKing();
            Moves moves = new Moves(this);

            foreach (FigureOnSquare figureOnSquare in YieldFigure())
            {
                FigureMoving figureMoving = new FigureMoving(figureOnSquare, enemyKing);
               
                if (moves.IsCanMove(figureMoving))
                {
                    myBool = true;
                }
            }

            return myBool;
        }

        private Square FindEnemyKing()
        {

            Figure enemyKing;

            if (moveColor == Color.black)
            {
                enemyKing = Figure.whiteKing;
            }
            else
            {
                enemyKing = Figure.blackKing;
            }

            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square) == enemyKing)
                {
                    return square;
                }
            }

            return Square.none;
        }

        public bool IsChekAfterMove(FigureMoving figureMoving)
        {
            Board after = Move(figureMoving);

            return after.CanEatKing();
        }

    }
}
