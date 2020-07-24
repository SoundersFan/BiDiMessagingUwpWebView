namespace BiDiMessagingUwpWebViewComponent
{
    using Windows.Foundation.Metadata;

    public delegate void CountIncrementHandler();
    public delegate void HostMessageHandler(string s);

    [AllowForWeb]
    public sealed class WebHost
    {
        public event CountIncrementHandler CountIncrement;
        public event HostMessageHandler MessageReceived;

        public void incrementHostCount() => this.CountIncrement?.Invoke();

        public void sendHostMessage(string message) => this.MessageReceived?.Invoke(message);
    }
}
