using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand _goToListingPageCmd;
        public DelegateCommand GoToListPageCommand =>
            _goToListingPageCmd ?? (_goToListingPageCmd = new DelegateCommand(ExecuteGoToListPageCommand));


        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }
        private async void ExecuteGoToListPageCommand()
        {
            await this.NavigationService.NavigateAsync("UserInfoListingPage");
        }
    }
}
