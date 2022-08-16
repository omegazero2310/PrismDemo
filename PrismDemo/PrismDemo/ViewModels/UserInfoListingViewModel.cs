using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismDemo.Models;
using PrismDemo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismDemo.ViewModels
{
    internal class UserInfoListingViewModel : ViewModelBase
    {
        private IDBServices<UserInfo> _services;
        private IPageDialogService _pageDialogService;
        public ObservableCollection<UserInfo> Users { get; }
        private UserInfo _selectedUser;
        public UserInfo SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }
        private DelegateCommand _loadUsersCmd;
        public DelegateCommand LoadUsersCommand =>
            _loadUsersCmd ?? (_loadUsersCmd = new DelegateCommand(ExecuteLoadUsers));

        
        public UserInfoListingViewModel(INavigationService navigationService,IPageDialogService pageDialog, IDBServices<UserInfo> dBServices) : base(navigationService)
        {
            _services = dBServices;
            _pageDialogService = pageDialog;
            this.Title = "All Users";
        }
        private void ExecuteLoadUsers()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
