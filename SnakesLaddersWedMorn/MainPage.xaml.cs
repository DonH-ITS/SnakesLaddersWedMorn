﻿using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersWedMorn
{
    public partial class MainPage : ContentPage
    {
        private Color gridColour = Color.FromArgb("#2B0B98");
        public MainPage() {
            InitializeComponent();
            CreatetheGrid();
        }

        private void CreatetheGrid() {
            for (int i = 0; i < 10; ++i) {
                for (int j = 0; j < 10; ++j) {
                    Border border = new Border
                    {
                        StrokeThickness = 4,
                        Background = gridColour,
                        Padding = new Thickness(4, 4),
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
                            Text = "1",
                            TextColor = Colors.White,
                            FontSize = 18,
                            FontAttributes = FontAttributes.Bold
                        }
                    };
                    GameBoard.Add(border, j, i);
                }

            }
        }
    }
}