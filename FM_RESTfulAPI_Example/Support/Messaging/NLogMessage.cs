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
using NLog;

namespace FM_RESTfulAPI_Example.Support.Messaging
{
    /// <summary>
    /// This is just a single class for writing messages to the console. We'll use Nlog for this. For two reasons:
    /// - It has colors.
    /// - You can switch easy to other target (e.g.: file, db, etc...)
    /// </summary>
    public class NLogMessage : UserMessage
    {
        private static Logger _oLogger = NLog.LogManager.GetCurrentClassLogger();

        public override void Write(string message, MessageLevel level = MessageLevel.Info)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                _oLogger.Log(MapLogLevel(level), message);
            }
        }

        private LogLevel MapLogLevel(MessageLevel level)
        {
            LogLevel result = LogLevel.Fatal;
            switch (level)
            {
                case MessageLevel.Off:
                    result = NLog.LogLevel.Off;
                    break;
                case MessageLevel.Fatal:
                    result = NLog.LogLevel.Fatal;
                    break;
                case MessageLevel.Error:
                    result = NLog.LogLevel.Error;
                    break;
                case MessageLevel.Warn:
                    result = NLog.LogLevel.Warn;
                    break;
                case MessageLevel.Info:
                    result = NLog.LogLevel.Info;
                    break;
                case MessageLevel.Debug:
                    result = NLog.LogLevel.Debug;
                    break;
                case MessageLevel.Trace:
                    result = NLog.LogLevel.Trace;
                    break;
                default:
                    break;
            }

            return result;
        }

        
    }
}
