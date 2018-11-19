using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentoView : ContentPage
    {
        public AgendamentoViewModel ViewModel { get; set; }

        public AgendamentoView(Veiculo veiculo)
        {
            InitializeComponent();
            this.ViewModel = new AgendamentoViewModel(veiculo);
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento", (msg) =>
            {
                DisplayAlert("Agendamento",
                $"Veiculo: {msg.Veiculo.Nome}" +
                $"\nNome: {msg.Nome}" +
                $"\nFone: {msg.Fone}" +
                $"\nE-mail: {msg.Email}" +
                $"\nData Agendamento: {msg.DataAgendamento.ToString("dd/MM/yyyy")}" +
                $"\nHora Agendamento: {msg.HoraAgendamento}",
                "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");
        }

        //private void Button_Clicked(object sender, EventArgs e) => DisplayAlert("Agendamento",
        //    $"Veiculo: {ViewModel.Agendamento.Veiculo.Nome}" +
        //    $"\nNome: {ViewModel.Agendamento.Nome}" +
        //    $"\nFone: {ViewModel.Agendamento.Fone}" +
        //    $"\nE-mail: {ViewModel.Agendamento.Email}" +
        //    $"\nData Agendamento: {ViewModel.Agendamento.DataAgendamento.ToString("dd/MM/yyyy")}" +
        //    $"\nHora Agendamento: {ViewModel.Agendamento.HoraAgendamento}",
        //    "OK");
    }
}