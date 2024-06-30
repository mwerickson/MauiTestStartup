using System.ComponentModel;

namespace MauiApp2;

[SecureStore]
public partial interface ITestSecureStore
{
    [DefaultValue("Sparky")]
    public string Name { get; set; }
}