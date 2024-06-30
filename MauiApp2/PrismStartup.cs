using System.Diagnostics;
using DryIoc;
using MauiApp2.ViewModels;
using Microsoft.Extensions.Logging;

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
                    Console.WriteLine($"----> Navigating to {context.Uri.OriginalString} ({context.Type}) with parameters:");

                    foreach (var parameter in context.Parameters)
                    {
                        Console.WriteLine($"----> [{parameter.ToString()}]");
                    }
                    return;
                }

                Debugger.Break();

                Console.WriteLine($"----> Exception navigating to {context.Uri.OriginalString} ({context.Type}) with the following parameters:");
                foreach (var parameter in context.Parameters)
                {
                    Console.WriteLine(parameter.ToString());
                }
                ContainerResolutionErrorCollection errors = new ContainerResolutionErrorCollection();

                System.Exception ex = context.Result.Exception;
                while (ex != null)
                {
                    if (ex is ContainerException ce)
                    {
                        var container = ContainerLocator.Container;
                        var details = ce.TryGetDetails(container.GetContainer());
                        Console.WriteLine("Container Exception: " + details);
                    }
                    if (ex is ContainerResolutionException cre)
                    {
                        var container = ContainerLocator.Container;
                        errors = cre.GetErrors();
                        foreach (var error in errors)
                        {
                            Console.Write($"Container Resolution Error: {error.Value.Message} {error.Value.StackTrace}");
                        }
                    }

                    ex = ex.InnerException;
                }
                // handle the navigation error here
                var x = 1;   // just for debugging breakpoint
            }))
            .CreateWindow(navigationService =>
                navigationService.CreateBuilder()
                    .AddSegment<MainPageViewModel>()
                    .NavigateAsync());
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
        container.RegisterStore<ITestSecureStore>();
        container.RegisterForNavigation<MainPage, MainPageViewModel>();
    }
}