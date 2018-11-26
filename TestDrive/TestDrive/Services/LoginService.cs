using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.Views;
using Xamarin.Forms;

namespace TestDrive.Services
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {
            using (var cliente = new HttpClient())
            {

                var camposFormulario = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("email", login.Email),
                    new KeyValuePair<string,string>("senha", login.Senha)
                });

                cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");
                var resultado = new HttpResponseMessage();
                try
                {

                    resultado = await cliente.PostAsync("/login", camposFormulario);
                }
                catch (Exception)
                {

                    MessagingCenter.Send<LoginException>(new LoginException("Ocorreu um erro de comunicação com o servidor.\nPor favor verifique a conexão e tente novamente mais tarde."), "FalhaLogin");
                }

                if (resultado.IsSuccessStatusCode)
                {
                    var conteudoResultado = await resultado.Content.ReadAsStringAsync();
                    var resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudoResultado);
                    MessagingCenter.Send<Usuario>(resultadoLogin.usuario, "SucessoLogin");
                }
                else
                {
                    MessagingCenter.Send<LoginException>(new LoginException("Usuário ou Senha incorretos."), "FalhaLogin");
                }


            }
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {

        }
    }

}
