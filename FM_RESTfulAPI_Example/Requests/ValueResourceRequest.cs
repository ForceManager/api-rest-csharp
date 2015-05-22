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
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*******************************************************************************/

using FM_RESTfulAPI_Example.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FM_RESTfulAPI_Example.FMDomain;

namespace FM_RESTfulAPI_Example.Requests
{
    /// <summary>
    /// This class is used to perform FM RESTful API requests to the Values resource.
    /// </summary>
    public class ValueResourceRequest
    {
        protected const ModelType.Models INFO_MODEL                                 = ModelType.Models.ValueInfo;
        protected const ModelType.Models CURRENT_MODEL                              = ModelType.Models.Value;
        protected const String PARAMETER_RESOURCE_NAME                              = "resourceName";


        /// <summary>
        /// Gets the list of available resources
        /// </summary>
        /// <returns>List of ValueInfo, containig the name and a description of the values</returns>
        public List<ValueInfo> GetAvailableResources()
        {
            var result = new List<ValueInfo>();
            bool finished = false;
            IRestResponse response = null;
            FM_Pagination paginator = null;
             
            do
            {
                var request = FM_RequestFactory.CreateReadRequest(INFO_MODEL, null, paginator);
                if (request != null)
                {
                    response = FM_RequestFactory.Client.Execute(request);
                    finished = FM_RequestProcessor.ProcessPaginatedResponse<ValueInfo>(response, ref result, out paginator);
                }
                else
                { finished = true; }

            } while (!finished);


            return result;
        }


        /// <summary>
        /// Gets Lists of Values (Search List of Values by Resource Name).
        /// We will use the excelent Newtonsoft JObject to obtain a generic object, probably in your case,
        /// you would like to have a Strong Typed Class for this (e.g. Company)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of generic objects</returns>
        public List<JObject> SearchValues(String name)
        {
            return SearchValuesAdvanced(name, null);
        }


        /// <summary>
        /// Gets Lists of Values (Search List of Values by Resource Name).
        /// We will use the excelent Newtonsoft JObject to obtain a generic object, probably in your case,
        /// you would like to have a Strong Typed Class for this (e.g. Company)
        /// </summary>
        /// <param name="name">The name of the resource</param>
        /// <param name="query">The advanced query search String</param>
        /// <returns>A list of generic objects</returns>
        public List<JObject> SearchValuesAdvanced(String name, String query)
        {
            var result = new List<JObject>();
            IRestResponse response = null;
            bool finished = false;
            FM_Pagination paginator = null;


            if (!String.IsNullOrWhiteSpace(name))
            {
                Dictionary<String, String> filter = new Dictionary<string, string>();

                // Adding the resource name parameter
                filter.Add(PARAMETER_RESOURCE_NAME, name);

                // Adding the advanced query parameter
                if (!String.IsNullOrWhiteSpace(query))
                { filter.Add(FM_RequestFactory.PARAMETER_ADVANCED_SEARCH, query); }

                do
                {
                    var request = FM_RequestFactory.CreateReadRequest(CURRENT_MODEL, filter, paginator);
                    if (request != null)
                    {
                        response = FM_RequestFactory.Client.Execute(request);
                        finished = FM_RequestProcessor.ProcessPaginatedResponse<JObject>(response, ref result, out paginator);
                    }
                    else
                    { finished = true; }


                } while (!finished);
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GenericIdDescription CreateValue(Value input)
        {
            GenericIdDescription data = null;
            IRestResponse response = null;

            var request = FM_RequestFactory.CreateInsertRequest(CURRENT_MODEL, input);
            if (request != null)
            {
                response = FM_RequestFactory.Client.Execute(request);
                data = FM_RequestProcessor.ProcessCreateRequestResponse(response);
            }

            return data;
        }

    }
}
