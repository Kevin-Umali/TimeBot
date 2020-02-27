using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using TimeBot.API;

namespace TimeBot
{
    class Program
    {
        private DiscordSocketClient _discordclient;
        private CommandService _discordcommands;
        private IServiceProvider _discordservices;
        static void Main(string[] args) => new Program()
            .RunBotAsync()
            .GetAwaiter()
            .GetResult();


        public async Task RunBotAsync()
        {
            _discordclient = new DiscordSocketClient();
            _discordcommands = new CommandService();

            _discordservices = new ServiceCollection()
                .AddSingleton(_discordclient)
                .AddSingleton(_discordcommands)
                .BuildServiceProvider();

            string token = new BotToken().GetToken;

            _discordclient.Log += _discordclient_Log;

            await RegisterCommandsAsync();
            await _discordclient.LoginAsync(TokenType.Bot, token);
            await _discordclient.StartAsync();

            await Task.Delay(-1);
        }

        private Task _discordclient_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _discordclient.MessageReceived += EventHandleCommandAsync;
            await _discordcommands.AddModulesAsync(Assembly.GetEntryAssembly(), _discordservices);
        }

        private async Task EventHandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_discordclient, message);

            if (message.Author.IsBot)
                return;

            int argPos = 0;
            if(message.HasStringPrefix("!", ref argPos))
            {
                var result = await _discordcommands.ExecuteAsync(context, argPos, _discordservices);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
