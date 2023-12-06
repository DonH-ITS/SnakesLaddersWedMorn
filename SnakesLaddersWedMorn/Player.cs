

namespace SnakesLaddersWedMorn
{
    public class Player {
        private int position;
        private int row;
        private int col;
        private Image image;
        static public Grid grid;

        public int[] CurrentPosition{
            get
            {
                int[] pos = new int[2];
                pos[0] = row;
                pos[1] = col;
                return pos;
            }

        }

        public Player(Image img) {
            row = 9;
            col = 0;
            position = 1;
            this.image = img;
        }

        public async Task MoveThePlayer(int amount) {
            for(int i=0; i<amount; i++) {
                if(position % 10 == 0) {
                    await MoveVertically();
                }
                else {
                    await MoveHorizontally();
                }
                position++;

            }
        }

        public async Task MoveHorizontally() {
            int direction = 1;
            if(row%2 == 0) 
                direction = -1;
            col += direction;
            double step = grid.Width / 10;
            await image.TranslateTo(step*direction, 0, 250);
            image.TranslationX = 0;
            image.SetValue(Grid.ColumnProperty, col);
        }

        private async Task MoveVertically() {
            row--;
            double step = grid.Height / 12;
            await image.TranslateTo(0, step * -1, 250);
            image.TranslationY = 0;
            image.SetValue(Grid.RowProperty, row);
        }

        public async Task MovebySnakeLadder(int endr, int endc) {
            int height = endr - row;
            int width = endc - col; 
            row = endr;
            col = endc;

            double xstep = grid.Width / 10;
            double ystep = grid.Height / 12;
            await image.TranslateTo(xstep*width, ystep*height, 400);
            image.TranslationY = 0;
            image.TranslationX = 0;
            image.SetValue(Grid.RowProperty, row);
            image.SetValue(Grid.ColumnProperty, col);
            position = whichPosition(row, col);

        }

        static private int whichPosition(int row, int col) {
            if (row % 2 == 0) {
                int start = 100 - row * 10;
                return start - col;
            }
            else {
                int start = (9 - row) * 10 + 1;
                return start + col;
            }
        }
    }
}
