﻿namespace Shrooms.Contracts.Options
{
    public class MailOptions
    {
        public string Sender { get; set; }

        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? Port { get; set; }

        public bool UseSmtp4Dev { get; set; }
    }
}
