using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Security;
using Javax.Security.Auth;
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
    [QueryProperty(nameof(index), "index")]
    public partial class AddOrEditDebtViewModel : ObservableObject
    {
        public AddOrEditDebtViewModel()
        {
            DebtTypes = new ObservableCollection<string> { "Credit Card", "Auto Loan", "Personal Loan", "Student Loan", "Alimony", "Mortgage" };
            StartPayment = CurrClient.debts[index].payment;
            CurrDebt = CurrClient.debts[index];
        }

        [ObservableProperty]
        private Debt currDebt;

        [ObservableProperty]
        private Client currClient;

        [ObservableProperty]
        private ObservableCollection<string> debtTypes;

        private int index;
        private decimal? StartPayment;

        [RelayCommand]
        public void SaveChanges() 
        {
            CurrClient.debts[index].payment = CurrDebt.payment;
            CurrClient.debt -= StartPayment;
            CurrClient.debt += CurrClient.debts[index].payment;

        }
    }


}
