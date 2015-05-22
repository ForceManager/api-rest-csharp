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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using FM_RESTfulAPI_Example.Models;

namespace FM_RESTfulAPI_Example.FMDomain
{
    public class FM_Pagination
    {
        // Pagination
        public const String API_PAGE_NUMBER_HEADER                              = "X-FM-Page";
        public const String API_NEXT_PAGE_NUMBER_HEADER                         = "X-FM-Next-Page";
        public const String API_PREV_PAGE_NUMBER_HEADER                         = "X-FM-Prev-Page";
        public const String API_NUMBER_RECORDS                                  = "X-FM-Entity-Count";


        #region Properties

        // These properties are used by some methods to know the status of the pagination
        // when performing GET requests (Usually on FM_RequestProcessor)

        protected int currentPage = 0;
        public int CurrentPage
        {
            get { return currentPage; }
            private set { }
        }

        protected int nextPage = 0;
        public int NextPage
        {
            get { return nextPage; }
            private set { }
        }

        protected int previousPage = 0;
        public int PreviousPage
        {
            get { return previousPage; }
            private set { }
        }

        protected int entityCount = 0;
        public int EntityCount
        {
            get { return entityCount; }
            private set { }
        }

        public bool HasMorePages
        {
            get
            {
                return (currentPage != nextPage);
            }
            private set { }
        }

        #endregion
               


        /// <summary>
        /// Constructor. Will set/calculate all the object's properties using the
        /// FM RESTful API response
        /// </summary>
        /// <param name="response">The request response returned by the FM RESTful API</param>
        public FM_Pagination(IRestResponse response)
        {
            if (response != null && response.Headers != null)
            {

                var responseHeaders = (from h in response.Headers
                                       where h.Type == ParameterType.HttpHeader
                                       select h);

                if (responseHeaders != null)
	            {
                    int tmpInt = 0;
		            foreach (Parameter parameter in responseHeaders)
                    {
                        if (parameter.Name.Equals(API_PAGE_NUMBER_HEADER, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (int.TryParse(parameter.Value.ToString(), out tmpInt))
                            { currentPage = tmpInt; }
                        }
                        else if (parameter.Name.Equals(API_NEXT_PAGE_NUMBER_HEADER, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (int.TryParse(parameter.Value.ToString(), out tmpInt))
                            { nextPage = tmpInt; }
                        }
                        else if (parameter.Name.Equals(API_PREV_PAGE_NUMBER_HEADER, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (int.TryParse(parameter.Value.ToString(), out tmpInt))
                            { previousPage = tmpInt; }
                        }
                        else if (parameter.Name.Equals(API_NUMBER_RECORDS, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (int.TryParse(parameter.Value.ToString(), out tmpInt))
                            { entityCount = tmpInt; }

                        } // if (parameter.Name.Equals(API_PAGE_NUMBER_HEADER, ...

                    } // foreach (Parameter parameter in responseHeaders)

                } // if (responseHeaders != null)

            } // if (response != null && response.Headers != null)

        }


        /// <summary>
        /// Will return the headers required for pagination
        /// </summary>
        /// <returns>Dictionary with the pagination headers and their values</returns>
        public Dictionary<String, String> GetPaginationHeaders()
        {
            Dictionary<String, String> result = new Dictionary<String, String>()
            { 
                { API_PAGE_NUMBER_HEADER, nextPage.ToString()}
            };
            // result.Add(API_PAGE_NUMBER_HEADER, nextPage.ToString());
            
            return result;
        }


    }
}
