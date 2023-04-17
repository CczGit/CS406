using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MLOBuddy.Services;

namespace MLOBuddy.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel() 
        {
            PreQuals = new ObservableCollection<PreQual>();
            Originators = new ObservableCollection<string> { "Edward", "Rafael" };
        }

        private RestServices RestServices = new();

        [ObservableProperty]
        ObservableCollection<PreQual> preQuals;

        [ObservableProperty]
        ObservableCollection<string> originators;

        [ObservableProperty]
        string searchId;

        [ObservableProperty]
        string selectedOriginator;

        [RelayCommand]
        public async void Search() 
        {
            var result = await RestServices.GetPreQual(SearchId);
            PreQuals.Add(result);
            SearchId = string.Empty;
        }
    }
}
