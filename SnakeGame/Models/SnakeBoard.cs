using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SnakeGame.Views;

namespace SnakeGame.Models
{
    public class SnakeBoard
    {
        private SnakeWindow Window;
        private List<List<BoardElement>> gameBoard;
        private Snake Snake;
        private Apple Apple;
        private (int MapSizeX, int MapSizeY) mapSize;

        public SnakeBoard(SnakeWindow window, int x, int y, int elementSize)
        {
            Window = window;
            mapSize = (x, y);
            SetInitialSizeOfWindow(window, x, y, elementSize);
            GenerateInitialMap(window, x, y, elementSize);
            GenerateInitialSnake(x,y);
            GenerateInitialApple(x, y);
        }

        public void RestartGame()
        {
            foreach (var column in gameBoard)
            {
                foreach (var boardElement in column)
                {
                    boardElement.BoardValue = BoardObjectValue.Free;
                    boardElement.BoardObject.Fill = null;
                }
            }
            Snake.SnakeQueue.Clear();

            DrawWallOnMap();
            GenerateInitialSnake(mapSize.MapSizeX, mapSize.MapSizeY);
            GenerateInitialApple(mapSize.MapSizeX, mapSize.MapSizeY);
        }


        private void SetInitialSizeOfWindow(SnakeWindow window, int x, int y, int elementSize)
        {
            window.Width = (y+2) * (elementSize+1);
            window.Height = (x+2) * (elementSize+1);

        }

        private void GenerateInitialMap(SnakeWindow window, int x, int y, int elementSize)
        {
            gameBoard = new List<List<BoardElement>>();

           
            for (int i = 0; i < x+2; i++)
            {
                gameBoard.Add(new List<BoardElement>());

                for (int j = 0; j < y + 2; j++)
                {
                    gameBoard[i].Add(new BoardElement(elementSize));
                    Canvas.SetTop(gameBoard[i][j].BoardObject, i * elementSize);
                    Canvas.SetLeft(gameBoard[i][j].BoardObject, j * elementSize);
                    window.snakeCanvas.Children.Add(gameBoard[i][j].BoardObject);
                }
            }

            DrawWallOnMap();
        }

        private void DrawWallOnMap()
        {
            for (int i = 0; i < gameBoard.Count; i++)
            {
                for (int j = 0; j < gameBoard[i].Count; j++)
                {
                    if (i == 0 || j == 0 || i == gameBoard.Count - 1 || j == gameBoard[i].Count - 1)
                    {
                        gameBoard[i][j].BoardObject.Fill = Brushes.Black;
                        gameBoard[i][j].BoardValue = BoardObjectValue.Wall;
                    }
                }
            }
        }

        private void GenerateInitialSnake(int x, int y)
        {
            this.Snake = new Snake(x, y);
            gameBoard[x / 2][y / 2].BoardObject.Fill = Brushes.SeaGreen;
            gameBoard[x / 2][y / 2].BoardValue = BoardObjectValue.Snake;
        }

        private void GenerateInitialApple(int x, int y)
        {
            Apple = new Apple(x, y, Snake);
            GenerateApple(x, y);
        }

        private void GenerateApple(int xMax, int yMax)
        {
            generateNewBackgroundImage();

            (int x, int y) newApplePosition = Apple.GenerateNewApplePosition(xMax, yMax, Snake);
            gameBoard[newApplePosition.x][newApplePosition.y].BoardValue = BoardObjectValue.Apple;
            gameBoard[newApplePosition.x][newApplePosition.y].BoardObject.Fill = Brushes.Red;
        }

        private void generateNewBackgroundImage()
        {
            var threadStart = new Thread(() => Window.BackgroundImage.setNewBackgroundImage(Window));
            threadStart.Start();
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

        public void UpdateSnakePositionLogic(SnakeBoard snakeBoard)
        {
            var isAppleWillBeEaten = IsAppleOnNextMove();
            var res = Snake.MoveSnake(Snake.ActualSnakeDirection, isAppleWillBeEaten);

            if (isAppleWillBeEaten)
            {
                GenerateApple(mapSize.MapSizeX, mapSize.MapSizeY);
            }

            if (DetectNotCorrectCollision())
                RestartGame();

            if (res.Item1.newSnakeX != -1)
            {
                
                gameBoard[res.Item1.newSnakeX][res.Item1.newSnakeY].BoardValue = BoardObjectValue.Snake;
                gameBoard[res.Item1.newSnakeX][res.Item1.newSnakeY].BoardObject.Fill = Brushes.Green;
            }

            if (res.Item2.oldSnakeX != -1)
            {
                gameBoard[res.Item2.oldSnakeX][res.Item2.OldSnakeY].BoardValue = BoardObjectValue.Free;
                gameBoard[res.Item2.oldSnakeX][res.Item2.OldSnakeY].BoardObject.Fill = null;
            }
        }

        private bool IsAppleOnNextMove()
        {
            return Apple.applePosition == Snake.SnakeHeadPosition;
        }


        public bool DetectNotCorrectCollision()
        {
            var head = Snake.SnakeHeadPosition;
            var direction = Snake.ActualSnakeDirection;
            return gameBoard[head.x][head.y].BoardValue == BoardObjectValue.Snake ||
                     gameBoard[head.x][head.y].BoardValue == BoardObjectValue.Wall;
        }
    }
}
