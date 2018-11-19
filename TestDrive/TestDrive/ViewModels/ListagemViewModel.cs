using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel
    {
        const string URL_GET_VEICULOS = @"http://aluracar.herokuapp.com/";

        private Veiculo veiculoSelecionado;

        public List<Veiculo> Veiculos { get; set; }

        public Veiculo VeiculoSelecionado
        {
            get { return veiculoSelecionado; }
            set
            {
                veiculoSelecionado = value;
                if (veiculoSelecionado != null)
                    MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");
            }
        }

   
        public ListagemViewModel() => this.Veiculos = new List<Veiculo>();

        public async Task GetVeiculos()
        {
            var cliente = new HttpClient();
            var resultado = await cliente.GetStringAsync(URL_GET_VEICULOS);
            var arrayVeiculos = JsonConvert.DeserializeObject<Veiculo[]>(resultado);
            this.Veiculos = null; 

        }
    }
}
