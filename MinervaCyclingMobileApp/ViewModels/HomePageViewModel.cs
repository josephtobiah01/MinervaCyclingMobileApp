using DevExpress.Maui.Controls;
using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiResponse;
using Timers = System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MinervaCyclingMobileApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region Fields

        private readonly IGetWarrantiesService _getWarrantiesService;
        private readonly IGetCertificatesService _getCertificatesService;
        private string _name;
        private long _externIdServiceBike;
        public bool _isVisible = false;
        private List<string> _warranties;
        private string _searchBarText;
        private List<string> _originalWarrantiesList;
        private BottomSheetState _openWarrantiesSheetState;
        public long _contactId;
        public bool _isWarrantiesVisible = true;
        public bool _isCertificatesVisible = false;

        #endregion Fields



        #region Properties

        Timers.Timer SearchTimer = null;

        public Command GetWarranties { get; set; }
        public Command GetCertificates { get; set; }
        public Command SelectWarranty { get; set; }

        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(ref _name, value); } 
        }

        public long ExternIdServiceBike
        {
            get { return _externIdServiceBike; }
            set { SetPropertyValue(ref _externIdServiceBike, value); }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetPropertyValue(ref _isVisible, value); }
        }

        public List<string> Warranties
        {
            get { return _warranties; }
            set { SetPropertyValue(ref _warranties, value); }
        }

        public List<string> OriginalWarrantiesList
        {
            get { return _originalWarrantiesList; }
            set { SetPropertyValue(ref _originalWarrantiesList, value); }
        }

        public BottomSheetState OpenWarrantiesSheetState
        {
            get { return _openWarrantiesSheetState; }
            set { SetPropertyValue(ref _openWarrantiesSheetState, value); }
        }

        public string SearchBarText
        {
            get { return _searchBarText; }
            set
            {
                SetPropertyValue(ref _searchBarText, value);
                SearchTimer?.Stop();
                SearchTimer?.Start();
            }
        }

        public long ContactId
        {
            get { return _contactId; }
            set { SetPropertyValue(ref _contactId, value); }
        }



        #endregion Properties



        #region Constructor

        public HomePageViewModel(
            INavigationService navigationService,
            IGetWarrantiesService getWarrantiesService,
            IGetCertificatesService getCertificatesService) : base(navigationService)
        {

            Initialize();
            
            _getWarrantiesService = getWarrantiesService;
            _getCertificatesService = getCertificatesService;

            GetWarranties = new Command(GetWarrantiesCommand);
            GetCertificates = new Command(GetCertificatesCommand);
            //SelectWarranty = new Command(SelectWarrantyCommand);
        }

        




        #endregion Constructor




        #region Methods

        public void Initialize()
        {
            OpenWarrantiesSheetState = BottomSheetState.Hidden;

            SearchTimer = new Timers.Timer();
            SearchTimer.Elapsed += SearchTimer_Elapsed;
            SearchTimer.Interval = 1200;
        }

        private void SearchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SearchCollection();
        }

        private void SearchCollection()
        {
            try
            {
                if (OriginalWarrantiesList == null) return;

                var searchEntered = SearchBarText;

                if (string.IsNullOrEmpty(searchEntered))
                {
                    foreach (var shop in OriginalWarrantiesList)
                    {
                        Warranties = new List<string>(OriginalWarrantiesList);
                    }
                }
                else
                {
                    searchEntered = searchEntered.ToLowerInvariant();

                    var filteredItems = OriginalWarrantiesList
                        .Where(x => x != null && x.ToLowerInvariant().Contains(searchEntered))
                        .ToList();


                    Warranties = new List<string>(filteredItems);

                }

            }
            catch (Exception ex)
            {
                ShowAlert("Woops", $"{ex.Message}");
            }
        }
        private async void GetWarrantiesCommand(object obj)
        {
            try
            {

                OpenWarrantiesSheetState = BottomSheetState.HalfExpanded;

                List<GetWarrantiesResponse> responses = await _getWarrantiesService.GetWarranties();

                OriginalWarrantiesList = new List<string>(responses.Select(x => x.Name));

                Warranties = new List<string>(OriginalWarrantiesList);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ShowAlert("Sorry", $"There is an error: {ex.Message}", "Ok");
            }
        }

        private void GetCertificatesCommand(object obj)
        {
            try
            {
                
                OpenWarrantiesSheetState = BottomSheetState.HalfExpanded;

                //List<GetCertificatesResponse> responses = await _getCertificatesService.GetCertificates(CancellationToken default);

                //OriginalWarrantiesList = new List<string>(responses.Select(x => x.Name));

                Warranties = new List<string>(OriginalWarrantiesList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ShowAlert("Sorry", $"There is an error: {ex.Message}", "Ok");
            }
        }

       


        #endregion Methods
    }
}
