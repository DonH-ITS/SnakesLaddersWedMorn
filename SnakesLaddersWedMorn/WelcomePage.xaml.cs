namespace SnakesLaddersWedMorn;

public partial class WelcomePage : ContentPage
{
    private int noOfPlayers;

	public WelcomePage()
	{
		InitializeComponent();
        noOfPlayers = 2;
        noPlayersText.Text = noOfPlayers + " players selected";
    }

    private void stepPlayers_ValueChanged(object sender, ValueChangedEventArgs e) {
        noOfPlayers = (int)stepPlayers.Value;
        noPlayersText.Text = noOfPlayers + " players selected";
    }

    private void PlayGame_Clicked(object sender, EventArgs e) {
        Preferences.Default.Set("noofplayers", noOfPlayers);
        Preferences.Default.Set("screenwidth", this.Width);
        Shell.Current.GoToAsync("//MainPage");
    }
}