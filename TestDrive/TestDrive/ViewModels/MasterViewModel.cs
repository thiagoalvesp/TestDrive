using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TestDrive.Media;
using TestDrive.Views;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {

        public Usuario usuario { get; set; }
        public string Nome
        {
            get { return this.usuario.nome; }
            set { this.usuario.nome = value; }
        }

        public string DataNascimento
        {
            get { return this.usuario.dataNascimento; }
            set { this.usuario.dataNascimento = value; }
        }

        public string Telefone
        {
            get { return this.usuario.telefone; }
            set { this.usuario.telefone = value; }
        }


        public string Email
        {
            get { return this.usuario.email; }
            set { this.usuario.email = value; }
        }

        private bool editando = false;

        public bool Editando
        {
            get { return editando; }
            private set {
                editando = value;
                OnPropertyChanged();
            }
        }

        private ImageSource fotoPerfil = "perfil.png";

        public ImageSource FotoPerfil
        {
            get { return fotoPerfil;  }
            private set {
                fotoPerfil = value;
                OnPropertyChanged();
            }
        }


        public MasterViewModel(Usuario usuario)
        {
            this.usuario = usuario;
            EditarPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "EditarPerfil");
            });

            SalvarCommand = new Command(() =>
            {
                this.Editando = false;
                MessagingCenter.Send<Usuario>(usuario, "SucessoSalvarUsuario");
            });

            EditarCommand = new Command(() => {
                this.Editando = true;
            });

            TirarFotoCommand = new Command( async() =>
            {
                //Linha é utilizada para chamar funções dos projetos Android, IOS, etc...
                //Porém para Camera não deu certo.
                //DependencyService.Get<ICamera>().TirarFoto();
                try
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
                    {
                        MessagingCenter.Send<InvalidOperationException>(new InvalidOperationException("Nenhuma câmera detectada."), "CameraError");
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            SaveToAlbum = true,
                            Directory = "Demo"
                        });

                    if (file == null)
                        return;

                    FotoPerfil = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                }
                catch (MediaPermissionException ex)
                {
                    MessagingCenter.Send<InvalidOperationException>(
                        new InvalidOperationException("O aplicativo não tem permissão para utilizar a camera.",ex)
                        , "CameraError");
                }
                
            });

        }

        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand TirarFotoCommand { get; private set; }

    }
}
