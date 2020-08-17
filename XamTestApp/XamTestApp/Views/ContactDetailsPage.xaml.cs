using System.ComponentModel;
using Xamarin.Forms;
using XamTestApp.ViewModels;

namespace XamTestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ContactDetailsPage : ContentPage
    {
        private readonly ContactViewModel _viewModel;

        public ContactDetailsPage(ContactViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }
    }
}