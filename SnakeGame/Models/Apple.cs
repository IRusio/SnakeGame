using System;
using System.Collections.Generic;

namespace SnakeGame.Models
{
    public class Apple
    {
        public (int x, int y) applePosition { get; set; }

        public Apple(int countOfWidthPoints, int countOfHeightPoints, Snake snake)
        {
            applePosition = GenerateNewApplePosition(countOfWidthPoints, countOfWidthPoints, snake);
        }

        public (int, int) GenerateNewApplePosition(int countOfWidthPoints, int countOfHeightPoints, Snake snake)
        {
            Random random = new Random();
            while (true)
            {
                var point = (x: random.Next(1, countOfWidthPoints + 1), y: random.Next(1, countOfHeightPoints + 1));
                if (!snake.SnakeQueue.Contains(point))
                {
                    applePosition = point;
                    return point;
                }
            }
        }

    }
}