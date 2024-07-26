using __Prism_Essentials__;
using ReactiveUI.Fody.Helpers;

namespace MauiApp2.ViewModels;

#nullable disable
public class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(BaseServices baseServices) : base(baseServices)
    {
        Title = "Main Page";
    }

    [Reactive] public string Name { get; set; }

    public override void OnAppearing()
    {
        base.OnAppearing();
        Name = SecureStore.Name;
    }
}