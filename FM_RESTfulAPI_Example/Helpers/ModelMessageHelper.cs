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

using FM_RESTfulAPI_Example.Models;
using FM_RESTfulAPI_Example.Support.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Helpers
{
    /// <summary>
    /// Used to write human readable common objects representations to a channel
    /// </summary>
    public static class ModelMessageHelper
    {
        public static void PrintModelList(Object lstModel, UserMessage channel)
        {
            try
            {
                if (channel != null && lstModel != null)
                {
                    dynamic tmpLstModel = lstModel;

                    if (tmpLstModel.Count > 0)
                    {
                        if (tmpLstModel[0] is IModel)
                        {
                            foreach (IModel model in tmpLstModel)
                            { PrintModel(model, channel); }    
                        }
                        else if (tmpLstModel[0] is JObject)
                        {
                            foreach (JObject model in tmpLstModel)
                            { PrintJson(model, channel); }
                        }
                        else
                        { throw new Exception("ModelMessageHelper doesn't support this model type"); }

                        
                    }
                    else
                    { channel.Write("No data", UserMessage.MessageLevel.Warn); }
                }
                else
                { throw new Exception("ModelMessageHelper needs a valid channel to write to"); }
            }
            catch
            {
                // do something...
            }
           
        }

        /// <summary>
        /// Printing regular model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="channel"></param>
        public static void PrintModel(IModel model, UserMessage channel)
        {
            if (model != null && channel != null)
            { channel.Write(model.GetPrettyRepresentation()); }
        }

        /// <summary>
        /// Printing Json
        /// </summary>
        /// <param name="model"></param>
        /// <param name="channel"></param>
        public static void PrintJson(JObject model, UserMessage channel)
        {
            if (model != null && channel != null)
            { channel.Write(model.ToString()); }
        }

    }
}
