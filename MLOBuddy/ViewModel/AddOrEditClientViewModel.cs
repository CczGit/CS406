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
    [QueryProperty(nameof(CurrClient), "Client")]
    public partial class AddOrEditClientViewModel : ObservableObject
    {
        public AddOrEditClientViewModel()
        {
        }

        [ObservableProperty]
        private Client currClient;

        [RelayCommand]
        public void CallClient()
        {
            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(CurrClient.phoneNumber);
            }
        }
        [RelayCommand]
        async Task GoToAddOrEditDebt(Debt? CurrDebt)
        {
            if (CurrDebt == null)
            {
                CurrDebt = new();
            }
            Dictionary<string, object> NavigationParameters = new Dictionary<string, object> 
            { 
                { "CurrClient", CurrClient }, 
                { "StartPayment", CurrDebt.payment } ,
                { "CurrDebt", CurrDebt },
            };
            await Shell.Current.GoToAsync($"{nameof(AddOrEditDebt)}", NavigationParameters);
        }
        [RelayCommand]
        async Task GoToAddOrEditJob(Job? CurrJob)
        {
            if (CurrJob == null)
            {
                CurrJob = new();
            }
            Dictionary<string, object> NavigationParameters = new Dictionary<string, object> { { "CurrJob", CurrJob }, { "CurrTotalDebt", CurrClient.debt } };
            await Shell.Current.GoToAsync($"{nameof(AddOrEditJob)}", NavigationParameters);
        }
    }
}