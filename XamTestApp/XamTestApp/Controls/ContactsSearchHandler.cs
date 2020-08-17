using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamTestApp.Helpers;
using XamTestApp.ViewModels;

namespace XamTestApp.Controls
{
    public class ContactsSearchHandler : SearchHandler
    {
        private ObservableCollection<ContactViewModel> _contacts;
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _contacts = ItemsSource as ObservableCollection<ContactViewModel>;
        }
        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else if (!newValue.Equals(oldValue, StringComparison.OrdinalIgnoreCase))
            {
                ItemsSource = await Task.Run(() => _contacts.Where(p => newValue.FoundInAny(p.Name, p.PhoneDigits)));
            }
        }

        protected override void OnItemSelected(object item)
        {
            var contact = (ContactViewModel)item;
            contact.OpenContactDetailsCommand.Execute(null);
        }
    }
}
