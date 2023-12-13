namespace SnakesLaddersWedMorn;

public partial class SettingsPage : ContentPage
{
	Settings set;
	public SettingsPage(Settings s)
	{
		InitializeComponent();
		set = s;
		BindingContext = set;
	}

    private async void SaveBtn_Clicked(object sender, EventArgs e) {
		set.WritetoJson();
		await Navigation.PopAsync();
    }
}