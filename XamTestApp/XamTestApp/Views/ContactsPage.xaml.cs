using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamTestApp.ViewModels;

namespace XamTestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ContactsPage : ContentPage
    {
        private readonly ContactsViewModel _viewModel;

        public ContactsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ContactsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Contacts.Count == 0)
            {
                _viewModel.LoadContactsCommand.Execute(null);
            }
        }
    }
}