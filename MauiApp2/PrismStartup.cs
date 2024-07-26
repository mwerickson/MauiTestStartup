using DryIoc;
using MauiApp2.ViewModels;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MauiApp2;

public static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
            .OnInitialized(OnInitialized)
            .ConfigureLogging(ConfigureLogging)
            .UsePrismEssentials()
            .ConfigureModuleCatalog(catalog => catalog.Initialize())
            .AddGlobalNavigationObserver(x => x.Subscribe(context =>
            {
                //var logger = ContainerLocator.Current.Resolve<ILogger>();
                if (context.Result.Success)
                {
                    Console.WriteLine(
                        $"----> Navigating to {context.Uri.OriginalString} ({context.Type}) with parameters:");

                    foreach (var parameter in context.Parameters)
                    {
                        Console.WriteLine($"----> [{parameter.ToString()}]");
                    }

                    return;
                }

                Debugger.Break();

                Console.WriteLine(
                    $"----> Exception navigating to {context.Uri.OriginalString} ({context.Type})");
                if (context.Parameters.Count > 0)
                {
                    Console.WriteLine("...with the following parameters:");
                    foreach (var parameter in context.Parameters)
                    {
                        Console.WriteLine(parameter.ToString());
                    }
                }

                ShowNavigationException(context.Result.Exception);
            }))
            .CreateWindow(async navigationService =>
                await navigationService.CreateBuilder()
                    .AddNavigationPage()
                    .AddSegment<MainPageViewModel>()
                    .NavigateAsync());

    }

    private static void ShowNavigationException(Exception? ex)
    {
        if (ex == null)
        {
            Console.WriteLine($"Inner Exception is null");
            return;
        }

        if (ex is ContainerException ce)
        {
            Console.WriteLine($"Container Exception: {ce.Message} {ce.StackTrace}");
            var details = ce.TryGetDetails(ContainerLocator.Container.GetContainer());
            Console.WriteLine("Container Exception Details: " + details);
            Console.WriteLine($"Has inner exception: {ce.InnerException != null}");
            ShowNavigationException(ce.InnerException);
        }
        else if (ex is ContainerResolutionException cre)
        {
            var container = ContainerLocator.Container;
            var errors = cre.GetErrors();
            foreach (var error in errors)
            {
                Console.WriteLine($"Container Resolution Error: {error.Value.Message} {error.Value.StackTrace}");
                Console.WriteLine($"Has inner exception: {error.Value.InnerException != null}");
                ShowNavigationException(error.Value.InnerException);
            }
        }
        else
        {
            Console.WriteLine($"Exception: {ex.Message} {ex.StackTrace} ");
            Console.WriteLine($"Has inner exception: {ex.InnerException != null}");
            ShowNavigationException(ex.InnerException);
        }
    }

    private static void OnInitialized(IContainerProvider container)
    {
    }

    private static void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
    }

    private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
    {
    }

    private static void RegisterTypes(IContainerRegistry container)
    {
        //container.RegisterSingleton<IKeyValueStoreFactory, KeyValueStoreFactory>();
        container.RegisterStore<ITestSecureStore>();
        container.RegisterForNavigation<MainPage, MainPageViewModel>();
        container.RegisterScoped<BaseServices>();

        // Check to make sure registered objects have dependencies registered as well
        var errors = container.GetContainer().Validate();
        if (errors.Length > 0)
            Debugger.Break();
    }
}