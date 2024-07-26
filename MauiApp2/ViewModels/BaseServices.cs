using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Animations;

namespace MauiApp2.ViewModels;

public class BaseServices
{
    public BaseServices(INavigationService navigationService,
        ITestSecureStore secureStore)
    {
        NavigationService = navigationService;
        SecureStore = secureStore;
    }

    public INavigationService NavigationService { get; }
    public ITestSecureStore SecureStore { get; }
}