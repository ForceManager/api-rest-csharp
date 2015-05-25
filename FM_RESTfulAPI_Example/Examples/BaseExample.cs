/*******************************************************************************
Copyright (c) 2015, Tritium Software S.L.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the Tritium Software S.L., nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL Tritium Software S.L. BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_RESTfulAPI_Example.Support.Messaging;


namespace FM_RESTfulAPI_Example.Examples
{
    /// <summary>
    /// This abstract class defines the contract to be maintained for Examples concrete implementations.
    /// DPA: Do you remember SOLID (ISP)?: http://en.wikipedia.org/wiki/Interface_segregation_principle
    /// </summary>
    public abstract class BaseExample : IExample, IMessaging
    {
        // We are injecting a Message Channel (mc). In our case the channel would be the console,
        // but inverting the control (IoC) we allow the classes to write whenever and wherever they might want
        protected UserMessage _messageChannel;

        // We'll inject the mc by constructor
        public BaseExample(UserMessage channel)
        {
            SetMessageChannel(channel);
        }

        // But we would also allow this by method
        public void SetMessageChannel(UserMessage channel)
        {
            _messageChannel = channel ?? MessageChannelFactory.CreateChannel();
        }

        // DPA: 
        // Quiz: Do you know how many other others IoC injections methods are available? (It's a tricky question)

        #region Abstract methods

        /// <summary>
        /// Execute/run the example
        /// </summary>
        public abstract void Execute();

        #endregion

    }
}
