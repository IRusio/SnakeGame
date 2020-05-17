using System;
using System.ComponentModel;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SnakeGame.Models;

namespace SnakeGame.Views
{
    /// <summary>
    /// Interaction logic for SnakeWindow.xaml
    /// </summary>
    public partial class SnakeWindow : Window
    {
        private static SnakeWindow Window { get; set; }
        public static SnakeBoard SnakeBoard { get; set; }
        private DispatcherTimer _timer;

        public static SnakeWindow GetSnakeWindow
        {
            get
            {
                if (Window == null)
                    Window = new SnakeWindow();
                return Window;
            }
        }

        private SnakeWindow()
        {
            InitializeComponent();
            SnakeBoard = new SnakeBoard(this, 30, 20, 20);
            //TODO: change this on normal image whos will be pinned to project.
            snakeCanvas.Background = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\jakub\\source\\repos\\SnakeGame\\SnakeGame\\Assets\\images\\Screenshot_1586838318.png", UriKind.Absolute)));
            TimerInitialization();
        }

        private void TimerInitialization()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.5);
            _timer.Tick += (sender, e) => GameLogicController(sender, e);
            _timer.Start();

        }

        private void GameLogicController(object sender, EventArgs e)
        {
            SnakeBoard.UpdateSnakePositionLogic(SnakeBoard);
        }

        private void SnakeWindow_OnClosed(object? sender, CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
            _timer.Stop();
        }

        private void SnakeMoveSwitch(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (SnakeBoard.GetCurrentSnakeDirection() != DirectionFlag.Up)
                        SnakeBoard.ChangeSnakeDirection(DirectionFlag.Up);
                    break;
                case Key.Down:
                    if (SnakeBoard.GetCurrentSnakeDirection() != DirectionFlag.Down)
                        SnakeBoard.ChangeSnakeDirection(DirectionFlag.Down);
                    break;
                case Key.Left:
                    if (SnakeBoard.GetCurrentSnakeDirection() != DirectionFlag.Left)
                        SnakeBoard.ChangeSnakeDirection(DirectionFlag.Left);
                    break;
                case Key.Right:
                    if (SnakeBoard.GetCurrentSnakeDirection() != DirectionFlag.Right)
                        SnakeBoard.ChangeSnakeDirection(DirectionFlag.Right);
                    break;
            }
        }
    }
}
