

namespace SnakesLaddersWedMorn
{
    public class Player
    {
        private int position;
        private int row;
        private int col;
        private Image image;
        static public Grid grid;

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

        public async Task MoveVertically() {
            row--;
            double step = grid.Height / 12;
            await image.TranslateTo(0, step * -1, 250);
            image.TranslationY = 0;
            image.SetValue(Grid.RowProperty, row);
        }
    }
}
