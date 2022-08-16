using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismDemo.Models;
using PrismDemo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        private DelegateCommand _loadUsersCmd;
        public DelegateCommand LoadUsersCommand =>
            _loadUsersCmd ?? (_loadUsersCmd = new DelegateCommand(ExecuteLoadUsers));
        private DelegateCommand<UserInfo> _viewUsercmd;
        public DelegateCommand<UserInfo> ViewUserCommand =>
            _viewUsercmd ?? (_viewUsercmd = new DelegateCommand<UserInfo>(ExecuteViewUserCommand));
        private DelegateCommand _addItemCommand;
        public DelegateCommand AddItemCommand =>
            _addItemCommand ?? (_addItemCommand = new DelegateCommand(ExecuteAddItemCommand));



        public UserInfoListingViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDBServices<UserInfo> dBServices) : base(navigationService)
        {
            _services = dBServices;
            _pageDialogService = pageDialog;
            this.Users = new ObservableCollection<UserInfo>();
            this.Title = "All Users";
        }
        private async void ExecuteLoadUsers()
        {
            this.IsBusy = true;
            try
            {
                this.Users.Clear();
                var list = await this._services.GetData();
                foreach (var user in list)
                {
                    this.Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await _pageDialogService.DisplayAlertAsync("Server Error", "Server return error: " + ex.Message, "OK");
            }
            finally
            {
                this.IsBusy = false;
            }
        }
        private async void ExecuteViewUserCommand(UserInfo parameter)
        {

        }
        private async void ExecuteAddItemCommand()
        {
            await this.NavigationService.NavigateAsync("UserInfoUpdatePage");
        }
    }
}
