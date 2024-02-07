using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Views.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.ViewModels.SignUp
{
    public class NameAndDobPageViewModel : ViewModelBase
    {

        #region Fields

        //private readonly INavigationService _navigationService;
        private string _firstName;
        private string _lastName;

        #endregion Fields

        #region Properties


        public Command GoBack { get; set; }
        public Command NextPage { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set { SetPropertyValue(ref _firstName, value); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { SetPropertyValue(ref _lastName, value); }
        }





        #endregion Properties

        #region Constructor

        public NameAndDobPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            //_navigationService = navigationService;

            GoBack = new Command(GoBackCommand);
            NextPage = new Command(NextPageCommand);
        }

        



        #endregion Constructor

        #region Methods

        private void GoBackCommand()
        {
            NavigationService.GoBack();
        }

        private async void NextPageCommand()
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("FirstName", FirstName);
                parameters.Add("LastName", LastName);

                await NavigationService.NavigateTo("EmailAndBdayPage", parameters);
            }
            catch (Exception ex)
            {
                ShowAlert("Woops", $"There is an error: {ex.Message}");
                Console.WriteLine(ex.ToString());
            }
            
        }


        #endregion Methods

    }

}

