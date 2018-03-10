using System;

namespace Triangle
{
    class Triangle
    {
        public int[][] _triangle;
        private int[] _largestPath;
        private int _bottom;
        private int _sum;

        public Triangle(int[][] triangle)
        {
            _triangle = triangle;
            _largestPath = new int[_triangle.Length];
            _bottom = _triangle.Length - 1;
        }

        public MaxSumPath MaxSum()
        {
            _sum = 0;
            var path = new int[_triangle.Length];
            Action<int, int, int, int[]> sum;
            if (_triangle[0][0] % 2 == 0)
                sum = EvenSum;
            else
                sum = OddSum;

            sum(0, 0, 0, path);

            return new MaxSumPath
            {
                Path = _largestPath,
                Sum = _sum
            };
        }

        private void OddSum(int x, int y, int sum, int[] path)
        {
            if (_triangle[x][y] % 2 == 0)
                return;

            Sum(x, y, sum, path, EvenSum);
        }

        private void EvenSum(int x, int y, int sum, int[] path)
        {
            if (_triangle[x][y] % 2 != 0)
                return;
            Sum(x, y, sum, path, OddSum);
        }


        private void Sum(int x, int y, int sum, int[] path, Action<int, int, int, int[]> next)
        {
            sum += _triangle[x][y];
            path[x] = _triangle[x][y];

            if (_bottom == x)
            {
                BottomCheck(path, sum);
                return;
            }

            if (_triangle[x].Length != y)
            {
                next(x + 1, y + 1, sum, path);
            }
            next(x + 1, y, sum, path);
        }

        private void BottomCheck(int[] path, int newSum)
        {
            if (_sum < newSum)
            {
                _sum = newSum;
                Array.Copy(path, _largestPath, path.Length);
            }
        }

        public class MaxSumPath
        {
            public int[] Path { get; set; }
            public int Sum { get; set; }
        }
    }
}
