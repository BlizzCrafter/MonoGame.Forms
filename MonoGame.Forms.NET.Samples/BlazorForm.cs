using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.WinForms;
using MonoGame.Forms.NET.Samples.Tests;
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
            services.AddSingleton(typeof(Welcome), welcome1);
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<App>("#app");
        }
    }
}
