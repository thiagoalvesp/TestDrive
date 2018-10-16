using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestDrive
{
    public class Veiculo
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string PrecoFormatado
        {
            get { return $"R$ {Preco}"; }
        }
    }

    public partial class MainPage : ContentPage
    {
        public List<Veiculo> Veiculos { get; set; }
        public MainPage()
        {
            InitializeComponent();

            Veiculos = new List<Veiculo>
            {
                new Veiculo{Nome= "Azera V6",Preco = 60000},
                new Veiculo{Nome= "Fiesta 2.0",Preco = 50000},
                new Veiculo{Nome= "HB20 S",Preco = 40000}
            };

            //ListViewVeiculos.ItemsSource = Veiculos;

            this.BindingContext = this;

        }

        private void ListViewVeiculos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var veiculo = (Veiculo)e.Item;

            DisplayAlert("Test Drive",
                $"Você tocou no modelo '{veiculo.Nome}', que custa {veiculo.PrecoFormatado}","OK");
        }
    }
}
