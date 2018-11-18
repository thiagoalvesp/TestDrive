using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheView : ContentPage
	{
        private const decimal FREIO_ABS = 800;
        private const decimal AR_CONDICIONADO = 1000;
        private const decimal MP3_PLAYER = 500;

        public string TextoFreioABS { get
            {
                 return $"Freio ABS - R$ {FREIO_ABS.ToString("N2")}";
            }
        }

        public string TextoArCondicionado
        {
            get
            {
                return $"Ar Condicionado - R$ {AR_CONDICIONADO.ToString("N2")}";
            }
        }
        public string TextoMP3Player
        {
            get
            {
                return $"MP3 Player - R$ {MP3_PLAYER.ToString("N2")}";
            }
        }



        public Veiculo Veiculo { get; set; }

        bool temFreioABS;
        public bool TemFreioABS {
            get {
                return temFreioABS;
            }
            set {
                temFreioABS = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        bool temArCondicionado;
        public bool TemArCondicionado
        {
            get
            {
                return temArCondicionado;
            }
            set
            {
                temArCondicionado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }


        bool temMP3Player;
        public bool TemMP3Player
        {
            get
            {
                return temMP3Player;
            }
            set
            {
                temMP3Player = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }


        public string ValorTotal
        {
            get
            {
                var Acessorios = temFreioABS ? FREIO_ABS : 0;
                Acessorios += temArCondicionado ? AR_CONDICIONADO : 0;
                Acessorios += temMP3Player ? MP3_PLAYER : 0;
                return $"Valor Total: R$ {Veiculo.Preco + Acessorios}";
            }
        }

        public DetalheView (Veiculo veiculo)
		{
			InitializeComponent ();
            this.Veiculo = veiculo;
            this.BindingContext = this;
		}

        private void ButtonProximo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgendamentoView(Veiculo));
        }
    }
}