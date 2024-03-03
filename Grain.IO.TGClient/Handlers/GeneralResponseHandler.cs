using Grain.IO.TGClient.Parsers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Grain.IO.TGClient.Handlers;

public interface IGeneralResponseHandler
{
    void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}

public class GeneralResponseHandler : IGeneralResponseHandler
{
    public async void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat, "Whisky notificator Grain.IO has started", cancellationToken: cancellationToken);

        var whiskys = WhiskyModelParserV1.Parse();

        if(whiskys == null || whiskys.Count == 0)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat, "Nothing parsed");
            return;
        }


        //TODO: add buttons
    }
}
