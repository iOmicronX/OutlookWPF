using OutlookWPF.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace OutlookWPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand<string> _navigateCommand;
        private readonly IRegionManager _regionManager;

        //controllo se _navigatecommand è null, e se lo è crea una nuova istanza di Delegate Command e restituisce il
        //valore di _navigate Command: è un getter controllato scritto con la lambda
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));


        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        void ExecuteNavigateCommand(string navigationPath)
        {
            if(string.IsNullOrEmpty(navigationPath))
            {
                throw new ArgumentNullException();
            }

            //il metodo permette la navigazione (ndr. spostamento dell'utent e attraverso la visualizzazipne
            //di diverse schermate o viste
            //verso una determinata vista all'interno di una regione specifica, 
            //arg1: nome della regione, arg2: nome della vista da navigare
            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
    }
}
