using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Matrix_Calculator_28
{
    class Program
    {
        static void Main()
        {
            do
            {
                Console.Clear();
                Console.WriteLine(" Well hello there!");
                Console.WriteLine(" Here is a brief instruction for my program so that you don't get lost in it");
                Console.WriteLine(" If you want more detailed instructions in Russian, it is attached in the format .txt in the program folder");
                Console.WriteLine(" Matrix Calculator can help you do some stuff like:");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("             ***OPERATIONS*** ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   trace - Finds the trace of the matrix ");
                Console.WriteLine("   transpose - Transpose the matrix ");
                Console.WriteLine("   sum - Finds the sum of two marits ");
                Console.WriteLine("   subtract - Subtracting two numbers ");
                Console.WriteLine("   multiply - Multiplies two matrices ");
                Console.WriteLine("   multinum - Multiplies a matrix by a number ");
                Console.WriteLine("   det - Finds the determinant ");
                Console.WriteLine("   gauss - Solves system of linear algebraic equations by the Gauss method ");
                Console.WriteLine("   help - Shows this window with functions ");
                Console.WriteLine("   exit - Ends the program ");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("             ***INPUT FORM*** ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   stdin - For standart input from console ");
                Console.WriteLine("   txt - For reading matrices from a file  ");
                Console.WriteLine("   random - For generate a random matrix ");
                Console.WriteLine("");
                string query = Console.ReadLine();
                if (!ValidateQuery(query, out string[] queryParameters))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID REQUEST");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    ProcessQuery(queryParameters);
                }
                Console.WriteLine("Press any key to continue, or 'Enter' for exit");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }
        /// <summary>
        /// This method checks userы input.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public static bool ValidateQuery(string query, out string[] queryParameters)
        {
            queryParameters = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] opers = { "trace", "transpose", "sum", "subtract", "multiply", "multinum", "det", "gauss" };
            string[] other = { "help", "exit" };
            string[] create_way = { "stdin", "txt", "random" };
            // If the command entered by the user was not in the array, then we give an error.
            if(queryParameters.Length == 1 && other.Contains(queryParameters[0]))
            {
                return true;
            }
            else if(queryParameters.Length != 2)
            {
                return false;
            }
            else if (!opers.Contains(queryParameters[0]) || !create_way.Contains(queryParameters[1]))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// With a user-specified command, select what the program should output.
        /// </summary>
        /// <param name="queryParameters"></param>
        public static void ProcessQuery(string[] queryParameters)
        {
            string comand = queryParameters[0];
            string input_way = "";
            if (queryParameters.Length == 2)
            {
                input_way = queryParameters[1];
            }
            string[] comands = { "trace", "transpose", "multinum", "det", "gauss", "sum", "subtract", "multiply" };
            int[][] matrix1 = MatrixCreate(1, 1);
            int[][] matrix2 = MatrixCreate(1, 1);
            int rows, cols;
            //Depending on the user's choice, we determine how many matrices we need.
            int ic = Array.IndexOf(comands, comand);
            // If we need only one matrix for operations.
            if(ic > -1 && ic < 5)
            {
                switch (input_way)
                {
                    case "stdin":
                        if(!InputRowsCols(out rows, out cols))
                        {
                            return;
                        }
                        if(!InputMatrix(rows, cols, out matrix1))
                        {
                            return;
                        }
                        break;
                    case "txt":
                        if(!InputFileMatrix(out matrix1))
                        {
                            return;
                        }
                        break;
                    case "random":
                        if (!InputRowsCols(out rows, out cols))
                        {
                            return;
                        }
                        matrix1 = RandomMatrix(rows, cols);
                        break;
                }
            }
            // If we need two matrixes for operations.
            else if (ic >= 5)
            {
                switch (input_way)
                {
                    case "stdin":
                        if (!InputRowsCols(out rows, out cols))
                        {
                            return;
                        }
                        if(!InputMatrix(rows, cols, out matrix1))
                        {
                            return;
                        }
                        if (!InputRowsCols(out rows, out cols))
                        {
                            return;
                        }
                        if(!InputMatrix(rows, cols, out matrix2))
                        {
                            return;
                        }
                        break;
                    case "txt":
                        if(!InputFileMatrix(out matrix1))
                        {
                            return;
                        }
                        if (!InputFileMatrix(out matrix2))
                        {
                            return;
                        }
                        break;
                    case "random":
                        if (!InputRowsCols(out rows, out cols))
                        {
                            return;
                        }
                        matrix1 = RandomMatrix(rows, cols);
                        if (!InputRowsCols(out rows, out cols))
                        {
                            return;
                        }
                        matrix2 = RandomMatrix(rows, cols);
                        break;
                }
            }
            try
            {
                switch (comand)
                {
                    case "trace":
                        FindTrace(matrix1);
                        break;
                    case "transpose":
                        TransposeMatrix(matrix1);
                        break;
                    case "sum":
                        GetSumOfMatrix(matrix1, matrix2);
                        break;
                    case "subtract":
                        Subtraction(matrix1, matrix2);
                        break;
                    case "multiply":
                        MultiplyTwoMatrix(matrix1, matrix2);
                        break;
                    case "multinum":
                        MultiplyByNumber(matrix1);
                        break;
                    case "det":
                        FindDeterminant(matrix1);
                        break;
                    case "gauss":
                        SolveWithGauss(matrix1);
                        break;
                    case "help":
                        Help();
                        break;
                    case "exit":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Bye! Have a nice day!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;

                }
            }
            // Print an error if Exeption gets out.
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*ERROR*");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        /// <summary>
        /// Show the window with tips if the user is confused.
        /// </summary>
        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          ***HELPER*** ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   trace - Finds the trace of the matrix ");
            Console.WriteLine("   transpose - Transpose the matrix ");
            Console.WriteLine("   sum - Finds the sum of two marits ");
            Console.WriteLine("   subtract - Subtracting two numbers ");
            Console.WriteLine("   multiply - Multiplies two matrices ");
            Console.WriteLine("   multinum - Multiplies a matrix by a number ");
            Console.WriteLine("   det - Finds the determinant ");
            Console.WriteLine("   gauss - Solves system of linear algebraic equations by the Gauss method ");
            Console.WriteLine("   help - Shows this window with functions ");
            Console.WriteLine("   exit - Ends the program ");
        }
        /// <summary>
        /// This method finds the trace of the matrix.
        /// </summary>
        /// <param name="matrix"></first matrix>
        public static void FindTrace(int[][] matrix)
        {
            int rows = matrix[0].Length;
            int cols = matrix.Length;
            int trace = 0;
            // Checking that the matrix is square.
            if (rows != cols)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THE MATRIX MUST BE SQUARE");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            for (int i = 0; i < rows; i++)
            {
                trace += matrix[i][i];
            }
            Console.WriteLine($"The trace of the matrix is: {trace}");
        }
        /// <summary>
        /// This method transposes the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        public static void TransposeMatrix(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            int[][] transposed = MatrixCreate(cols, rows);
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    transposed[i][j] = matrix[j][i];
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your Matrix:");
            Console.ForegroundColor = ConsoleColor.White;
            PrintMatrix(transposed);
        }
        /// <summary>
        /// This method finds the sum of two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        public static void GetSumOfMatrix(int[][] matrix1, int[][] matrix2)
        {
            if (matrix1.Length != matrix2.Length || matrix1[0].Length != matrix2[0].Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THE MATRICES MUST BE THE SAME SIZE MATRIX MUST BE SQUARE");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            int rows = matrix1.Length;
            int cols = matrix1[0].Length;
            int[][] matrix_sum = MatrixCreate(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix_sum[i][j] = matrix1[i][j] + matrix2[i][j];
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your Matrix:");
            Console.ForegroundColor = ConsoleColor.White;
            PrintMatrix(matrix_sum);
        }
        /// <summary>
        /// This method finds the difference between two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        public static void Subtraction(int[][] matrix1, int[][] matrix2)
        {
            if (matrix1.Length != matrix2.Length|| matrix1[0].Length != matrix2[0].Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THE MATRICES MUST BE THE SAME SIZE MATRIX MUST BE SQUARE");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            int rows = matrix1.Length;
            int cols = matrix1[0].Length;
            int[][] matrix_sub = MatrixCreate(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < matrix2.Length; j++)
                {
                    matrix_sub[i][j] = matrix1[i][j] - matrix2[i][j];
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your Matrix:");
            Console.ForegroundColor = ConsoleColor.White;
            PrintMatrix(matrix_sub);
        }
        /// <summary>
        /// This method multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        public static void MultiplyTwoMatrix(int[][] matrix1, int[][] matrix2)
        {
            int rows1, cols1, rows2, cols2;
            rows1 = matrix1.Length;
            cols1 = matrix1[0].Length;
            rows2 = matrix2.Length;
            cols2 = matrix2[0].Length;
            if (cols1 != rows2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THE NUMBER OF COLUMNS OF THE MATRIX 1 DOES NOT EQUAL THE NUMBER OF ROWS OF 2 MATRICES");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            int[][] matrix = MatrixCreate(rows1, cols2);
            for (int i = 0; i < rows1; ++i)
            {
                for (int j = 0; j < cols2; ++j)
                {
                    for (int k = 0; k < cols1; ++k)
                    {
                        matrix[i][j] += matrix1[i][k] * matrix2[k][j];
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your Matrix:");
            Console.ForegroundColor = ConsoleColor.White;
            PrintMatrix(matrix);
        }
        /// <summary>
        /// This method multiplies the matrix by a number.
        /// </summary>
        /// <param name="matrix"></param>
        public static void MultiplyByNumber(int[][] matrix)
        {
            Console.Write("What number do you want to multiply the matrix by?   ");
            int number;
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("YOU CAN'T MULTIPLY THE MATRIX BY THE VALUE YOU ENTERED");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    matrix[i][j] *= number;
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your Matrix:");
            Console.ForegroundColor = ConsoleColor.White;
            PrintMatrix(matrix);
        }
        /// <summary>
        /// This method finds the determinant of the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        public static void FindDeterminant(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            List<int> was_ip = new List<int>();
            long det = 1;
            if (rows != cols)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THE ENTERED EQUATION IS INCORRECT");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            int[] row;
            int ip, multiplier_n1, multiplier_n2, number1, number2;
            long determn = 1;
            for (int j = 0; j < cols; j++)
            {
                for (ip = 0; ip < rows; ip++)
                {
                    if (matrix[ip][j] != 0 && !was_ip.Contains(ip))
                    {
                        was_ip.Add(ip);
                        break;
                    }
                }
                if (ip != rows)
                {
                    row = matrix[ip];
                    for (int i = 0; i < rows; i++)
                    {
                        if (i != ip)
                        {
                            number1 = row[j];
                            number2 = matrix[i][j];
                            multiplier_n1 = number2 / GCD(number1, number2);
                            multiplier_n2 = number1 / GCD(number1, number2);
                            determn *= multiplier_n2;
                            for (int j1 = 0; j1 < cols; j1++)
                            {
                                matrix[i][j1] = matrix[i][j1] * multiplier_n2 - row[j1] * multiplier_n1;
                            }
                        }
                    }
                }
            }
            int mn;
            for (int i = 0; i < rows; i++)
            {
                mn = 0;
                for (int j = 0; j < cols; j++)
                {
                    if(matrix[i][j] != 0)
                    {
                        mn = matrix[i][j];
                        break;
                    }
                }
                det *= mn;
            }
            det /= determn;
            Console.WriteLine($"The determinant of the matrix is: {det} ");
        }
        /// <summary>
        /// This method solves SLAE by the Gaussian method.
        /// </summary>
        /// <param name="matrix"></param>
        public static void SolveWithGauss(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            List<int> was_ip = new List<int>();
            if (rows + 1 != cols)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THE ENTERED EQUATION IS INCORRECT");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            int[] row;
            int ip, multiplier_n1, multiplier_n2, number1, number2;
            for (int j = 0; j < cols - 1; j++)
            {
                for (ip = 0; ip < rows; ip++)
                {
                    if (matrix[ip][j] != 0 && !was_ip.Contains(ip))
                    {
                        was_ip.Add(ip);
                        break;
                    }
                }
                if (ip != rows)
                {
                    row = matrix[ip];
                    for(int i = 0; i< rows; i++)
                    {
                        if(i != ip)
                        {
                            number1 = row[j];
                            number2 = matrix[i][j];
                            multiplier_n1 = number2 / GCD(number1, number2);
                            multiplier_n2 = number1 / GCD(number1, number2);
                            for(int j1 = j; j1 < cols; j1++)
                            {
                                matrix[i][j1] = matrix[i][j1] * multiplier_n2 - row[j1] * multiplier_n1;
                            }
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"STEP {j+1}: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    PrintMatrix(matrix);
                }

            }
        }
        /// <summary>
        /// This method finds the Greatest Common Divisor for the Gauss solution.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static int GCD(int n1, int n2)
        {
            // Here n1 and n2 are the numbers one and two.
            n1 = Math.Abs(n1);
            n2 = Math.Abs(n2);
            int t;
            if (n1 < n2)
            {
                t = n1;
                n1 = n2;
                n2 = t;
            }
            if (n2 == 0)
            {
                return n1;
            }
            return GCD(n2, n1 % n2);
        }
        /// <summary>
        /// This method allocates space in memory for the matrix.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static int[][] MatrixCreate(int rows, int cols)
        {
            int[][] matrix = new int[rows][];
            for (int i = 0; i < rows; ++i)
                matrix[i] = new int[cols];
            return matrix;
        }
        /// <summary>
        /// This method outputs the matrix to the console.
        /// </summary>
        /// <param name="matrix"></param>
        public static void PrintMatrix(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// This method creates a matrix based on user-defined parameters.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool InputMatrix(int rows, int cols, out int[][] matrix)
        {
            matrix = MatrixCreate(rows, cols);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter the matrix line by line with spaces between the digits: ");
            Console.ForegroundColor = ConsoleColor.White;
            string[] row;
            for (int i = 0; i < rows; i++)
            {
                row = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (row.Length != cols)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INCORRECT ROW LENGTH");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                for (int j = 0; j < cols; j++)
                {
                    if (!int.TryParse(row[j], out matrix[i][j]) || matrix[i][j] >= 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ELEMENTS MUST BE INTEGERS NOT BIGGER THAN 100");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// This method reads the matrix from a file.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool InputFileMatrix(out int[][] matrix)
        {
            matrix = MatrixCreate(1, 1);
            Console.WriteLine("Enter the full path to the file:");
            string path = Console.ReadLine();
            try
            {
                string[] lines = File.ReadAllLines(path);
                if (!int.TryParse(lines[0], out int rows) || rows < 1 || rows > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID ROWS");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                if (!int.TryParse(lines[1], out int cols) || cols < 1 || cols > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID COLS");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                if (rows != lines.Length - 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID MATRIX SIZE");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                matrix = MatrixCreate(rows, cols);
                string[] str_row;
                for (int i = 2; i < rows + 2; i++)
                {
                    str_row = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (str_row.Length != cols)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("INVALID MATRIX SIZE");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                    for (int j = 0; j < cols; j++)
                    {
                        if (!int.TryParse(str_row[j], out matrix[i-2][j]) || matrix[i-2][j] > 100 || matrix[i - 2][j] < -100)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("INVALID MATRIX SIZE");
                            Console.ForegroundColor = ConsoleColor.White;
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("THERE IS NO SUCH WAY");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }

        }
        /// <summary>
        /// This method creates a matrix of random numbers.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static int[][] RandomMatrix(int rows, int cols)
        {
            int[][] matrix = MatrixCreate(rows, cols);
            Random randomic = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i][j] = randomic.Next(-100, 100);
                }
            }
            PrintMatrix(matrix);
            return matrix;
        }
        /// <summary>
        /// This method checks whether it is possible to create a matrix of the specified size.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static bool InputRowsCols(out int rows, out int cols)
        {
            Console.WriteLine("Enter the matrix size from 1-10:");
            Console.Write("Rows: ");
            string s_rows = Console.ReadLine();
            Console.Write("Columns: ");
            string s_cols = Console.ReadLine();
            cols = 0;
            if (!int.TryParse(s_rows, out rows) || !int.TryParse(s_cols, out cols)
                || rows > 10 || rows < 1
                || cols > 10 || cols < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("IT IS NOT POSSIBLE TO CREATE SUCH MATRIX");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return true;
        }
    }

}
