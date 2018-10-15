using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestDrive
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ListViewVeiculos.ItemsSource = new string[] 
            {
                "Azera V6",
                "Fista 2.0",
                "HB20 S"
            };
        }
    }
}
