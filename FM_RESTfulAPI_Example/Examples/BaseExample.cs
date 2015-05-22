using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_RESTfulAPI_Example.Support.Messaging;


namespace FM_RESTfulAPI_Example.Examples
{
    public abstract class BaseExample : IExample
    {
        protected UserMessage _messageChannel;

        public BaseExample(UserMessage channel)
        {
            SetMessageChannel(channel);
        }

        public abstract void Execute();
        
        public void SetMessageChannel(UserMessage channel)
        {
            _messageChannel = channel ?? new NLogMessage();
        }
    }
}
