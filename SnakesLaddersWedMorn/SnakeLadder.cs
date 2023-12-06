using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Calls;

namespace SnakesLaddersWedMorn
{
    public class SnakeLadder
    {
        private int StartCol;
        private int EndCol;
        private int StartRow;
        private int EndRow;
        private Image image;
        static public Grid grid;

        public int[] EndPosition
        {
            get
            {
                int[] pos = new int[2];
                pos[0] = EndRow;
                pos[1] = EndCol;
                return pos;
            }

        }

        public SnakeLadder(int StartR, int EndR, int StartC, int EndC) {
            this.StartRow = StartR;
            this.EndRow = EndR;
            this.StartCol = StartC;
            this.EndCol = EndC;

            if (StartRow > EndRow)
                placeladderonboard();
            else if (StartRow < EndRow)
                placesnakeonboard();
        }

        private void CreatetheImage(double height, double width, string fileName) {
            image = new Image
            {
                Source = ImageSource.FromFile(fileName),
                WidthRequest = width,
                HeightRequest = height,
                Aspect = Aspect.Fill,
                ZIndex = 5
            };
            grid.Add(image);
        }

        private void placeladderonboard() {
            int Gridheight = StartRow - EndRow + 1;
            int Gridwidth = Math.Abs(EndCol - StartCol) + 1;
            double ystep = grid.HeightRequest / 12;
            double xstep = grid.WidthRequest / 10;
            if(Gridwidth == 1) {
                CreatetheImage(Gridheight * ystep - 20, xstep - 10, "ladder01.png");
            }
            else {
                double length = Math.Sqrt(Gridheight * Gridheight + Gridwidth * Gridwidth);
                CreatetheImage(length * ystep - 20, xstep - 10, "ladder01.png");
                double direction = 1.0;
                if (StartCol > EndCol)
                    direction = -1.0;
                double angle = Math.Atan(direction*Gridwidth / Gridheight);
                double degree = angle * 180 / Math.PI;
                image.Rotation = degree;
            }

            image.SetValue(Grid.RowProperty, EndRow);
            image.SetValue(Grid.RowSpanProperty, Gridheight);
            if( EndCol < StartCol ) 
                image.SetValue(Grid.ColumnProperty, EndCol);
            else
                image.SetValue(Grid.ColumnProperty, StartCol);
            image.SetValue(Grid.ColumnSpanProperty, Gridwidth);
        }

        private void placesnakeonboard() {

        }

        public bool IsStartPositionHere(int r, int c) {
            /* if (r == StartRow && c == StartCol)
                 return true;
             else
                 return false;*/
            return (r == StartRow && c == StartCol);
        }

    }
}
