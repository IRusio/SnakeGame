using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Models
{
    class BoardElement
    {
        public Ellipse BoardObject { get; set; }
        public BoardObjectValue BoardValue { get; set; }

        public BoardElement(int elementSize)
        {
            BoardObject = new Ellipse
            {
                Fill = null,
                Width = elementSize,
                Height = elementSize
            };
        }
    }
}