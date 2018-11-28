using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterView : TabbedPage
	{
        public MasterView (Usuario usuario)
		{
			InitializeComponent ();
            this.BindingContext = new MasterViewModel(usuario);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Usuario>(this, "EditarPerfil",(usuario)=> {
                this.CurrentPage = Children[1];
            });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoSalvarUsuario", (usuario) => {
                this.CurrentPage = Children[0];
            });

            MessagingCenter.Subscribe<InvalidOperationException>(this, "CameraError", async (exp) => {
                await DisplayAlert("Ops", exp.Message, "OK");
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Usuario>(this, "EditarPefil");
            MessagingCenter.Unsubscribe<Usuario>(this, "SucessoSalvarUsuario");
            MessagingCenter.Unsubscribe<InvalidOperationException>(this, "CameraError");

        }
    }
}