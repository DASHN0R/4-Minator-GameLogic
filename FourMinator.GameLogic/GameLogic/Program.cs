using System;

namespace GameLogic
{
    class Program
    {
        private const int Rows = 6;
        private const int Columns = 7;
        private const int Empty = 0;
        private const int Red = 1;
        private const int Yellow = 2;

        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                int[,] board = new int[Rows, Columns];
                int currentPlayer = GetRandomStartingPlayer();
                bool gameRunning = true;
                int movesMade = 0;

                while (gameRunning)
                {
                    Console.Clear();
                    DrawBoard(board);
                    Console.WriteLine($"Player {currentPlayer} ({GetPlayerColor(currentPlayer)})'s turn. Choose a column (0-6): ");

                    int column = GetValidColumnInput(board);
                    if (column != -1)
                    {
                        if (DropDisc(board, column, currentPlayer))
                        {
                            movesMade++;
                            if (CheckWin(board, currentPlayer))
                            {
                                Console.Clear();
                                DrawBoard(board);
                                Console.WriteLine($"Player {currentPlayer} ({GetPlayerColor(currentPlayer)}) wins!");
                                gameRunning = false;
                            }
                            else if (movesMade == Rows * Columns)
                            {
                                Console.Clear();
                                DrawBoard(board);
                                Console.WriteLine("It's a draw!");
                                gameRunning = false;
                            }
                            else
                            {
                                currentPlayer = SwitchPlayer(currentPlayer);
                            }
                        }
                        else
                        {
                            Console.WriteLine("This column is full. Please choose another one.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please choose a column from 0 to 6");
                    }
                }

                Console.WriteLine("Press 1 to play again or 2 to exit.");
                string choice = GetValidChoiceInput();
                playAgain = choice == "1";
            }

            Console.WriteLine("Goodbye!");
        }

        private static int GetRandomStartingPlayer()
        {
            Random random = new Random();
            return random.Next(1, 3);
        }

        private static void DrawBoard(int[,] board)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    Console.Write("|");
                    Console.Write(GetPlayerColor(board[row, column]));
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(new string('-', Columns * 2 + 1));
        }

        private static char GetPlayerColor(int player)
        {
            return player switch
            {
                Red => 'R',
                Yellow => 'Y',
                _ => ' '
            };
        }

        private static int GetValidColumnInput(int[,] board)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int column) && IsValidColumn(column))
                {
                    if (board[0, column] == Empty)
                    {
                        return column;
                    }
                    else
                    {
                        Console.WriteLine("This column is full. Please choose another one.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose a column from 0 to 6");
                }
            }
        }


        private static bool IsValidColumn(int column)
        {
            return column >= 0 && column < Columns;
        }

        private static bool DropDisc(int[,] board, int column, int player)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, column] == Empty)
                {
                    board[row, column] = player;
                    return true;
                }
            }

            return false;
        }

        private static bool CheckWin(int[,] board, int player)
        {
            return CheckHorizontalWin(board, player) ||
                   CheckVerticalWin(board, player) ||
                   CheckDiagonalWin(board, player);
        }

        private static bool CheckHorizontalWin(int[,] board, int player)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns - 3; column++)
                {
                    if (board[row, column] == player &&
                        board[row, column + 1] == player &&
                        board[row, column + 2] == player &&
                        board[row, column + 3] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckVerticalWin(int[,] board, int player)
        {
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (board[row, column] == player &&
                        board[row + 1, column] == player &&
                        board[row + 2, column] == player &&
                        board[row + 3, column] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckDiagonalWin(int[,] board, int player)
        {
            // Check for diagonals left \
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int column = 0; column < Columns - 3; column++)
                {
                    if (board[row, column] == player &&
                        board[row + 1, column + 1] == player &&
                        board[row + 2, column + 2] == player &&
                        board[row + 3, column + 3] == player)
                    {
                        return true;
                    }
                }
            }

            // Check for diagonals right /
            for (int row = 3; row < Rows; row++)
            {
                for (int column = 0; column < Columns - 3; column++)
                {
                    if (board[row, column] == player &&
                        board[row - 1, column + 1] == player &&
                        board[row - 2, column + 2] == player &&
                        board[row - 3, column + 3] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static int SwitchPlayer(int currentPlayer)
        {
            return currentPlayer == Red ? Yellow : Red;
        }

        private static string GetValidChoiceInput()
        {
            while (true)
            {
                string choice = Console.ReadLine();
                if (choice == "1" || choice == "2")
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1 to play again or 2 to exit.");
                }
            }
        }
    }
}