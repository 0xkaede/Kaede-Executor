using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaede_Executor
{
    public class RichPresenceClient
    {
        public static DiscordRpcClient Client { get; set; }

        private static RichPresence _currentPresence;

        private static readonly Assets _assets = new Assets
        {
            LargeImageKey = "mainimage",
            LargeImageText = $"Version {Environment.Version}"
        };

        private static readonly Timestamps _timestamps = new Timestamps
        {
            StartUnixMilliseconds = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds()
        };

        private static readonly Button[] _buttons =
        {
            new Button
            {
                Label = "Join my Discord",
                Url = "https://discord.com"
            },
            new Button
            {
                Label = "Visit my Github",
                Url = "https://github.com/0xkaede"
            }
        };

        public static void Start()
        {
            Client = new DiscordRpcClient("962830507274555393");

            _currentPresence = new RichPresence
            {
                Details = "Location - Home",
                State = "Created by kaede#1337",
                Assets = _assets,
                Buttons = _buttons,
                Timestamps = _timestamps
            };

            Client.SetPresence(_currentPresence);
            Client.Initialize();
        }

        public static void UpdatePresence(string details, string State)
        {
            _currentPresence.Details = details;

            _currentPresence.State = State;

            Client.SetPresence(_currentPresence);
        }
    }
}
