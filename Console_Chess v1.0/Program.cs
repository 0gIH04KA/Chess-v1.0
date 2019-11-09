using System;
using System.Collections.Generic;

namespace Console_Chess_v1._0
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Random random = new Random();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Chess chess = new Chess(); // ("rnbqkbnr/1p1111p1/8/8/8/8/1P1111P1/RNBQKBNR w KQkq - 0 1");


            List<string> list;

            while (true)
            {
                //Console.Clear();
                
                chess.My();

                list = chess.GetAllMoves();
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));

                Console.WriteLine(chess.IsChek() ? "CHEK" : "");

                foreach (string moves in list)
                {
                    Console.Write(moves + "\t");
                }

                Console.WriteLine();
                Console.Write(">");

                string move = Console.ReadLine();

                if (move == "q")
                {
                    break;
                }
                if (move == "")
                {
                    move = list[random.Next(list.Count + 1)];
                }
                
                
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess chess)
        {
            string text = "  +-----------------+ \n";

            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }
                text += "|\n";
            }
            text += "  +-----------------+ \n";
            text += "    a b c d e f g h \n";

            return text;

        }

        static void Print(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char x = text[i];

                if (x >= 'a' && x <= 'z')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (x >= 'A' && x <= 'Z')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.Write(x);
                Console.ResetColor();
            }
        }
        
    }
}
