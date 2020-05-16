using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SnakeGame.Models;
using SnakeGame.Views;
using Xunit;

namespace SnakeGame_Tests
{
    public class SnakeMoveTests
    {
        [Fact]
        [STAThread]
        public void CorrectDirectionChange()
        {
            //Arrange
            var SnakeWindow = Mock.Of<SnakeWindow>();
            SnakeBoard board = new SnakeBoard(SnakeWindow, 20, 20, 20);

            //Act
            board.ChangeSnakeDirection(DirectionFlag.Left); 

            //Assert
            Assert.Equal(board.GetCurrentSnakeDirection(), DirectionFlag.Left);
        }

        [Fact]
        [STAThread]
        public void IncorrectDirectionChange()
        {
            //Arrange
            ISnakeWindow SnakeWindow = Mock.Of<ISnakeWindow>();
            SnakeBoard board = new SnakeBoard(SnakeWindow as SnakeWindow, 20, 20, 20);

            //Act
            board.ChangeSnakeDirection(DirectionFlag.Down);

            //Assert
            Assert.Equal(board.GetCurrentSnakeDirection(), DirectionFlag.Up);
        }
    }
}
