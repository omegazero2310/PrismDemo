using Prism.Navigation;
using PrismDemo.Exts.Enum;
using PrismDemo.Models;
using PrismDemo.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Prism.Commands;
using System.Reflection;
using Prism.Services;

namespace PrismDemo.ViewModels
{
    internal class UserInfoUpdateViewModel : ViewModelBase
    {
        public IDBServices<UserInfo> _dBServices;
        public IPageDialogService _pageDialogService;
        /// <summary>
        /// check if View is New or Update, true => is update mode and disable edit on UserName field, else add new user
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        private bool _isUpdate = false;
        public bool IsUpdate
        {
            get { return _isUpdate = false; }
            set { SetProperty(ref _isUpdate, value); }
        }
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
        public DateTime DateCreated { get; set; } = DateTime.Now;
        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand(ExecuteUpdateCommand));


        public UserInfoUpdateViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDBServices<UserInfo> dBServices) : base(navigationService)
        {
            _dBServices = dBServices;
            _pageDialogService = pageDialog;
            this.Title = "Create New User";
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.UserName = parameters["UserName"].ToString();
            this.DateCreated = Convert.ToDateTime(parameters["DateCreated"]);
            this.Title = "Update User";
        }
        private async void ExecuteUpdateCommand()
        {
            try
            {
                Enum.TryParse(this.UserGender, out UserGenderOption gender);
                UserInfo info = new UserInfo
                {
                    UserName = this.UserName,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Gender = gender,
                    DateOfBirth = this.DateOfBirth,
                    Address = this.Address,
                    Email = this.Email,
                    PhoneNumber = this.PhoneNumber,
                    TotalKM = decimal.Parse(Kilometer),
                    DateCreated = this.DateCreated,
                    DateModified = DateTime.Now
                };
                if (this.IsUpdate)
                    await this._dBServices.Update(info);
                else
                    await this._dBServices.Create(info);
                await this.NavigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await _pageDialogService.DisplayAlertAsync("Server Error", "Cannot save, try again later", "Ok");
            }

        }
    }
}
