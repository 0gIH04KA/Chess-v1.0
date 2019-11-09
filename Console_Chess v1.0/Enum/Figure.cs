using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Chess_v1._0
{
    public enum Figure
    {
        none, 

        whiteKing   = 'K', // \u2654
        whiteQueen  = 'Q', // \u2655
        whiteRook   = 'R', // \u2656
        whiteBishop = 'B', // \u2657
        whiteKnight = 'N', // \u2658
        whitePawn   = 'P', // \u2659

        blackKing   = 'k', // \u265A
        blackQueen  = 'q', // \u265B
        blackRook   = 'r', // \u265C
        blackBishop = 'b', // \u265D
        blackKnight = 'n', // \u265E
        blackPawn   = 'p', // \u265F
    }

    static class FigureMethods
    {
        public static Color GetColor(this Figure figure)
        {
            Color color = Color.none;

            if (figure == Figure.whiteKing   ||
                figure == Figure.whiteQueen  ||
                figure == Figure.whiteRook   ||
                figure == Figure.whiteBishop ||
                figure == Figure.whiteKnight ||
                figure == Figure.whitePawn)
            {
                color = Color.white;
            }
            if (figure == Figure.blackKing   ||
                figure == Figure.blackQueen  ||
                figure == Figure.blackRook   ||
                figure == Figure.blackBishop ||
                figure == Figure.blackKnight ||
                figure == Figure.blackPawn)
            {
                color = Color.black;
            }
           
            return color;           
        }
    }
}
