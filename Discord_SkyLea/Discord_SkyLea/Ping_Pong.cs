using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discord_SkyLea.Commands
{
    public class Ping_Pong : BaseCommandModule
    {

        string url = "";
        [Command("Ping")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("Add")]
        public async Task Add(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString()).ConfigureAwait(false);
        }

        [Command("Response")]
        public async Task TestMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("ResponseEmoji")]
        public async Task TestEmoji(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("Track")]
        public async Task Track(CommandContext ctx)
        {
            
            
            var interactivity = ctx.Client.GetInteractivity();

            await ctx.Channel.SendMessageAsync(ctx.User.Mention + " Enter your Sky Lea URL");

            var url_pre = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author == ctx.User);
            url = url_pre.Result.Content;
            var channel = ctx.Channel;
            Console.WriteLine(channel.GetType());

            Thread t = new Thread(() => Tracking(url_pre.Result.Content, ctx));
            t.Start();
        }

        private void Tracking(string url, CommandContext ctx)
        {
            

            string source = getSource(url);

            if (source == "") return;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(source);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id='additional_stats_container']/div[1]/span[2]");

            foreach (HtmlNode node in nodes)
            {
                if (node.InnerText != "Private Island")
                {
                    int i = 0;
                    while (i <= 2)
                    {
                        ctx.Channel.SendMessageAsync(ctx.User.Mention + " Your not on your island");
                        i++;
                    }
                    ulong ID = ctx.Channel.Id;
                    //Client.
                    //IMessageChannel
                    //DiscordChannel channel = DiscordChannel.GetChannelAsync(ID);
                }
            }
        }

        string getSource(string url)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                byte[] myDataBuffer = myWebClient.DownloadData(url);
                string source = Encoding.ASCII.GetString(myDataBuffer);

                return source;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return "";
            }
        }

    }
}
