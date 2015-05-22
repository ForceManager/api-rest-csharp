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
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.FMDomain
{
    public static class FM_RequestProcessor
    {

        /// <summary>
        /// Used to process GET reponses when search by:
        /// - All entities
        /// - Important fields
        /// - Advanced Search
        /// - Search List of Values
        /// - Search List of Values by Resource Name
        /// - Search by Internal Ids
        /// </summary>
        /// <typeparam name="T">The kind of object (model) we are trying to obtain</typeparam>
        /// <param name="response">The request response returned by the FM RESTful API</param>
        /// <param name="data">The list of objects obtained from the response</param>
        /// <param name="paginator">Paginator object to get information about the pagination state</param>
        /// <returns>True when finished pagination, False otherwise</returns>
        public static bool ProcessPaginatedResponse<T>(IRestResponse response, ref List<T> data, out FM_Pagination paginator)
        {
            bool finished = true;
            paginator = null;

            if (data == null)
            {
                // Not really necessary. You can perform: data = new List<T>();
                throw new Exception("You must provide an initialized object for the variable: IList<T> data");
            }
            else
            {
                if (response != null)
                {
                    switch (response.StatusCode)
                    {

                        case HttpStatusCode.BadRequest:
                            // The error was mine, probably a wrong parameter
                            break;
                        case HttpStatusCode.NotFound:
                            // We did a correct request, but we just didn't find any data 
                            finished = true;
                            break;
                        case HttpStatusCode.OK:
                            // All Ok, proccess
                            paginator = new FM_Pagination(response);
                            if (!String.IsNullOrWhiteSpace(response.Content))
                            {
                                List<T> responseData = JsonConvert.DeserializeObject<List<T>>(response.Content);
                                data = data.Concat(responseData).ToList();
                            }

                            finished = (!paginator.HasMorePages);
                            break;
                        case HttpStatusCode.InternalServerError:
                            // We can log or throw this, but this is not out fault
                            break;
                        default:
                            // This is really strange!
                            break;
                    }
                }
            }

            return finished;
        }


        /// <summary>
        /// Process a GET request when it only contains only 0/1 object. I.E.: "Search by entity id"
        /// </summary>
        /// <typeparam name="T">The kind of object (model) we are trying to obtain</typeparam>
        /// <param name="response">The request response returned by the FM RESTful API</param>
        /// <returns>The object return by the FM RESTful API, null if unable to get</returns>
        public static T ProcessSingleObjectResponse<T>(IRestResponse response)
        {
            T result = default(T);
            if (response != null)
            {
                switch (response.StatusCode)
                {

                    case HttpStatusCode.BadRequest:
                        // The error was mine, probably a wrong parameter
                        break;
                    case HttpStatusCode.OK:
                        // We are fine, process
                        if (!String.IsNullOrWhiteSpace(response.Content))
                        {
                            var data = JsonConvert.DeserializeObject<List<T>>(response.Content);
                            if (data != null && data.Count > 0)
                            { result = data[0]; }
                        }

                        break;
                    case HttpStatusCode.InternalServerError:
                        // We can log this, but this is not out fault
                        break;
                    default:
                        // This is really strange!
                        break;
                }
            }

            return result;
        }


        /// <summary>
        /// Tells when a DELETE request has deleted a record
        /// </summary>
        /// <param name="response">The request response returned by the FM RESTful API</param>
        /// <returns>True was deleted, otherwise False</returns>
        public static bool ProcessDeleteRequestResponse(IRestResponse response)
        {
            // You can do wethever you want here. Log errors, perform the request more times, etc...
            // In fact, you would notice delete response always returns a message body, it's up to you
            // if you need further processing
            return (response != null && response.StatusCode == HttpStatusCode.OK);
        }


        /// <summary>
        /// Gets the id and a description when creating records on the FM RESTful API
        /// </summary>
        /// <param name="response">The request response returned by the FM RESTful API</param>
        /// <returns>A GenericIdDescription object with the id of the new created entity and a status message.
        /// Null if the object wasn't created</returns>
        public static GenericIdDescription ProcessCreateRequestResponse(IRestResponse response)
        {
            GenericIdDescription result = null;

            // You can do wethever you want here. Log errors, perform the request more times, etc...
            if (response != null && response.StatusCode == HttpStatusCode.Created)
            { result = JsonConvert.DeserializeObject<GenericIdDescription>(response.Content); }

            return result;
        }


        /// <summary>
        /// Tells when a PUT request has updated the record
        /// </summary>
        /// <param name="response">The request response returned by the FM RESTful API</param>
        /// <returns>True was updated, otherwise False</returns>
        public static bool ProcessUpdateRequestResponse(IRestResponse response)
        {
            // You can do wethever you want here. Log errors, perform the request more times, etc...
            return (response != null && response.StatusCode == HttpStatusCode.OK);
        }


    }
}
