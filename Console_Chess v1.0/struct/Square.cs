using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Chess_v1._0
{
    struct Square
    {
        /// <summary>
        /// 
        /// Описывет структуру клетки 
        /// 
        /// </summary>
       
        
        public static Square none = new Square(-1, -1);
        /// <summary>
        /// 
        /// статическое поле none задает координаты ложной клетки 
        /// 
        /// </summary>
       
        public int x { get; private set; }
        /// <summary>
        /// 
        /// Поле которое описывает координату Х
        /// 
        /// </summary>


        public int y { get; private set; }
        /// <summary>
        /// 
        /// Поле которое описывает координату Y
        /// 
        /// </summary>

        /// <summary>
        /// 
        /// конструктор для класса клетки в который передаются
        /// численные координаты 
        /// 
        /// </summary>
        /// <param name="x">координата Х </param>
        /// <param name="y">координата Y </param>
        public Square (int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 
        /// дополнительный конструктор в который передается строковое предстваление
        /// 
        /// </summary>
        /// <param name="e2"> сроковое предстваление клетки </param>
        public Square(string e2)
        {
            if (e2.Length == 2 &&
                e2[0] >= 'a' && e2[0] <= 'h' &&
                e2[1] >= '1' && e2[1] <= '8')
            {
                x = e2[0] - 'a';
                y = e2[1] - '1';
            }
            else
            {
                this = none;
            }
        }
        /// <summary>
        /// 
        /// метод проверки нахождения клетки на шахмотной доске
        /// 
        /// </summary>
        /// <returns></returns>
        public bool OnBord()
        {
            bool cheсkLocationCell = false;

            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                cheсkLocationCell = true;
            }

            return cheсkLocationCell;
        }

        public string Name { get { return ((char)('a' + x)).ToString() + (y + 1).ToString(); } }

        public static bool operator == (Square a, Square b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator != (Square a, Square b)
        {
            return !(a == b);   //!(a == b);          // a.x != b.x || a.y != b.y; // !(a == b);
        }

        public static IEnumerable<Square> YieldSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }
    }
}
