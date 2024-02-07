using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Views.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.ViewModels.SignUp
{
    public class EmailAndBdayPageViewModel : ViewModelBase
    {
        #region Fields

        private string _email;
        private DateTime _bday;
        private string _firstName;
        private string _lastName;
        private bool _isPopupOpen = false;
        private DateTime _selectedDate = DateTime.Now;
        private string _datePickerButtonText;


        #endregion Fields

        #region Properties

        public Command GoBack { get; set; }
        public Command NextPage { get; set; }
        public Command SelectedDateChanged { get; set; }
        public Command OpenCalendar { get; set; }
        public string Email
        {
            get { return _email; }
            set { SetPropertyValue(ref _email, value); }
        }

        public DateTime Bday
        {
            get { return _bday; }
            set { SetPropertyValue(ref _bday, value); }
        }

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
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { SetPropertyValue(ref _isPopupOpen, value); }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { SetPropertyValue(ref _selectedDate, value); }
        }
        public string DatePickerButtonText
        {
            get { return _datePickerButtonText; }
            set { SetPropertyValue(ref _datePickerButtonText, value); }
        }

        #endregion Properties

        #region Constructor

        public EmailAndBdayPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            

            GoBack = new Command(GoBackCommand);
            NextPage = new Command(NextPageCommand);
            SelectedDateChanged = new Command(SelectedDateChangedCommand);
            OpenCalendar = new Command(OpenCalendarCommand);
        }

        





        #endregion Constructor

        #region Methods

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
            return base.OnNavigatingTo(parameter);
        }

        private void GoBackCommand(object obj)
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
                parameters.Add("Email", Email);
                parameters.Add("Bday", Bday.ToString("MMM d, yyyy"));

                await NavigationService.NavigateTo("ReviewDetailsPage", parameters);
            }
            catch (Exception ex)
            {
                ShowAlert("Woops", $"There was an error: {ex.Message}");
                Console.WriteLine(ex.ToString());
            }
            
        }

        private void SelectedDateChangedCommand()
        {
            Bday = SelectedDate;
            SetDate(SelectedDate);
        }

        public Task SetDate(DateTime? DateInput)
        {

            string formattedDate = string.Empty;

            string numberSuffix = string.Empty;

            string monthShort = string.Empty;

            if (DateInput != null)
            {
                SelectedDate = DateInput.GetValueOrDefault();

                monthShort = SelectedDate.ToString("MMM");

                numberSuffix = GetDayNumberSuffix(SelectedDate);

                formattedDate = string.Format("{0} {1}, {2}", SelectedDate.Day + numberSuffix, monthShort, SelectedDate.DayOfWeek);

                DatePickerButtonText = formattedDate;

                IsPopupOpen = false;
            }


            return Task.CompletedTask;
        }

        private string GetDayNumberSuffix(DateTime date)
        {
            int day = date.Day;

            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";

                case 2:
                case 22:
                    return "nd";

                case 3:
                case 23:
                    return "rd";

                default:
                    return "th";
            }
        }

        private void OpenCalendarCommand()
        {
            IsPopupOpen = true;
        }

        #endregion Methods
    }
}
