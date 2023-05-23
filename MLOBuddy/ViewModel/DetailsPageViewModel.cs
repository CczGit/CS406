using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using MLOBuddy.Services;

namespace MLOBuddy.ViewModel
{
    [QueryProperty(nameof(CurrCase),"Client")]
    [QueryProperty(nameof(PreQuals), "PreQuals")]
    public partial class DetailsPageViewModel : ObservableObject
    {
        public DetailsPageViewModel() 
        {
        }

        private PreQual currCase;  // Named this way to avoid clashing with Client class
        public PreQual CurrCase
        {  // Client data will not change in this page after being assigned by QueryProperty, so it doesn't require to be Observable
            get => currCase;
            set 
            {
                SetProperty(ref currCase, value);
                FavoriteString = CurrCase.favoriteString;  // Using this setter allows the favorite string to be set properly at construction.
               
            } 
        }

        RestServices RestServices = new();

        [ObservableProperty]
        ObservableCollection<PreQual> preQuals;

        [ObservableProperty]
        private string favoriteString;

        [RelayCommand]
        public async void DownloadLetter() 
        {
            try 
            { 
                await Browser.Default.OpenAsync("https://api.primerhogarpr.com:8443/download/" + CurrCase.id, BrowserLaunchMode.SystemPreferred); 
            }
            catch (Exception ex)
            {
                //If no browser is installed on the device, this will print the error to console.
                Console.Write(ex.Message);
            }
        }
        [RelayCommand]
        public void CallClient()
        {
            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(CurrCase.Clients[0].phoneNumber);
            }
        }
        [RelayCommand]
        public void SetFavorite()
        { 
            CurrCase.favorite = !CurrCase.favorite;
            FavoriteString = CurrCase.FavoriteString();
        }
        [RelayCommand]
        public void DeleteClient(Client client)
        {
            CurrCase.Clients.Remove(client);
        }
        [RelayCommand]
        async Task EditClient(Client? client)
        {
            if (client == null)
            {
                client = new();
                if (CurrCase.Clients == null)
                {
                    CurrCase.Clients = new ObservableCollection<Client>();
                }
                CurrCase.Clients.Add(client);
            }
            Dictionary<string, object> NavigationParameters = new Dictionary<string, object> { { "Client", client }, };
            await Shell.Current.GoToAsync($"{nameof(AddOrEditClient)}", NavigationParameters);
        }
        [RelayCommand]
        async Task SaveChanges()
        {
            bool NewCase = false;
            if (CurrCase.id == null)
            {
                NewCase = true;
                Random random = new();
                CurrCase.id = random.Next().ToString();
            }
            CurrCase = CurrCase.PreQualify();
            RestServices.PostCase(CurrCase, NewCase);
            PreQuals.Clear();
            await Shell.Current.GoToAsync($"..");
        }
    }
}
