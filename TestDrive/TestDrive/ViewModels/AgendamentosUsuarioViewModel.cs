using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TestDrive.Data;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentosUsuarioViewModel : BaseViewModel
    {
        ObservableCollection<Agendamento> lista = new ObservableCollection<Agendamento>();

        private Agendamento agendamentoSelecionado;

        public Agendamento AgendamentoSelecionado
        {
            get { return agendamentoSelecionado; }
            set {
                agendamentoSelecionado = value;
                try
                {
                    if (agendamentoSelecionado != null)
                        MessagingCenter.Send<Agendamento>(agendamentoSelecionado, "AgendamentoSelecionado");
                }
                catch (Exception ex)
                {
                }
               
            }
        }


        public AgendamentosUsuarioViewModel()
        {
            AtualizarLista();

        }

        public void AtualizarLista()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                AgendamentoDAO dao = new AgendamentoDAO(conexao);
                var listaDB = dao.Lista;
                var query =
                    listaDB.OrderBy(_ => _.DataAgendamento)
                    .ThenBy(_ => _.HoraAgendamento);

                Lista.Clear();
                foreach (var agendamento in query)
                {
                    Lista.Add(agendamento);
                }
            }
        }

        public ObservableCollection<Agendamento> Lista {
            get{
                return lista;
            }
            private set {
                lista = value;
                OnPropertyChanged();
            }
        }

       
    }
}
