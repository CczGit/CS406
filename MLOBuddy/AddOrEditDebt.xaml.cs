using MLOBuddy.ViewModel;

namespace MLOBuddy;

public partial class AddOrEditDebt : ContentPage
{
	public AddOrEditDebt(AddOrEditDebtViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}