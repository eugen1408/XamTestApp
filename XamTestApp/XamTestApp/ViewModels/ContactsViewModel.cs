﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamTestApp.Services;

namespace XamTestApp.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        #region Fields & Properties
        public ObservableCollection<ContactViewModel> Contacts { get; set; }
        public Command LoadContactsCommand { get; set; }
        public Command ForceLoadContactsCommand { get; set; } 
        #endregion

        public ContactsViewModel()
        {
            Contacts = new ObservableCollection<ContactViewModel>();
            LoadContactsCommand = new Command(async () => await ExecuteLoadContactsCommand());
            ForceLoadContactsCommand = new Command(async () => await ExecuteLoadContactsCommand(forceReload: true));
        }

        #region Commands
        private async Task ExecuteLoadContactsCommand(bool forceReload = false)
        {
            IsBusy = true;
            try
            {
                var contacts = await ContactsDataStore.GetContactsFromSourceAsync(forceReload);
                Contacts.Clear();
                foreach (var contact in contacts)
                {
                    Contacts.Add(new ContactViewModel(contact));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert($"Ошибка", "Произошла ошибка при загрузке контактов. Будут показаны контакты из локальной БД.", "OK");
                // если это первая загрузка и список пустой, загрузим из кэша
                if (Contacts.Count == 0)
                {
                    await LoadContactsFromCache();
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadContactsFromCache()
        {
            IsBusy = true;
            try
            {
                var contacts = await ContactsDataStore.GetContactsFromCacheAsync();
                foreach (var contact in contacts)
                {
                    Contacts.Add(new ContactViewModel(contact));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                await Shell.Current.DisplayAlert($"Ошибка", "Произошла ошибка при загрузке контактов из локальной БД.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}