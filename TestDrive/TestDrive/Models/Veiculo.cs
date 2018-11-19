using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{
    public class Veiculo
    {
        public const decimal FREIO_ABS = 800;
        public const decimal AR_CONDICIONADO = 1000;
        public const decimal MP3_PLAYER = 500;

        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("preco")]
        public decimal Preco { get; set; }
        public string PrecoFormatado
        {
            get { return $"R$ {Preco}"; }
        }

        public bool TemFreioABS { get; set; }
        public bool TemArCondicionado { get; set; }
        public bool TemMP3Player { get; set; }

        public string PrecoTotalFormatado
        {
            get {
                var Acessorios = TemFreioABS ? FREIO_ABS : 0;
                Acessorios += TemArCondicionado ? AR_CONDICIONADO : 0;
                Acessorios += TemMP3Player ? MP3_PLAYER : 0;
                return $"Valor Total: R$ {Preco + Acessorios}";

            }
        }

    }
}
