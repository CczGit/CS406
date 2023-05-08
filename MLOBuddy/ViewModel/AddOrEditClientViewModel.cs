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

[QueryProperty(nameof(CurrClient), "Client")]
public partial class AddOrEditClientViewModel : ObservableObject
{
	public AddOrEditClientViewModel()
	{
	}

    private Client currClient;  // Named this way to avoid clashing with Client class
    public Client CurrClient
    {  // Client data will not change in this page after being assigned by QueryProperty, so it doesn't require to be Observable
        get => currClient;
        set
        {
            SetProperty(ref currClient, value);
            FirstName = CurrClient?.firstName;
            LastName = CurrClient?.lastName;
            PhoneNum = CurrClient?.phoneNumber;
            ClientEmail = CurrClient?.email;
        }
    }
    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _clientEmail;

    [ObservableProperty]
    private string _phoneNum;
}