using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Mvc;

namespace Grunk.Web.Services
{
    public enum MessageType
    {
        [Description("alert-error")]
        Error,

        [Description("alert-block")]
        Standard,

        [Description("alert-success")]
        Success,

        [Description("alert-info")]
        Info
    }

    public class Message
    {
        public MvcHtmlString Title { get; private set; }
        public MvcHtmlString Content { get; private set; }
        public MessageType Type { get; set; }
        public string CssClass
        {
            get
            {
                var memberInfo = typeof(MessageType).GetMember(Type.ToString());
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                return ((DescriptionAttribute)attributes[0]).Description;
            }
        }

        public Message(MvcHtmlString title, MvcHtmlString content, MessageType type)
        {
            this.Title = title;
            this.Content = content;
            this.Type = type;
        }
    }

    public class MessageHandler : IEnumerable<Message>
    {
        private List<Message> messages = new List<Message>();

        public bool ContainMessages
        {
            get
            {
                return messages.Any();
            }
        }

        public MessageHandler Add(string title, string content, MessageType type)
        {
            this.messages.Add(new Message(MvcHtmlString.Create(title), MvcHtmlString.Create(content), type));

            return this;
        }

        public IEnumerator<Message> GetEnumerator()
        {
            return messages.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return messages.GetEnumerator();
        }

        public static MessageHandler CreateNew()
        {
            return new MessageHandler();
        }

        public MessageHandler AddStandardSavingError()
        {
            return this.Add("Der skete en fejl", "Der skete en fejl da vi prøvede at gemme", MessageType.Error);
        }

        public MessageHandler AddStandardRetriveError()
        {
            return this.Add("Der skete en fejl", "Vi kunne ikke hente den ønskede data ud", MessageType.Error);
        }

        public MessageHandler AddStandardDeleteError()
        {
            return this.Add("Der skete en fejl", "Vi kunne ikke slette den ønskede data", MessageType.Error);
        }

        public MessageHandler AddStandardUpdateError()
        {
            return this.Add("Der skete en fejl", "Vi kunne ikke rette den ønskede data", MessageType.Error);
        }

        public MessageHandler AddStandardCreateError()
        {
            return this.Add("Der skete en fejl", "Vi kunne ikke iorette den ønskede data", MessageType.Error);
        }
    }
}
