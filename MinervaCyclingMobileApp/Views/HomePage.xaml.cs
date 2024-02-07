using MinervaCyclingMobileApp.ViewModels;

namespace MinervaCyclingMobileApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();

		this.BindingContext = vm;
	}
}