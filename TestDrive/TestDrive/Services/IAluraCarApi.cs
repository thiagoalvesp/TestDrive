using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;

namespace TestDrive.Services
{
    interface IAluraCarApi
    {
        [Get("")]
        Task<ObservableCollection<Veiculo>> GetVeiculos();
    }
}
