namespace BiDiMessagingUwpWebView
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using BiDiMessagingUwpWebViewComponent;

    public sealed partial class MainPage : Page
    {
        private int count = 0;
        private readonly WebHost host = new WebHost();

        public MainPage()
        {
            this.InitializeComponent();
            this.host.CountIncrement += this.IncrementHostCounter;
            this.host.MessageReceived += this.ShowHostMessage;
        }

        private void IncrementHostCounter() => this.TextBlock.Content = $"Click #{++this.count}";

        private void ShowHostMessage(string s) => this.FromWeb.Text = s;

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args) => this.WebView.AddWebAllowedObject("host", host);

        private async void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            await this.WebView.InvokeScriptAsync("eval", new[] { "incrementWebCounter()" });
            this.IncrementHostCounter();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) => await this.WebView.InvokeScriptAsync("eval", new[] { $"setWebMessage('{this.ToWeb.Text}')" });
    }
}
