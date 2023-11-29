namespace SnakesLaddersWedMorn
{
    public partial class MainPage : ContentPage
    {
        public MainPage() {
            InitializeComponent();
            CreatetheGrid();
        }

        private void CreatetheGrid() {
            for (int i = 0; i < 10; ++i) {
                for (int j = 0; j < 10; ++j) {
                    Frame styledFrame = new()
                    {
                        BackgroundColor = Color.FromRgb(255, 255, 255), // Set the background color
                        CornerRadius = 10, // Set rounded corners
                        HasShadow = false, // Add a shadow effect
                        Padding = new Thickness(0), // Add padding to the frame
                        BorderColor = Color.FromRgb(100, 100, 100)

                    };
                    GameBoard.Add(styledFrame, j, i);
                }

            }
        }
    }
}