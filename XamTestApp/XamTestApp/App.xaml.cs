using System.Threading;
using Xamarin.Forms;

namespace XamTestApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            // для форматирования даты
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
