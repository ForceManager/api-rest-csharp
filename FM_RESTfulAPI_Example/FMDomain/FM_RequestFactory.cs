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
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace FM_RESTfulAPI_Example.FMDomain
{
    /// <summary>
    /// This class is not really a Factory (the design pattern), it's more the Facade one, which main responsibility
    /// is to create FM RESTful API requests.
    /// 
    /// The great majority of standard requests are supported.
    /// </summary>
    public static class FM_RequestFactory
    {

        #region Constants and Fields

        private const String FM_PUBLIC_URL_HOLDER                                   = "FM_API_URL";
        private const String FM_DEFAULT_PUBLIC_URL                                  = "https://restfm.forcemanager.net/api/";
        private const String JSON_CONTENT_TYPE                                      = "application/json";
        public const String PARAMETER_ADVANCED_SEARCH                               = "q";

        private static object _oLocker = new Object();
        private static IRestClient _client = null;

        #endregion


        /// <summary>
        /// We want only one instance of this object (Singleton)
        /// </summary>
        public static IRestClient Client
        {
            get
            {
                lock (_oLocker)
                {
                    if (_client == null)
                    { _client = new RestClient(CreateFMURL()); }    
                }

                return _client;
            }

            private set
            { }
        }


        #region READ Requests (Selects)

        /// <summary>
        /// Searchs list of entities using advanced search (Advanced Search)
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="query">The query string used for filtering</param>
        /// <returns>The response from the server</returns>
        public static IRestRequest CreateReadAdvancedRequest(ModelType.Models modelType, String query, FM_Pagination paginator = null)
        {
            return CreateReadRequest(modelType, new Dictionary<string, string>() { { PARAMETER_ADVANCED_SEARCH, query } }, paginator);
        }

        /// <summary>
        /// Searchs an entity by its id (Search by entity id)
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="id"></param>
        /// <returns>The response from the server</returns>
        public static IRestRequest CreateReadRequestById(ModelType.Models modelType, int id)
        {
            return CreateRequest(modelType, Method.GET, id);
        }

        /// <summary>
        /// Searchs list of entities by criteria (Search all entities, Search by important fields)
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="parameters"></param>
        /// <param name="paginator"></param>
        /// <returns>The response from the server</returns>
        public static IRestRequest CreateReadRequest(ModelType.Models modelType, Dictionary<String, String> parameters = null, FM_Pagination paginator = null)
        {
            return CreateRequest(modelType, Method.GET, null, parameters, paginator);
        }

        #endregion


        #region PUT Requests (Updates)

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="id">The id of the entity to operate</param>
        /// <param name="body">The data used to perform the operation</param>
        /// <returns>The response from the server</returns>
        public static IRestRequest CreateUpdateRequest(ModelType.Models modelType, int id, IModel body)
        {
            return CreateRequest(modelType, Method.PUT, id, payload: body);
        }

        #endregion

        
        #region POST Requests (Inserts)

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="body">The data used to perform the operation</param>
        /// <returns>The response from the server</returns>
        public static IRestRequest CreateInsertRequest(ModelType.Models modelType, IModel body)
        {
            return CreateRequest(modelType, Method.POST, payload: body);
        }

        #endregion


        #region DEL Requests (Deletes)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="id">The id of the entity to operate</param>
        /// <param name="body">The data used to perform the operation</param>
        /// <returns>The response from the server</returns>
        public static IRestRequest CreateDeleteRequest(ModelType.Models modelType, int id, IModel body = null)
        {
            return CreateRequest(modelType, Method.DELETE, id, payload: body);
        }

        #endregion


        #region Support Methods

        /// <summary>
        /// This is the worker method who really creates the requests
        /// </summary>
        /// <param name="modelType">The model type to use</param>
        /// <param name="methodType"></param>
        /// <param name="id">The id of the entity to operate</param>
        /// <param name="parameters">The request's parameters</param>
        /// <param name="paginator">The paginator object used for pagination</param>
        /// <param name="payload">The request's body</param>
        /// <returns>The response from the server</returns>
        private static IRestRequest CreateRequest(ModelType.Models modelType, Method methodType, int? id = null,
                                                  Dictionary<String, String> parameters = null, FM_Pagination paginator = null,
                                                  IModel payload = null)
        {
            IRestRequest request = null;

            try
            {
                String resource = ResourceLocator.GetResourceLocator(modelType) + "/";

                if (id != null)
                { resource += id.Value.ToString() + "/"; }

                if (parameters != null)
                { resource += "?" + String.Join("&", parameters.Select(x => String.Format("{0}={1}", x.Key, WebUtility.UrlEncode(x.Value)))); }

                request = new RestRequest(resource, methodType);
                request.RequestFormat = DataFormat.Json;

                var requestHeaders = CreateRequestHeaders(paginator);
                foreach (var header in requestHeaders)
                { request.AddHeader(header.Key, header.Value); }

                if (payload != null)
                {
                    String serializedObject = JsonConvert.SerializeObject(payload,
                                                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                    request.AddParameter(JSON_CONTENT_TYPE, serializedObject, ParameterType.RequestBody);
                }

            }
            catch (Exception ex)
            {
                // Error handling
                request = null;
            }

            return request;
        }
        
        /// <summary>
        /// Returns a list with the requests headers
        /// </summary>
        /// <param name="paginator">The paginator object used for pagination</param>
        /// <returns>A dictionary with Key: Header key and Value: Header value</returns>
        private static Dictionary<String, String> CreateRequestHeaders(FM_Pagination paginator)
        {
            Dictionary<String, String> headers = new Dictionary<string, string>();
            FM_Authentication authenticationObject = new FM_Authentication();
            FM_Versioning versioningObject = new FM_Versioning();

            // Authentication 
            headers = authenticationObject.GetAuthenticationHeaders();

            // Versionioning
            var versioningHeaders = versioningObject.GetVersioningHeaders();
            if (versioningHeaders != null)
            { headers = headers.Concat(versioningHeaders).ToDictionary(k => k.Key, v => v.Value); }

            // Regular
            var regularHeaders = authenticationObject.GetRegularHeaders();
            if (regularHeaders != null)
            { headers = headers.Concat(regularHeaders).ToDictionary(k => k.Key, v => v.Value); }

            // Pagination
            if (paginator != null)
            {
                var paginationHeaders = paginator.GetPaginationHeaders();
                if (paginationHeaders != null)
                { headers = headers.Concat(paginationHeaders).ToDictionary(k => k.Key, v => v.Value); }
            }

            return headers;
        }


        /// <summary>
        /// Gets the RESTful API endpoint's URL
        /// </summary>
        /// <returns>The URL in String format</returns>
        private static String CreateFMURL()
        {
            String url = String.Empty;

            try
            {
                url = ConfigurationManager.AppSettings.Get(FM_PUBLIC_URL_HOLDER).ToString();
            }
            catch
            { url = FM_DEFAULT_PUBLIC_URL; }

            return url;
        }

        #endregion

    }
}
