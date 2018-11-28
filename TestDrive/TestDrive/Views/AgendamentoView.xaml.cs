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

        protected  override  void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento", async (msg) =>
            {
                var agendar = await DisplayAlert("Salvar Agendamento", "Deseja mesmo enviar o agendamento?", 
                    "Sim", "Não");

                if (agendar)
                {

                    ViewModel.SalvarAgendamento();

                   //await DisplayAlert("Agendamento",
                   //$"Veiculo: {msg.Veiculo.Nome}" +
                   //$"\nNome: {msg.Nome}" +
                   //$"\nFone: {msg.Fone}" +
                   //$"\nE-mail: {msg.Email}" +
                   //$"\nData Agendamento: {msg.DataAgendamento.ToString("dd/MM/yyyy")}" +
                   //$"\nHora Agendamento: {msg.HoraAgendamento}",
                   //"OK");
                }
            });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", "Agendamento salvo com sucesso!", "OK");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", "Falha ao agendar o test drive!", "OK");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
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