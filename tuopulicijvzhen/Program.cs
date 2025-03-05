using System;

namespace ToeplitzMatrixChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = {
                { 1, 2, 3, 4 },
                { 5, 1, 2, 3 },
                { 9, 5, 1, 2 }
            };

            bool isToeplitz = IsToeplitzMatrix(matrix);
            Console.WriteLine("该矩阵是否为托普利茨矩阵？ " + isToeplitz);
        }

        static bool IsToeplitzMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // 检查每条对角线上的元素是否相同
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    if (matrix[i, j] != matrix[i - 1, j - 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
