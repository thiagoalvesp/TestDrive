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
        public AgendamentoDAO(SQLiteConnection conexao)
        {
            Conexao = conexao;
            Conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            Conexao.Insert(agendamento);
        }
    }
}
