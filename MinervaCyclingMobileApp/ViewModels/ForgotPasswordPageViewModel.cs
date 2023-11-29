﻿using MinervaCyclingMobileApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.ViewModels
{
    public class ForgotPasswordPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly INavigationService NavigationService;

        

        #endregion Fields

        #region Properties

        public Command GoBack {  get; set; }

        #endregion Properties

        #region Constructor

        public ForgotPasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;

            GoBack = new Command(GoBackCommand);
        }

        private void GoBackCommand(object obj)
        {
            NavigationService.GoBack();
        }


        #endregion Constructor

        #region Methods
        #endregion Methods
    }
}
