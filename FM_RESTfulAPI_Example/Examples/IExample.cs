using FM_RESTfulAPI_Example.Support.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Examples
{
    public interface IExample
    {
        void Execute();
        void SetMessageChannel(UserMessage channel);
    }
}
