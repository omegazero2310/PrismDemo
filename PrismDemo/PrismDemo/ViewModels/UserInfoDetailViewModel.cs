using Prism.Navigation;
using Prism.Services;
using PrismDemo.Exts.Enum;
using PrismDemo.Models;
using PrismDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismDemo.ViewModels
{
    internal class UserInfoDetailViewModel : ViewModelBase
    {
        private IDBServices<UserInfo> _services;
        private IPageDialogService _pageDialogService;
        public List<string> GenderList
        {
            get
            {
                return Enum.GetNames(typeof(UserGenderOption)).ToList();
            }
        }
        private string _userGender;
        public string UserGender
        {
            get { return _userGender; }
            set { SetProperty(ref _userGender, value); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        private DateTime _dateOfBirth = DateTime.UtcNow;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { SetProperty(ref _dateOfBirth, value); }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        private string _kilometer;
        public string Kilometer
        {
            get { return _kilometer; }
            set { SetProperty(ref _kilometer, value); }
        }
        public UserInfoDetailViewModel(INavigationService navigationService, IDBServices<UserInfo> dBServices, IPageDialogService pageDialogService) : base(navigationService)
        {
            this._services = dBServices;
            this._pageDialogService = pageDialogService;
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                this.UserName = parameters["UserName"].ToString();
                this.Title = "View User Detail";
                var info = await this._services.GetData(this.UserName);
                this.UserName = info.UserName;
                this.FirstName = info.FirstName;
                this.LastName = info.LastName;
                this.UserGender = ((int)info.Gender).ToString();
                this.DateOfBirth = info.DateOfBirth;
                this.Address = info.Address;
                this.Email = info.Email;
                this.PhoneNumber = info.PhoneNumber;
                this.Kilometer = info.TotalKM.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await _pageDialogService.DisplayAlertAsync("Loading Error", "Load user failed:" + ex.Message, "Return");
                await this.NavigationService.GoBackAsync();
            }
        }
    }
}
