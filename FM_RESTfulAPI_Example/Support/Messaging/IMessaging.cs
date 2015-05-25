using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Support.Messaging
{
    /// <summary>
    /// This interface defines the contract for messaging
    /// </summary>
    public interface IMessaging
    {
        void SetMessageChannel(UserMessage channel);
    }
}
