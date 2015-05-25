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
using FM_RESTfulAPI_Example.Requests;
using FM_RESTfulAPI_Example.Support.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Examples
{
    /// <summary>
    /// This example shows how to Perform Operations on List of Values (LoV):
    ///  - How to create a LoV object
    ///  - How to update LoV
    ///  - How to delete LoV
    /// </summary>
    public class Example4 : BaseExample
    {
        protected const String RESOURCE_NAME = "Z_tblTestLinks";

        public Example4(UserMessage channel = null) : base(channel)
        {

        }

       
        public override void Execute()
        {
            _messageChannel.Write(">>>> Running example 4");

            // Request for the List of Values resource
            StandardRequest<Value> valueRequest = new StandardRequest<Value>(ModelType.Models.Value);

            //*********************************************************************************************//
            // First, we create a new value on memory
            Value data = GetSampleObject();
            
            // The LoV is saved on FM
            GenericIdDescription processed = valueRequest.CreateEntity(data);

            if (processed != null)
            {
                _messageChannel.Write(String.Format("The object on '{0}' was created with id: {1}", RESOURCE_NAME, processed.id));

                // We search the value by id
                ValueResourceRequest requestValue = new ValueResourceRequest();
                var createdObject = requestValue.SearchValuesAdvanced(RESOURCE_NAME, String.Format("id={0}", processed.id));

                // Some validation
                if (createdObject == null || createdObject.Count == 0)
                { throw new Exception("This is weird, should not happen"); }
                else
                {
                    // The LoV is updated on memory
                    data.data = createdObject[0];
                    UpdateSampleObject(ref data);

                    // The object is changed on FM
                    bool updated = valueRequest.UpdateEntity(processed.id, data);

                    // The object is deleted
                    bool deleted = valueRequest.DeleteEntity(processed.id, GetResourceHeader());

                    _messageChannel.Write(String.Format("Was updated the object?: {0}", updated));
                    _messageChannel.Write(String.Format("Was deleted the object?: {0}", deleted));
                }
            }

            _messageChannel.Write("==== END Running example 4");            
        }


        #region Internal Methods


        protected Value GetResourceHeader()
        {
            return new Value()
                        {
                            resourceName = RESOURCE_NAME,
                        };
        }


        protected Value GetSampleObject()
        {
            Value result = GetResourceHeader();
            List<JProperty> innerData = new List<JProperty>()
            {
                new JProperty("description_es", "Coursera"),
                new JProperty("description_en", "Coursera"),
                new JProperty("Z_URL", "https://www.coursera.org/"),
                new JProperty("intOrder", "0")
            };

            result.data = new JObject();
            result.data.Add(innerData);

            return result;
        }


        protected void UpdateSampleObject(ref Value tmpValue)
        {
            if (tmpValue != null)
            {
                List<JProperty> innerData = new List<JProperty>()
                {
                    new JProperty("strIdEnvironment", "abc000"),
                    new JProperty("description_en", "Coursera link"),
                    new JProperty("intOrder", "25"),
                };

                tmpValue.data = new JObject() { innerData };
            }
        }


        #endregion

    }
}
