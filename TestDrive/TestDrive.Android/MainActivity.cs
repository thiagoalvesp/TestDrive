using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Plugin.Media;
using TestDrive.Media;
using Xamarin.Forms;

namespace TestDrive.Droid
{
    [Activity(Label = "TestDrive", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            await CrossMedia.Current.Initialize();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region Não funciona API 25 - Xamarin 3.4
        static Java.IO.File arquivoImagem;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            MessagingCenter.Send<Java.IO.File>(arquivoImagem, "TirarFoto");
        }

        public void TirarFoto()
        {
            try
            {
                //Intent
                Intent intent = new Intent(MediaStore.ActionImageCapture);

                Java.IO.File diretorio = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                            Android.OS.Environment.DirectoryPictures), "Imagens");

                if (!diretorio.Exists())
                    diretorio.Mkdir();

                arquivoImagem = new Java.IO.File(diretorio, "MinhaFoto.jpg");
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(arquivoImagem));
                ;
                //Activity
                var activity = Forms.Context as Activity;
                activity.StartActivityForResult(intent, 0);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}