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
        }

        private void placeladderonboard() {
            int Gridheight = StartRow - EndRow + 1;
            int Gridwidth = Math.Abs(EndCol - StartCol) + 1;
            double ystep = grid.Height / 12;
            double xstep = grid.Width / 10;
        }

        private void placesnakeonboard() {

        }

    }
}
