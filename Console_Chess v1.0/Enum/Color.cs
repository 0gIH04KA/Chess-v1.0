using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Chess_v1._0
{
    public enum Color
    {
        none, 
        white,
        black
    }

    static class ColorMethods
    {
        public static Color FlipColor(this Color color)
        {
            Color newColor = Color.none;

            if (color == Color.black)
            {
                newColor = Color.white;
            }

            if (color == Color.white)
            {
                newColor = Color.black;
            }

            return newColor;
        }
    }
}
