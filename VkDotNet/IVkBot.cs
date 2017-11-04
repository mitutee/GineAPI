namespace VkDotNet
{
    public delegate void VkBotEventHandler<TArgs>(IVkBot bot, TArgs args);

    public interface IVkBot
    {
        string AccessToken { get; }

        void StartListening();

        event VkBotEventHandler<Core.QueryBuilders.Message> MessageReceived;
    }
}