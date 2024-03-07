using Grain.IO.TGClient.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Grain.IO.TGClient;

public class Bootstraper
{
    private IServiceProvider _serviceProvider;

    private ITelegramBotClient _telegramBot;

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            //TODO: request per Context (where context ~ User)
            var message = update.Message;
            if (message.Text.Equals("/start", StringComparison.CurrentCultureIgnoreCase))
            {
                //TODO: send context through container
                _serviceProvider.GetRequiredService<IGeneralResponseHandler>().Handle(botClient, update, cancellationToken);
                return;
            }
        }
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }

    public void Initialize(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
        //TODO:
        _telegramBot = new TelegramBotClient("TOKEN");

        Console.WriteLine("Bot started " + _telegramBot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        _telegramBot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}
