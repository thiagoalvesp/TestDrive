using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TestDrive.Models;

namespace TestDrive.Data
{
    public class AgendamentoDAO
    {
        readonly SQLiteConnection Conexao;

        private List<Agendamento> lista;

        public List<Agendamento> Lista
        {
            get { return Conexao.Table<Agendamento>().ToList(); }
            private set { lista = value; }
        }


        public AgendamentoDAO(SQLiteConnection conexao)
        {
            Conexao = conexao;
            Conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (Conexao.Find<Agendamento>(agendamento.ID) == null)
            {
                Conexao.Insert(agendamento);
            }
            else
            {
                Conexao.Update(agendamento);
            }
            
        }
    }
}
