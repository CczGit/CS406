using MLOBuddy.ViewModel;

namespace MLOBuddy;

public partial class AddOrEditJob : ContentPage
{
	public AddOrEditJob(AddOrEditJobVIewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}