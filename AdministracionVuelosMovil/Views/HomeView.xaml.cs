using AdministracionVuelosMovil.Models;
using AdministracionVuelosMovil.ViewModels;

namespace AdministracionVuelosMovil.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		var v = ((Border)sender).BindingContext;

        HomeViewModel viewmodel = (HomeViewModel)this.BindingContext;

		viewmodel.VerInfoCommand.Execute(v);

    }
}