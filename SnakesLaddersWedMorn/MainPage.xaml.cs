using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersWedMorn
{
    public partial class MainPage : ContentPage
    {
        private Color gridColour = Color.FromArgb("#2B0B98");
        private Random random;
        private Color DICE_COLOUR = Color.FromArgb("#000000");
        private bool diceisrolling = false;

        public bool DiceIsRolling
        {
            get => diceisrolling;
            set
            {
                if (diceisrolling == value)
                    return;
                diceisrolling = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NotDiceIsRolling));
            }
        }

        public bool NotDiceIsRolling => !DiceIsRolling;

        public MainPage() {
            InitializeComponent();
            CreatetheGrid();
            BindingContext = this;
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
                        StrokeThickness = 2,
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

        private async void RollDice_Clicked(object sender, EventArgs e) {
            if (diceisrolling)
                return;
            diceisrolling = true;
            await RolltheDice();
            diceisrolling = false; 
           // RollLbl.Text = roll.ToString();
        }

        private async Task RolltheDice() {
            int numberofrolls = random.Next(4, 10);
            int roll;
            int lastroll = 0;
            BorderDice.RotationY = 0;
            for (int i = 0; i < numberofrolls; i++) {
                do {
                    roll = random.Next(1, 7);
                } while (roll == lastroll);
                lastroll = roll;
                await BorderDice.RotateYTo(BorderDice.RotationY + 90, 150);
                CleartheDiceGrid(DiceGrid);
                FillDiceGrid(roll, DiceGrid);
                await BorderDice.RotateYTo(BorderDice.RotationY + 90, 150);
            }
        }

        
        private static void CleartheDiceGrid(Grid grid) {
            List<View> childrenToRemove = new();
            foreach (var item in grid.Children) {
                if (item.GetType() == typeof(Ellipse)) {
                    childrenToRemove.Add((Ellipse)item);
                }
            }

            //Actually remove them from the Grid
            foreach (var item in childrenToRemove) {
                grid.Remove(item);
            }
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

        private void FillDiceGrid(int rollcount, Grid grid) {
            switch (rollcount) {
                case 1:
                    grid.Add(drawcircle(), 1, 1);
                    break;
                case 2:
                    grid.Add(drawcircle(), 0, 0);
                    grid.Add(drawcircle(), 2, 2);
                    break;
                case 3:
                    for(int i=0; i<3; ++i) {
                        grid.Add(drawcircle(), i, i);
                    }
                    break;
                case 4:
                    for (int j = 0; j < 3; j += 2) {
                        for (int k = 0; k < 3; k += 2) {
                            grid.Add(drawcircle(), j, k);
                        }
                    }
                    break;
                case 5:
                    for (int j = 0; j < 3; j += 2) {
                        for (int k = 0; k < 3; k += 2) {
                            grid.Add(drawcircle(), j, k);
                        }
                    }
                    grid.Add(drawcircle(), 1, 1);
                    break;
                case 6:
                    for (int j = 0; j < 3; j += 2) {
                        for (int k = 0; k < 3; ++k) {
                            grid.Add(drawcircle(), k, j);
                        }
                    }
                    break;
            }
        }
    }
}