using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.Services;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        const string URL_GET_VEICULOS = @"http://aluracar.herokuapp.com/";

        private Veiculo veiculoSelecionado;

        public ObservableCollection<Veiculo> Veiculos { get; set; }

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


        public ListagemViewModel() => this.Veiculos = new ObservableCollection<Veiculo>();

        //public async Task GetVeiculos()
        //{
        //    Aguarde = true;
        //    try
        //    {
        //        HttpClient cliente = new HttpClient();

        //        var resultado = await cliente.GetStringAsync(URL_GET_VEICULOS);

        //        var veiculosJson = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

        //        foreach (var veiculoJson in veiculosJson)
        //        {
        //            this.Veiculos.Add(new Veiculo
        //            {
        //                Nome = veiculoJson.nome,
        //                Preco = veiculoJson.preco
        //            });
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        MessagingCenter.Send<Exception>(exc, "FalhaListagem");
        //    }

        //    Aguarde = false;
        //}

        //class VeiculoJson
        //{
        //    public string nome { get; set; }
        //    public int preco { get; set; }
        //}

        public async Task GetVeiculos()
        {
            Aguarde = true;

            //var cliente = new HttpClient();
            //var resultado = await cliente.GetStringAsync(URL_GET_VEICULOS);
            //var arrayVeiculos = JsonConvert.DeserializeObject<Veiculo[]>(resultado);
            //foreach (var veiculo in arrayVeiculos)
            //{
            //    Veiculos.Add(veiculo);
            //}

            //teste retrofit
            var aluraCarApi = RestService.For<IAluraCarApi>(URL_GET_VEICULOS);
            var resultado = await aluraCarApi.GetVeiculos();
            foreach (var veiculo in resultado)
            {
                this.Veiculos.Add(veiculo);
            }

            Aguarde = false;

        }



        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

    }
}
