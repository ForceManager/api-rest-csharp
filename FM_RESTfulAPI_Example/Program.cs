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
using FM_RESTfulAPI_Example.Examples;
using FM_RESTfulAPI_Example.Support.Messaging;

namespace FM_RESTfulAPI_Example
{
    /// <summary>
    /// This is the entry point of the app
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Channel used to display messages.
            UserMessage messageChannel = MessageChannelFactory.CreateChannel();

            try
            {
                messageChannel.Write("Running the examples...");

                /*************************************************************************************/
                Example1 example1 = new Example1(messageChannel);
                example1.Execute();

                /*************************************************************************************/
                Example2 example2 = new Example2(messageChannel);
                example2.Execute();

                /*************************************************************************************/
                Example3 example3 = new Example3(messageChannel);
                example3.Execute();

                /*************************************************************************************/
                Example4 example4 = new Example4(messageChannel);
                example4.Execute();

                /*************************************************************************************/
                messageChannel.Write("Process ended!");
            }
            catch (Exception ex)
            {
                messageChannel.Write(ex.Message, UserMessage.MessageLevel.Fatal);
            }
        }
    }
}
