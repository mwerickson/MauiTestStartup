using ReactiveUI.Fody.Helpers;

namespace MauiApp2.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly ITestSecureStore _secureStore;

    public MainPageViewModel(ITestSecureStore secureStore)
    {
        _secureStore = secureStore;
        Title = "Main Page";
    }

    [Reactive] public string Name { get; set; }

    public override void OnAppearing()
    {
        base.OnAppearing();
        Name = _secureStore.Name;
    }
}