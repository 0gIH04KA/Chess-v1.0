using System;
using System.Collections.Generic;
using System.Text;


namespace Console_Chess_v1._0
{
    class Moves
    {
        /// <summary>
        /// 
        /// В этом классе описана:
        /// ЛОГИКА движения фигур
        /// 
        /// </summary>
       
        FigureMoving figureMoving;
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// 
        /// метод который описывает: 
        /// движения ЛЮБОЙ фигуры
        /// 
        /// </summary>
        /// <param name="figureMoving"> фигура которая совершает движение </param>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли ДАННАЯ фигура совершать ход 
        /// 
        /// </returns>
        public bool IsCanMove(FigureMoving figureMoving)
        {
            this.figureMoving = figureMoving;
            bool canMove = CanMoveFrom() &&
                           CanMoveTo() &&
                           CanFigureMove();

            return canMove;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// движения фигуры С позиции
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// возвращает логическое значение
        /// может ли ДАННАЯ фигура совершить движение С этой клетки
        /// 
        /// </returns>
        private bool CanMoveFrom()
        {
            bool canFigureWalk = false;
            bool moveFrom = figureMoving.from.OnBord();

            if (figureMoving.figure.GetColor() == board.moveColor)
            {
                canFigureWalk = true;
            }

            moveFrom = moveFrom && canFigureWalk;

            return moveFrom;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// движения фигуры В данную позицию
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// возвращает логическое значение
        /// может ли ДАННАЯ фигура совершить движение В эту клетки
        /// 
        /// </returns>
        private bool CanMoveTo()
        {
            bool canFigureWalk = false;
            bool moveTo = figureMoving.to.OnBord();

            if (figureMoving.from != figureMoving.to)
            {
                if (board.GetFigureAt(figureMoving.to).GetColor() != board.moveColor)
                {
                    canFigureWalk = true;
                }
            }
           
            moveTo = moveTo && canFigureWalk;

            return moveTo;
        }
        /// <summary>
        /// 
        /// метод который описывает:
        /// логику движений ВСЕХ фигур
        /// 
        /// </summary>
        /// <returns> 
        /// 
        /// возвращает логическое значение
        /// может ли ДАННАЯ фигура перейти в указанную клетку
        /// в зависимости от логики фигуры
        /// 
        /// </returns>
        private bool CanFigureMove()
        {
            bool canFigureWalk = false;
            
            switch (figureMoving.figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:

                    if (CanKingMove())
                    {
                        canFigureWalk = true;
                    }

                    break;

                case Figure.whiteQueen:
                case Figure.blackQueen:

                    if (CanStraightMove())
                    {
                        canFigureWalk = true;
                    }

                    break;

                case Figure.whiteRook:
                case Figure.blackRook:

                    if (CanStraightMove()       && 
                        figureMoving.SignX == 0 || 
                        figureMoving.SignY == 0 )
                    {
                        canFigureWalk = true;
                    }

                    break;

                case Figure.whiteBishop:
                case Figure.blackBishop:

                    if (figureMoving.SignX != 0 && 
                        figureMoving.SignY != 0 &&
                        CanStraightMove())
                    {
                        canFigureWalk = true;
                    }

                    break;

                case Figure.whiteKnight:
                case Figure.blackKnight:
                    
                    if (CanKnightMove())
                    {
                        canFigureWalk = true;
                    }

                    break;

                case Figure.whitePawn:
                case Figure.blackPawn:
                    
                    if (CanPawnMowe())
                    {
                        canFigureWalk = true;
                    }
                    break;

                default:

                    canFigureWalk = false;
                    break;
            }

            return canFigureWalk;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// движение Пешки
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли ДАННАЯ Пешка совершать движение
        /// 
        /// </returns>
        private bool CanPawnMowe()
        {
            bool canPawnWalk = false;
            int stepY = 0;

            if (figureMoving.from.y < (int)CoordinateY.Position_1 || 
                figureMoving.from.y > (int)CoordinateY.Position_6)
            {
                canPawnWalk = false;
            }

            if (figureMoving.figure.GetColor() == Color.white)
            {
                stepY = 1;
            }
            else
            {
                stepY = -1;
            }

            if (CanPawnGo(stepY)   || 
                CanPawnJump(stepY) || 
                CanPawnEat(stepY) )
            {
                canPawnWalk = true;
            }

            return canPawnWalk;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// может ли ДАННАЯ Пешка сьесть противника 
        /// 
        /// </summary>
        /// <param name="stepY">количество шагов Пешки по координате Y</param>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли ДАННАЯ Пешка сьесть противника 
        /// 
        /// </returns>
        private bool CanPawnEat(int stepY)
        {
            bool myBoll = false;

            if (board.GetFigureAt(figureMoving.to) == Figure.none)
            {
                if (figureMoving.DeltaX == 0)
                {
                    if (figureMoving.DeltaY == stepY)
                    {
                        myBoll = true;
                    }
                }
            }

            return myBoll;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// может ли ДАННАЯ Пешка совершить прыжок 
        /// 
        /// </summary>
        /// <param name="stepY">количество шагов Пешки по координате Y</param>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли ДАННАЯ Пешка совершить ход на ДВЕ клетки
        /// 
        /// </returns>
        private bool CanPawnJump(int stepY)
        {
            bool canPawnWalk = false;

            if (board.GetFigureAt(figureMoving.to) == Figure.none)
            {
                if (figureMoving.DeltaX == 0)
                {
                    if (figureMoving.DeltaY == (int)DifferentValues.doubleStrokePawn * stepY)
                    {
                        if (figureMoving.from.y == 1 || 
                            figureMoving.from.y == 6)
                        {
                            if (board.GetFigureAt(new Square(figureMoving.from.x, 
                                figureMoving.from.y + stepY)) == Figure.none)
                            {
                                canPawnWalk = true;
                            }
                        }
                    }
                }
            }

            return canPawnWalk;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// движение Пешки на одну клетку
        /// 
        /// </summary>
        /// <param name="stepY">количество шагов Пешки по координате Y<</param>
        /// <returns>
        /// 
        /// возвращает логическое значение
        /// может ли ДАННАЯ Пешка идти прямо
        /// 
        /// </returns>
        private bool CanPawnGo(int stepY)
        {
            bool myBoll = false;

            if (board.GetFigureAt(figureMoving.to) != Figure.none)
            {
                if (figureMoving.AbsDeltaX == 1)
                {
                    if (figureMoving.DeltaY == stepY)
                    {
                        myBoll = true;
                    }
                }
            }

            return myBoll;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// может ли ДАННАЯ фигура совершать движение прямое
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли ДАННАЯ фигура совершать ход прямо
        /// 
        /// </returns>
        private bool CanStraightMove()
        {
            bool canFigureStraightWalk = false;
            Square at = figureMoving.from;
            do
            {
                at = new Square(at.x + figureMoving.SignX,
                                at.y + figureMoving.SignY);

                if (at == figureMoving.to)
                {
                    canFigureStraightWalk = true;
                }

            } while (at.OnBord() && board.GetFigureAt(at) == Figure.none);

            return canFigureStraightWalk;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// логику ходов Короля  
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли Король совершить ход
        /// 
        /// </returns>
        private bool CanKingMove()
        {
            bool myBoll = false;

            if (figureMoving.AbsDeltaX <= 1 && 
                figureMoving.AbsDeltaY <= 1)
            {
                myBoll = true;
            }

            return myBoll;
        }

        /// <summary>
        /// 
        /// метод который описывает:
        /// логику ходов Коня  
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// возвращает логическое значение 
        /// может ли ДАННАЯ конь совершить ход
        /// 
        /// </returns>
        private bool CanKnightMove()
        {
            bool canKnightWalk = false;

            if (figureMoving.AbsDeltaX == (int)DifferentValues.moveKnightAbsX_1 && 
                figureMoving.AbsDeltaY == (int)DifferentValues.moveKnightAbsY_2)
            {
                canKnightWalk = true;
            }

            if (figureMoving.AbsDeltaX == (int)DifferentValues.moveKnightAbsX_2 && 
                figureMoving.AbsDeltaY == (int)DifferentValues.moveKnightAbsY_1)
            {
                canKnightWalk = true;
            }

            return canKnightWalk;
        }
        
    }
}
