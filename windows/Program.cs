using System.Reflection;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace PainelEmFolhas;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainWindow());
    }
}

internal sealed class MainWindow : Form
{
    private readonly WebView2 browser = new() { Dock = DockStyle.Fill };

    public MainWindow()
    {
        Text = "Painel em Folhas";
        Width = 1360;
        Height = 860;
        MinimumSize = new Size(360, 500);
        StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(browser);
        Shown += async (_, _) => await OpenAppAsync();
    }

    private async Task OpenAppAsync()
    {
        try
        {
            var appFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PainelEmFolhas", "app-v1");
            Directory.CreateDirectory(appFolder);
            Extract("index.html", Path.Combine(appFolder, "index.html"));
            Extract("jspdf.umd.min.js", Path.Combine(appFolder, "jspdf.umd.min.js"));

            var userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PainelEmFolhas", "WebView2");
            var environment = await CoreWebView2Environment.CreateAsync(userDataFolder: userDataFolder);
            await browser.EnsureCoreWebView2Async(environment);
            browser.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "painel.local", appFolder, CoreWebView2HostResourceAccessKind.Allow);
            browser.Source = new Uri("https://painel.local/index.html");
        }
        catch (Exception error)
        {
            MessageBox.Show(
                "Não foi possível abrir o Painel em Folhas. Verifique se o Microsoft Edge WebView2 Runtime está instalado.\n\n" + error.Message,
                "Painel em Folhas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void Extract(string name, string destination)
    {
        using var resource = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("PainelEmFolhas." + name)
            ?? throw new InvalidOperationException("Recurso ausente: " + name);
        using var output = File.Create(destination);
        resource.CopyTo(output);
    }
}
