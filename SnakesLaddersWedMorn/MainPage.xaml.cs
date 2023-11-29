using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersWedMorn
{
    public partial class MainPage : ContentPage
    {
        private Color gridColour = Color.FromArgb("#2B0B98");
        private Random random;
        private Color DICE_COLOUR = Color.FromArgb("#000000");
        public MainPage() {
            InitializeComponent();
            CreatetheGrid();
            random = new Random();
        }

        private int whichPosition(int row, int col) {
            if(row % 2 == 0) {
                int start = 100 - row * 10;
                return start - col;
            }
            else {
                int start = (9 - row) * 10 + 1;
                return start + col;
            }
        }

        private LayoutOptions whichLayout(int row) {
            if (row % 2 == 0) return LayoutOptions.End;
            else return LayoutOptions.Start;
        }

        private void CreatetheGrid() {
            for (int i = 0; i < 10; ++i) {
                for (int j = 0; j < 10; ++j) {
                    Border border = new Border
                    {
                        StrokeThickness = 4,
                        Background = gridColour,
                        Padding = new Thickness(2, 2),
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        StrokeShape = new RoundRectangle
                        {
                            CornerRadius = new CornerRadius(4, 4, 4, 4)
                        },
                        Stroke = new LinearGradientBrush
                        {
                            EndPoint = new Point(0, 1),
                            GradientStops = new GradientStopCollection
                            {
                                new GradientStop { Color = Colors.Orange, Offset = 0.1f },
                                new GradientStop { Color = Colors.Brown, Offset = 1.0f }
                            },
                        },
                        Content = new Label
                        {
                            Text = whichPosition(i,j).ToString(),
                            TextColor = Colors.White,
                            FontSize = 10,
                            HorizontalOptions = whichLayout(i),
                            FontAttributes = FontAttributes.Bold
                        }
                    };
                    GameBoard.Add(border, j, i);
                }

            }
        }

        private void RollDice_Clicked(object sender, EventArgs e) {
            int roll = random.Next(1, 7);
           // RollLbl.Text = roll.ToString();
        }

        private Ellipse drawcircle() {
            Ellipse ell = new Ellipse()
            {
                Fill = DICE_COLOUR,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            return ell;
        }

        private void FillDiceGrid(int rollcount) {
            switch (rollcount) {
                case 1:

                    break;
                case 2:

                    break;
            }
        }
    }
}