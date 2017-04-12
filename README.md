The simplest way to create fast and reliable bot for vk.com!
## Установка

[ .Net Core only for a now ]
Install as [NuGet package](https://www.nuget.org/packages/vk-dotnet/):

```powershell
Install-Package vk-dotnet
```
# Получение токена для пользователя [ ВРЕМЕННО ]
```
https://oauth.vk.com/authorize?client_id=5933165&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=134217727&response_type=token&v=5.63&state=123456
```
![alt tag](http://image.prntscr.com/image/a3e010678c0e40d28e368bb5c5dd9315.png)
## Использование

```C#
    class Program
    {
        private static string myToken = "<my_token>";

        static void Main(string[] args)
        {
            BotClient MyBot = new BotClient(myToken);
            MyBot.IncomingMessage += IncomingMessageHandler;
            MyBot.StartListeningAsync();

            Console.ReadKey(); // Otherwise program will finish immediately
        }

        private static void IncomingMessageHandler(BotClient sender, Message e)
        {
            sender.SendTextMessageAsync(e.User_id.ToString(), "Hello from MyBot!");
        }
    }
    ```
