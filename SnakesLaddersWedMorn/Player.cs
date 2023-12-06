

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
    }
}
