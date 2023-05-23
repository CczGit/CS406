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

        [RelayCommand]
        public async void LoadOriginator()
        {
            PreQuals.Clear(); // Clear out the prequalifications that might have been loaded already
            PreQual[] res;
            switch (SelectedOriginator) 
            { 
                case "Rafael":
                    res = await RestServices.GetOriginator("661");
                    break;
                case "Edward":
                    res = await RestServices.GetOriginator("660");  
                    break;
                default: // Do nothing if an originator is not selected.
                    return;
            }

            foreach (PreQual client in res) 
            { 
                if (client.hidden)
                {
                    continue;
                }
                PreQuals.Add(client);
            }
        }
        [RelayCommand]
        public void ShowFavorites()
        {
            ObservableCollection<PreQual> all_clients = new(PreQuals);
            PreQuals.Clear();
            foreach (PreQual client in all_clients)
            {
                if(!client.favorite)
                {
                    continue;
                }
                PreQuals.Add(client);
            }
        }
        [RelayCommand]
        public void SetFavorite(PreQual client)
        {
            client.favorite = !client.favorite; 
            RestServices.PostCase(client, false);
        }
        [RelayCommand]
        public void SetHidden(PreQual client)
        {
            client.hidden = !client.hidden;
            if (PreQuals.Contains(client)) { PreQuals.Remove(client); }
            RestServices.PostCase(client, false);
        }
        [RelayCommand]
        async Task ViewDetails(PreQual? Client)
        {
            if (Client == null)
            {
                Client = new();
                
            }
            Dictionary<string, object> NavigationParameters = new Dictionary<string, object> { { "Client", Client }, { "PreQuals", PreQuals } };
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", NavigationParameters);
        }
    }
}
