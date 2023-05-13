using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLOBuddy.ViewModel
{
    [QueryProperty(nameof(CurrJob), "Job")]
    public partial class AddOrEditJobVIewModel : ObservableObject
    {
        public AddOrEditJobVIewModel() { } 

        [ObservableProperty]
        private Job currJob;
    }
}

