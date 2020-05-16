using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Models
{
    public class Snake
    {
        public Queue<(int x, int y)> SnakeQueue;
        public (int x, int y) SnakeHeadPosition;
        public DirectionFlag ActualSnakeDirection;
        public Snake(int x, int y)
        {
            SnakeQueue = new Queue<(int x, int y)>();
            SnakeQueue.Enqueue((x / 2, y / 2));
            ActualSnakeDirection = DirectionFlag.Up;
            SnakeHeadPosition = SnakeQueue.Peek();
        }

        /// <summary>
        /// This function should have a previous verification of non colision with reverse move
        /// </summary>
        /// <returns>
        /// in result, we gain new, and old snake element
        /// (if apple is got ate, we shouldn't remove the last element, and method return (-1, -1) as not exsist value
        /// </returns>
        public ((int newSnakeX, int newSnakeY), (int oldSnakeX, int OldSnakeY)) MoveSnake(DirectionFlag direction, bool isAppleAte = false)
        {
            (int x, int y) newSnakeHeadPosition = (-1, -1);
            (int x, int y) snakeElementToDequeue = (-1, -1);

            switch (direction)
            {
                case DirectionFlag.Left:
                    newSnakeHeadPosition = (SnakeHeadPosition.x - 1, SnakeHeadPosition.y);
                    break;
                case DirectionFlag.Right:
                    newSnakeHeadPosition = (SnakeHeadPosition.x + 1, SnakeHeadPosition.y);
                    break;
                case DirectionFlag.Up:
                    newSnakeHeadPosition = (SnakeHeadPosition.x, SnakeHeadPosition.y + 1);
                    break;
                case DirectionFlag.Down:
                    newSnakeHeadPosition = (SnakeHeadPosition.x, SnakeHeadPosition.y - 1);
                    break;
            }

            SnakeQueue.Enqueue(newSnakeHeadPosition);
            SnakeHeadPosition = newSnakeHeadPosition;

            if (!isAppleAte)
                snakeElementToDequeue = SnakeQueue.Dequeue();
            
            return (newSnakeHeadPosition,snakeElementToDequeue);
        }
    }
}