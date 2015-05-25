using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Support.Messaging
{
    /// <summary>
    /// Messaging Channel Factory 
    /// </summary>
    public static class MessageChannelFactory
    {
        /// <summary>
        /// Well, you have to put more stuff here ;-)
        /// </summary>
        /// <returns></returns>
        public static UserMessage CreateChannel()
        {
            return new NLogMessage();
        }
    }
}
