using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.WinForms;
using MonoGame.Forms.NET.Samples.Shared;
using MonoGame.Forms.NET.Samples.Tests;
using MonoGame.Forms.NET.Samples.Tests.Container;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MonoGame.Forms.NET.Samples
{
    public partial class BlazorForm : Form
    {
        public BlazorForm()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton(typeof(TabPageLayout));
            services.AddSingleton(typeof(MonoGameControlPanel), monoGameControlPanel);
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<App>("#app");
        }
    }
}
