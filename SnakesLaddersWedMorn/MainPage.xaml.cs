using System.Text.Json;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace SnakesLaddersWedMorn
{
    public partial class MainPage : ContentPage
    {
        private Color gridColour = Color.FromArgb("#2B0B98");
        private Random random;
        private Color DICE_COLOUR = Color.FromArgb("#000000");
        private bool diceisrolling = false;
        private List<Player> playerList;
        private List<SnakeLadder> snakeLadderList;
        private int whichPlayerTurn;
        private Settings set;
        private bool fromSettingsPage = false;

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
            InitialiseAllVariables();
            BindingContext = this;
            
        }
        private void InitialiseAllVariables() {
            string filename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "settings.json");
            if (File.Exists(filename)) {
                try {
                    using (StreamReader reader = new StreamReader(filename)) {
                        string jsonstring = reader.ReadToEnd();
                        set = JsonSerializer.Deserialize<Settings>(jsonstring);
                    }
                }
                catch {
                    set = new Settings();
                }
            }
            else {
                set = new Settings();
            }
            UpdateSettings();
            CreatetheGrid();
            random = new Random();
            SetUpThePlayers(1);
            PlaceSnakeLadders();
        }

        private void UpdateSettings() {
            Resources["GridColour1"] = Color.FromArgb(set.GRID_COLOUR1);
            Resources["GridColour2"] = Color.FromArgb(set.GRID_COLOUR2);
        }

        private void PlaceSnakeLadders() {
            SnakeLadder.grid = GameBoard;
            snakeLadderList = new List<SnakeLadder>();
            snakeLadderList.Add(new SnakeLadder(9, 7, 6, 6));

            snakeLadderList.Add(new SnakeLadder(7, 5, 0, 1));

            snakeLadderList.Add(new SnakeLadder(5, 3, 9, 6));

            //Straight snakes
            snakeLadderList.Add(new SnakeLadder(0, 5, 2, 2));

            snakeLadderList.Add(new SnakeLadder(8, 9, 2, 2));

            //4x3 snakes
            snakeLadderList.Add(new SnakeLadder(0, 3, 4, 6));
            snakeLadderList.Add(new SnakeLadder(3, 6, 9, 7));

            //3x2 snakes
            snakeLadderList.Add(new SnakeLadder(5, 7, 4, 3));
            snakeLadderList.Add(new SnakeLadder(7, 9, 1, 2));

            //Diagonal snakes
            snakeLadderList.Add(new SnakeLadder(7, 8, 6, 5));
            snakeLadderList.Add(new SnakeLadder(0, 1, 1, 2));
        }

        private void SetUpThePlayers(int amount) {
            playerList = new List<Player>();
            whichPlayerTurn = 0;
            Player.grid = GameBoard;
            for(int i= 0; i < amount; i++) {
                playerList.Add(new Player(Player1Piece));
            }
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
                    if(whichPosition(i,j) % 2 == 0)
                        border.SetDynamicResource(Border.BackgroundColorProperty, "GridColour1");
                    else
                        border.SetDynamicResource(Border.BackgroundColorProperty, "GridColour2");
                    /*
                      border.SetDynamicResource(Border.BackgroundColorProperty, whichPosition(i,j) % 2 == 0 ? "GridColour1" : GridColour2");
                    */
                    GameBoard.Add(border, j, i);
                }
            }
        }

        private async void RollDice_Clicked(object sender, EventArgs e) {
            if (DiceIsRolling)
                return;
            DiceIsRolling = true;
            await RolltheDice();
            DiceIsRolling = false; 
           // RollLbl.Text = roll.ToString();
        }

        private async Task RolltheDice() {
            int numberofrolls = random.Next(4, 10);
            int roll = 0;
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
            await playerList[whichPlayerTurn].MoveThePlayer(roll);

            int[] curpos = playerList[whichPlayerTurn].CurrentPosition;
            foreach(var boardpiece in snakeLadderList) {
                if(boardpiece.IsStartPositionHere(curpos[0], curpos[1])) {
                    await playerList[whichPlayerTurn].MovebySnakeLadder(boardpiece.EndPosition[0], boardpiece.EndPosition[1]);
                }
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

        private async void Settings_Clicked(object sender, EventArgs e) {
            fromSettingsPage = true;
            await Navigation.PushAsync(new SettingsPage(set));
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args) {
            if (fromSettingsPage) {
                UpdateSettings();
                fromSettingsPage = false;
            }
            base.OnNavigatedTo(args);
        }
    }
}