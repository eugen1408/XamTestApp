using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamTestApp.Views;
using XamTestAppDataLibrary;
using XamTestAppDataLibrary.Models;

namespace XamTestApp.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        #region Fields & Properties
        private readonly Contact _contact;
        private readonly Lazy<string> _phoneDigits;
        public Command OpenContactDetailsCommand { get; set; }
        public Command OpenPhoneDialerCommand { get; set; }

        public string PhoneDigits => _phoneDigits.Value;
        public string Name => _contact.Name;
        public string Phone => _contact.Phone;
        public float Height => _contact.Height;
        public Temperament Temperament => _contact.Temperament;


        // в некоторых объектах Start > End, вопрос, насколько это корректно и надо ли как-то обрабатывать в рамках теста
        public string EducationPeriod => _contact.EducationPeriod is null ? "Период не указан" :
            $"{_contact.EducationPeriod.Start.DateTime.ToShortDateString()} - {_contact.EducationPeriod.End.DateTime.ToShortDateString()}";

        public string Biography => _contact.Biography; 
        #endregion
        public ContactViewModel(Contact contact)
        {
            _contact = contact ?? throw new ArgumentNullException(nameof(contact));
            OpenContactDetailsCommand = new Command(async () => await ExecuteOpenContactDetailsCommand());
            OpenPhoneDialerCommand = new Command(async () => await ExecuteOpenPhoneDialerCommand());
            _phoneDigits = new Lazy<string>(GetPhoneDigits);
        }

        private string GetPhoneDigits()
        {
            var phoneArray = _contact.Phone.ToCharArray();
            phoneArray = Array.FindAll(phoneArray, (p => char.IsDigit(p)));
            var phoneDigits = new string(phoneArray);
            return phoneDigits;
        }

        #region Commands

        private async Task ExecuteOpenContactDetailsCommand() =>
            await Shell.Current.Navigation.PushAsync(new ContactDetailsPage(this));

        private async Task ExecuteOpenPhoneDialerCommand()
        {
            try
            {
                PhoneDialer.Open(Phone);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert("Ошибка", $"Ошибка при поптыке набора номера {Phone}", "ОК");
            }
        } 
        #endregion
    }
}
