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

        private readonly INavigationService NavigationService;

        #endregion Fields

        #region Properties


        public Command GoBack { get; set; }
        public Command NextPage { get; set; }





        #endregion Properties

        #region Constructor

        public NameAndDobPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;

            GoBack = new Command(GoBackCommand);
            NextPage = new Command(NextPageCommand);
        }

        



        #endregion Constructor

        #region Methods

        private void GoBackCommand()
        {
            NavigationService.GoBack();
        }

        private void NextPageCommand(object obj)
        {
            NavigationService.NavigateTo(nameof(EmailAndBdayPage));
        }


        #endregion Methods

    }

}

