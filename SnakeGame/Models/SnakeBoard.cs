using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SnakeGame.Views;

namespace SnakeGame.Models
{
    public class SnakeBoard
    {
        private List<List<BoardElement>> gameBoard;
        private Snake Snake;
        private Apple Apple;

        public SnakeBoard(SnakeWindow window, int x, int y, int elementSize)
        {
            SetInitialSizeOfWindow(window, x, y, elementSize);
            GenerateInitialMap(window, x, y, elementSize);
            GenerateInitialSnake(x,y);
            GenerateInitialApple(x, y);
        }

        private void SetInitialSizeOfWindow(SnakeWindow window, int x, int y, int elementSize)
        {
            window.Width = y * elementSize;
            window.Height = x * elementSize;

        }

        private void GenerateInitialMap(SnakeWindow window, int x, int y, int elementSize)
        {
            gameBoard = new List<List<BoardElement>>();
            for (int i = 0; i < x; i++)
            {
                gameBoard.Add(new List<BoardElement>());
                for (int j = 0; j < y; j++)
                {
                    gameBoard[i].Add(new BoardElement(elementSize));
                    Canvas.SetTop(gameBoard[i][j].BoardObject, i * elementSize);
                    Canvas.SetLeft(gameBoard[i][j].BoardObject, j * elementSize);
                    window.snakeCanvas.Children.Add(gameBoard[i][j].BoardObject);
                }
            }
        }
        public void GenerateInitialSnake(int x, int y)
        {
            this.Snake = new Snake(x, y);
            gameBoard[x / 2][y / 2].BoardObject.Fill = Brushes.SeaGreen;
            gameBoard[x / 2][y / 2].BoardValue = BoardObjectValue.Snake;
        }

        public void GenerateInitialApple(int x, int y)
        {
            Apple = new Apple(x, y, Snake);
            GenerateApple(x, y);
        }

        public void GenerateApple(int xMax, int yMax)
        {
            (int x, int y) newApplePosition = Apple.GenerateNewApplePosition(xMax, yMax, Snake);
            gameBoard[newApplePosition.x][newApplePosition.y].BoardValue = BoardObjectValue.Apple;
            gameBoard[newApplePosition.x][newApplePosition.y].BoardObject.Fill = Brushes.Red;
        }

        public DirectionFlag GetCurrentSnakeDirection()
        {
            return Snake.ActualSnakeDirection;
        }

        public void ChangeSnakeDirection(DirectionFlag newDirection)
        {
            if (Snake.ActualSnakeDirection != newDirection)
                switch (newDirection)
                {
                    case DirectionFlag.Left:
                        if (Snake.ActualSnakeDirection == DirectionFlag.Right)
                            return;
                        Snake.ActualSnakeDirection = DirectionFlag.Left;
                        break;
                    case DirectionFlag.Right:
                        if (Snake.ActualSnakeDirection == DirectionFlag.Left)
                            return;
                        Snake.ActualSnakeDirection = DirectionFlag.Right;
                        break;
                    case DirectionFlag.Up:
                        if (Snake.ActualSnakeDirection == DirectionFlag.Down)
                            return;
                        Snake.ActualSnakeDirection = DirectionFlag.Up;
                        break;
                    case DirectionFlag.Down:
                        if (Snake.ActualSnakeDirection == DirectionFlag.Up)
                            break;
                        Snake.ActualSnakeDirection = DirectionFlag.Down;
                        break;
                }
        }
    }
}
