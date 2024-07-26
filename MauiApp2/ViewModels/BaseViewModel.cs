using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MauiApp2.ViewModels;

#nullable disable
public class BaseViewModel : ReactiveObject, IPageLifecycleAware, INavigationAware
{
    public BaseViewModel(BaseServices baseServices)
    {
        SecureStore = baseServices.SecureStore;
        NavigationService = baseServices.NavigationService;
    }

    public ITestSecureStore SecureStore { get; }
    public INavigationService NavigationService { get; }
    [Reactive] public string Title { get; set; }

    public virtual void OnAppearing()
    {
    }

    public virtual void OnDisappearing()
    {
    }

    public virtual void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {
    }
}