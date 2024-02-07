using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiRequest;
using MinervaCyclingMobileApp.Models.ApiResponse;
using MinervaCyclingMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.ViewModels.SignUp
{
    public class ReviewDetailsPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly INavigationService _navigationService;
        private readonly ICreateContactService _createContactService;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _bday;
        

        #endregion Fields

        #region Properties

        public Command GoBack { get; set; }
        public Command CreateAccount { get; set; }

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
        public string Email
        {
            get { return _email; }
            set { SetPropertyValue(ref _email, value); }
        }

        public DateTime Bday
        {
            get { return _bday; }
            set
            {
                if (SetPropertyValue(ref _bday, value))
                {
                    OnPropertyChanged(nameof(FormattedBday));
                }
            }
        }

        public string FormattedBday
        {
            get { return _bday.ToString("MMM d, yyyy");}
        }

        #endregion Properties

        #region Constructor

        public ReviewDetailsPageViewModel(
            INavigationService navigationService,
            ICreateContactService createContactService) : base(navigationService)
        {
            _navigationService = navigationService;
            _createContactService = createContactService;
            

            GoBack = new Command(GoBackCommand);
            CreateAccount = new Command(CreateAccountCommand);
        }

        



        #endregion Constructor

        #region Methods

        private void GoBackCommand(object obj)
        {
            _navigationService.GoBack();
        }

        public override Task OnNavigatingTo(Dictionary<string, object> parameter)
        {
            if (parameter.TryGetValue("FirstName", out object firstName))
            {
                FirstName = firstName.ToString();
            }
            if (parameter.TryGetValue("LastName", out object lastName))
            {
                LastName = lastName.ToString();
            }
            if (parameter.TryGetValue("Email", out object email))
            {
                Email = email.ToString();
            }
            if (parameter.TryGetValue("Bday", out object bday))
            {
                if (DateTime.TryParseExact(bday.ToString(), "MMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bdate)) 
                {
                    Bday = bdate;
                }
            }
            return base.OnNavigatingTo(parameter);
        }

        private async void CreateAccountCommand(object obj)
        {
            CreateContactRequest createContactRequest = new CreateContactRequest();

            createContactRequest.FirstName = FirstName;
            createContactRequest.LastName = LastName;
            createContactRequest.Email = Email;
            createContactRequest.DateOfBirth = Bday;

            CreateContactResponse result = await _createContactService.CreateContact(createContactRequest);

            if (result.Status != false)
            {
                ShowAlert("Success", "We have created your account", "Ok");

                await _navigationService.NavigateTo(nameof(HomePage));
            }
            else
            {
                ShowAlert("Sorry", "Something went wrong, please try again in a bit");

                await _navigationService.NavigateTo(nameof(LoginPage));
            }

            

        }

        #endregion Methods

    }
}
