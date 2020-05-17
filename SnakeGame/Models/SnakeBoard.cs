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
        private (int MapSizeX, int MapSizeY) mapSize;

        public SnakeBoard(SnakeWindow window, int x, int y, int elementSize)
        {
            mapSize = (x, y);
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

        public void UpdateSnakePositionLogic(SnakeBoard snakeBoard)
        {
            VerifyIsAppleDrawOnMap();
            var isAppleWillBeEaten = IsAppleOnNextMove();
            var res = Snake.MoveSnake(Snake.ActualSnakeDirection, isAppleWillBeEaten);

            if (isAppleWillBeEaten)
            {
                GenerateApple(mapSize.MapSizeX, mapSize.MapSizeY);
            }

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

        public bool IsAppleOnNextMove()
        {
            switch (Snake.ActualSnakeDirection)
            {
                case DirectionFlag.Left:
                    return (Snake.SnakeHeadPosition.x, Snake.SnakeHeadPosition.y - 1) == Apple.applePosition;
                    break;
                case DirectionFlag.Right:
                    return (Snake.SnakeHeadPosition.x, Snake.SnakeHeadPosition.y + 1) == Apple.applePosition;
                    break;
                case DirectionFlag.Up:
                    return (Snake.SnakeHeadPosition.x + 1, Snake.SnakeHeadPosition.y) == Apple.applePosition;
                    break;
                case DirectionFlag.Down:
                    return (Snake.SnakeHeadPosition.x - 1, Snake.SnakeHeadPosition.y) == Apple.applePosition;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void VerifyIsAppleDrawOnMap()
        {
            foreach (var column in gameBoard)
            {
                foreach (var boardElement in column)
                {
                    if (boardElement.BoardValue != BoardObjectValue.Apple &&
                        boardElement.BoardObject.Fill == Brushes.Red)
                    {
                        switch (boardElement.BoardValue)
                        {
                            case BoardObjectValue.Free:
                                boardElement.BoardObject.Fill = null;
                                break;
                            case BoardObjectValue.Snake:
                                boardElement.BoardObject.Fill = Brushes.Green;
                                break;
                            case BoardObjectValue.Wall:
                                boardElement.BoardObject.Fill = Brushes.Black;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    if (boardElement.BoardValue == BoardObjectValue.Apple && boardElement.BoardObject.Fill == Brushes.Red)
                        return;
                }
            }

            var applePosition = Apple.applePosition;
            gameBoard[applePosition.x][applePosition.y].BoardValue = BoardObjectValue.Apple;
            gameBoard[applePosition.x][applePosition.y].BoardObject.Fill = Brushes.Red;
        }
    }
}
