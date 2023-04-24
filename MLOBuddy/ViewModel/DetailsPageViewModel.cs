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
    [QueryProperty(nameof(CurrClient),"Client")]
    public partial class DetailsPageViewModel : ObservableObject
    {
        public DetailsPageViewModel() 
        {
        }

        private PreQual currClient;  // Named this way to avoid clashing with Client class
        public PreQual CurrClient {  // Client data will not change in this page after being assigned by QueryProperty, so it doesn't require to be Observable
            get => currClient;
            set 
            {
                SetProperty(ref currClient, value);
                FavoriteString = CurrClient.favoriteString;  // Using this setter allows the favorite string to be set properly at construction.

            } 
        }

        [ObservableProperty]
        private string favoriteString;

        [RelayCommand]
        public async void DownloadLetter() 
        {
            try 
            { 
                await Browser.Default.OpenAsync("https://api.primerhogarpr.com:8443/download/" + CurrClient.id, BrowserLaunchMode.SystemPreferred); 
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
                PhoneDialer.Default.Open(CurrClient.clients[0].phoneNumber);
            }
        }
        [RelayCommand]
        public void SetFavorite()
        { 
            CurrClient.favorite = !CurrClient.favorite;
            FavoriteString = CurrClient.FavoriteString();
        }
    }
}
