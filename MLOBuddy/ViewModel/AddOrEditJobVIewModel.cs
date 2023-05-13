using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLOBuddy.ViewModel
{
    [QueryProperty(nameof(CurrClient), "CurrClient")]
    [QueryProperty(nameof(StartIncome), "StartIncome")]
    [QueryProperty(nameof(CurrJob), "CurrJob")]
    public partial class AddOrEditJobVIewModel : ObservableObject
    {
        public AddOrEditJobVIewModel() 
        {
            JobTypes = new ObservableCollection<string> { "Nomina","Servicios Profesionales","Pension" };
        } 

        [ObservableProperty]
        private Job currJob;

        [ObservableProperty]
        private Client currClient;

        [ObservableProperty]
        private double startIncome;

        [ObservableProperty]
        private ObservableCollection<string> jobTypes;



        [RelayCommand]
        public async void SaveChanges()
        {
            if (CurrJob.id != 0)
            {

                int index = CurrClient.Jobs.IndexOf(CurrJob);
                CurrClient.Jobs[index].salary = CurrJob.salary;
                if (StartIncome != 0) { CurrClient.income -= StartIncome; }
                CurrClient.income += CurrClient.Jobs[index].salary;
            }
            else
            {
                Random rnd = new();
                CurrClient.income += CurrJob.salary;
                CurrJob.id = rnd.Next();
                CurrClient.Jobs.Add(CurrJob);
            }
            await Shell.Current.GoToAsync("..");
        }

    }
}

