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
        public BackgroundImage BackgroundImage { get; set; }
        public static Configuration Configuration { get; set; }
        public DispatcherTimer _timer;

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
            BackgroundImage = new BackgroundImage();
            Configuration = new Configuration();
            InitializeComponent();
                SnakeBoard = new SnakeBoard(this, 40, 40, 20);
            TimerInitialization();
        }

        private void TimerInitialization()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.3);
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

        private void SnakeWindow_OnActivation(object sender, EventArgs e)
        {
            _timer.Start();
        }

        private void SnakeWindow_OnDeactivation(object sender, EventArgs e)
        {
            _timer.Stop();
        }
    }
}
