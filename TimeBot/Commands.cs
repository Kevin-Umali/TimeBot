using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeBot.API;

namespace TimeBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("commands")]
        public async Task BotCommands()
        {
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;
            // build out the reply
            sb.AppendLine($"Hello, " + user + "");
            sb.AppendLine("!time");
            sb.AppendLine("Example: !timezone Seoul");
            sb.AppendLine("!timediffarea");
            sb.AppendLine("Example: !timezone Seoul and Tokyo (Still Working)");
            //string response = new APIRequest().Type(1);
            await ReplyAsync(sb.ToString());
        }

        [Command("time")]
        [Alias("ask")]
        public async Task Time([Remainder]string value = null)
        {
            var sb = new StringBuilder();
            if (value == null)
            {
                sb.AppendLine("Unknown Location?");
            }
            else
            {
                string response = new APIRequest().Type(1, value);
                DeserializeJson deserializeJson = new DeserializeJson();
                await deserializeJson.deserialize(response);

                string _time = deserializeJson.getTime;
                sb.AppendLine(_time);
            }
            await ReplyAsync(sb.ToString());
        }
    }
}
