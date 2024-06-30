using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MauiApp2.ViewModels;

public class BaseViewModel : ReactiveObject, IPageLifecycleAware, INavigationAware
{
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