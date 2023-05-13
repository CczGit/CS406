using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MLOBuddy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLOBuddy.ViewModel
{
    [QueryProperty(nameof(CurrClient), "CurrClient")]
    [QueryProperty(nameof(StartPayment), "StartPayment")]
    [QueryProperty(nameof(CurrDebt), "CurrDebt")]
    public partial class AddOrEditDebtViewModel : ObservableObject
    {
        public AddOrEditDebtViewModel()
        {
            DebtTypes = new ObservableCollection<string> { "Credit Card", "Auto Loan", "Personal Loan", "Student Loan", "Alimony", "Mortgage" };
            
        }

        [ObservableProperty]
        private Debt currDebt;
     
        [ObservableProperty]
        private Client currClient;

        [ObservableProperty]
        private ObservableCollection<string> debtTypes;

        [ObservableProperty]
        private double startPayment;

        [RelayCommand]
        public async void SaveChanges() 
        {
            if (CurrDebt.id != 0)
            {
                
                int index = CurrClient.Debts.IndexOf(CurrDebt);
                CurrClient.Debts[index].payment = CurrDebt.payment;
                if(StartPayment!= 0) { CurrClient.debt -= StartPayment; }
                CurrClient.debt += CurrClient.Debts[index].payment;
            }
            else
            {
                Random rnd = new();
                CurrClient.debt += CurrDebt.payment;
                CurrDebt.id = rnd.Next();
                CurrClient.Debts.Add(CurrDebt);
            }
            await Shell.Current.GoToAsync("..");
        }
    }


}
