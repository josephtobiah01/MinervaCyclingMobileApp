using MinervaCyclingMobileApp.ViewModels.SignUp;

namespace MinervaCyclingMobileApp.Views.SignUp;

public partial class ReviewDetailsPage : ContentPage
{
	public ReviewDetailsPage(ReviewDetailsPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}