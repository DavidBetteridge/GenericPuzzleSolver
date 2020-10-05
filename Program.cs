using GenericPuzzleSolver.Games;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Transactions;

namespace GenericPuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = SkyScrapers.Setup();
            //var board = Futoshiki.Setup();
            //var board = RoundNumbers.Setup();

            while (!Complete(board))
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    for (int row = 0; row < board.GetLength(1); row++)
                    {
                        if (board[column, row].PossibleValues.Count > 1)
                        {
                            foreach (var possibleValue in board[column, row].PossibleValues.ToList())
                            {
                                if (board[column, row].Constraints.Any(c => !c.ValueIsValid(possibleValue, board)))
                                {
                                    board[column, row].PossibleValues.Remove(possibleValue);
                                }
                            }
                        }
                    }
                }

                for (int column = 0; column < board.GetLength(0); column++)
                {
                    for (int possibleValue = 0; possibleValue <= board.GetLength(0); possibleValue++)
                    {
                        var cellsContainingValue = new List<int>();
                        for (int row = 0; row < board.GetLength(1); row++)
                        {
                            if (board[column, row].PossibleValues.Contains(possibleValue))
                            {
                                cellsContainingValue.Add(row);
                            }
                        }

                        if (cellsContainingValue.Count == 1)
                        {
                            if (board[column, cellsContainingValue.Single()].PossibleValues.Count > 1)
                            {
                                board[column, cellsContainingValue.Single()].PossibleValues = new List<int> { possibleValue };
                            }
                        }

                    }
                }

                for (int row = 0; row < board.GetLength(1); row++)
                {
                    for (int possibleValue = 0; possibleValue <= board.GetLength(1); possibleValue++)
                    {
                        var cellsContainingValue = new List<int>();
                        for (int column = 0; column < board.GetLength(0); column++)
                        {
                            if (board[column, row].PossibleValues.Contains(possibleValue))
                            {
                                cellsContainingValue.Add(column);
                            }
                        }

                        if (cellsContainingValue.Count == 1)
                        {
                            if (board[cellsContainingValue.Single(), row].PossibleValues.Count > 1)
                            {
                                board[cellsContainingValue.Single(), row].PossibleValues = new List<int> { possibleValue };
                            }
                        }

                    }
                }

                CreateImage(board);


                //DisplayOptions(board, 6, 6);

                DisplayBoard(board);
                Console.ReadKey();

            }

            Console.ReadKey();

        }

        private static void CreateImage(Cell[,] board)
        {
            var columnWidth = 1000 / board.GetLength(0);
            var rowHeight = 1000 / board.GetLength(1);

            using var bitmap = new Bitmap(1100, 1100, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using var graphics = Graphics.FromImage(bitmap);
            var pen = new Pen(Color.Black);
            var possibleBrush = new SolidBrush(Color.Gray);
            var possibleFont = new Font("Arial", 16);
            var solvedBrush = new SolidBrush(Color.Black);
            var solvedFont = new Font("Arial", 50);


            graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(0, 0, 1100, 1100));
            graphics.TranslateTransform(50, 50);
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 1000, 1000));

            for (int row = 1; row < board.GetLength(1); row++)
            {
                var y = row * rowHeight;
                graphics.DrawLine(pen, 0, y, 1000, y);
            }

            for (int column = 1; column < board.GetLength(0); column++)
            {
                var x = column * columnWidth;
                graphics.DrawLine(pen, x, 0, x, 1000);
            }

            for (int row = 0; row < board.GetLength(1); row++)
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    if (board[column, row].PossibleValues.Count == 1)
                    {
                        var value = board[column, row].PossibleValues.Single().ToString();
                        var size = graphics.MeasureString(value, solvedFont);
                        var x = (column * columnWidth) + ((columnWidth / 2) - (size.Width / 2));
                        var y = (row * rowHeight) + ((rowHeight / 2) - (size.Height / 2));
                        graphics.DrawString(value, solvedFont, solvedBrush, x, y);
                    }
                    else
                    {
                        float xOffset = 0;
                        float yOffset = 10;
                        foreach (var possibleValue in board[column, row].PossibleValues)
                        {
                            var value = possibleValue.ToString();
                            var size = graphics.MeasureString(value, possibleFont);
                            var x = (column * columnWidth) + xOffset;
                            var y = (row * rowHeight) + yOffset;
                            graphics.DrawString(value, possibleFont, possibleBrush, x, y);

                            xOffset += size.Width + 20;
                            if (xOffset > columnWidth)
                            {
                                xOffset = 0;
                                yOffset = rowHeight - size.Height - 10;
                            }
                        }
                    }
                }
            }

            bitmap.Save(@"c:\temp\puzzle.bmp");
        }

        private static bool Complete(Cell[,] board)
        {
            for (int row = 0; row < board.GetLength(1); row++)
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    if (board[column, row].PossibleValues.Count > 1)
                        return false;
                }
            }

            return true;
        }

        private static void DisplayOptions(Cell[,] board, int column, int row)
        {
            var values = string.Join(',', board[column, row].PossibleValues);
            Console.WriteLine($"Cell {column},{row} can be {values}");
        }

        private static void DisplayBoard(Cell[,] board)
        {
            Console.WriteLine("");

            for (int row = 0; row < board.GetLength(1); row++)
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    if (board[column, row].PossibleValues.Count > 1)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write(board[column, row].PossibleValues.Single());
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
        }
    }
}
