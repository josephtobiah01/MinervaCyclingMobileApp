using CommunityToolkit.Mvvm.Messaging;
using MinervaCyclingMobileApp.ViewModels.WarrantyCertification;

namespace MinervaCyclingMobileApp.Views.WarrantyCertification;

public partial class CreateNewCertificationPage : ContentPage
{
    
    public CreateNewCertificationPage(CreateNewCertificationPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        if(this.BindingContext is CreateNewCertificationPageViewModel vm)
        {
            
            vm.SearchTextEnteredCommand();

        }

        SearchBar searchBar = (SearchBar)sender;
        searchBar.Unfocus();
    }
}