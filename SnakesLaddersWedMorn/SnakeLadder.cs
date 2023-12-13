
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
            int Gridheight = Math.Abs(StartRow - EndRow) + 1;
            int Gridwidth = Math.Abs(StartCol - EndCol) + 1;
            double xStep = grid.WidthRequest / 10;
            double yStep = grid.HeightRequest / 12;

            if (Gridheight == 4 && Gridwidth == 3) {
                place4x3snake(xStep, yStep);
            }
            else if (Gridheight == 3 && Gridwidth == 2) {
                place3x2snake(xStep, yStep);
            }
            else {
                //Diagonal or straight Snakes, similar code to ladders
                placeothersnake(xStep, yStep, Gridwidth, Gridheight);
            }
        }

        private void place4x3snake(double xStep, double yStep) {
            CreatetheImage(4 * (yStep - 5), 3 * (xStep - 5), "snake3.png");
            if (StartCol < EndCol) {
                image.SetValue(Grid.ColumnProperty, StartCol);
            }
            else {
                image.SetValue(Grid.ColumnProperty, EndCol);
                image.RotationY = 180;
            }
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, 4);
            image.SetValue(Grid.ColumnSpanProperty, 3);
        }

        private void place3x2snake(double xStep, double yStep) {
            CreatetheImage(3 * (yStep - 5), 2 * (xStep - 5), "snake2.png");
            if (StartCol < EndCol) {
                image.SetValue(Grid.ColumnProperty, StartCol);
            }
            else {
                image.SetValue(Grid.ColumnProperty, EndCol);
                image.RotationY = 180;
            }
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, 3);
            image.SetValue(Grid.ColumnSpanProperty, 2);
        }

        private void placeothersnake(double xStep, double yStep, int width, int height) {
            //Pythagoras Theorem
            if (width == 1) {
                CreatetheImage(yStep * height - 10, xStep - 5, "snake1.png");
            }
            else {
                double endHeight = Math.Sqrt(width * width + height * height);
                CreatetheImage(endHeight * yStep - 20, xStep - 20, "snake1.png");
                double direction = 1.0;
                if (StartCol < EndCol)
                    direction = -1.0;
                double tan = direction * width / height;
                double radian = Math.Atan(tan);
                double degrees = radian * 180 / Math.PI;
                image.Rotation = degrees;
            }
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, height);
            if (StartCol < EndCol)
                image.SetValue(Grid.ColumnProperty, StartCol);
            else
                image.SetValue(Grid.ColumnProperty, EndCol);
            image.SetValue(Grid.ColumnSpanProperty, width);
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
