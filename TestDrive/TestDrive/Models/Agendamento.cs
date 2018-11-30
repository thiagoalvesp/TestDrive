using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{
    public class Agendamento
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        [Ignore]
        public Veiculo Veiculo { get; set; }
        public string Nome { get;  set; }
        public string Fone { get;  set; }
        public string Email { get;  set; }
        public string Modelo { get;  set; }
        public decimal Preco { get;  set; }
        public bool Confirmado { get; set; }

        [Ignore]
        public string DataFormatada {
            get {
                return DataAgendamento.Add(HoraAgendamento).ToString("dd/MM/yyyy HH:mm");
            }
        }

        public Agendamento()
        {

        }
        public Agendamento(string nome, string fone, string email, 
            string modelo, decimal preco, DateTime dataAgendamento, TimeSpan horaAgendamento, bool confirmado, int id)
        {
            Nome = nome;
            Fone = fone;
            Email = email;
            Modelo = modelo;
            Preco = preco;
            DataAgendamento = dataAgendamento;
            HoraAgendamento = horaAgendamento;
            Confirmado = confirmado;
            ID = id;
        }

        private DateTime dataAgendamento = DateTime.Today;
        public DateTime DataAgendamento { get => dataAgendamento; set => dataAgendamento = value; }
        public TimeSpan HoraAgendamento { get; set; }


    }
}
