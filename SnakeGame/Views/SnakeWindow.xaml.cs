using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnakeGame.Views
{
    /// <summary>
    /// Interaction logic for SnakeWindow.xaml
    /// </summary>
    public partial class SnakeWindow : Window
    {
        private static SnakeWindow Window { get; set; }
        private static int x = 300;
        private static int y = 200;


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
        }

        private void GenerateGameBoard(int x, int y)
        {
            List<List<Canvas>> gameBoard;
            //https://stackoverflow.com/questions/4038044/how-can-i-create-rectangles-in-wpf-dynamically
        }





    }
}
