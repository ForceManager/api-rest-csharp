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

using FM_RESTfulAPI_Example.FMDomain;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_RESTfulAPI_Example.Models;

namespace FM_RESTfulAPI_Example.Requests
{
    /// <summary>
    /// This class is used to perform generic FM RESTful API requests. 
    /// This kind of requests are those involving regular objects in the FM System, like
    /// Companies, Contacts, Opportunities, etc...
    /// </summary>
    /// <typeparam name="T">The kind of object (model) we are trying to obtain</typeparam>
    public class StandardRequest<T> where T: Models.IModel
    {
        /// <summary>
        /// The constructor will initialize this field, defining this model will tell
        /// which resource to use
        /// </summary>
        protected ModelType.Models _currentModel = ModelType.Models.Company;


        public StandardRequest(ModelType.Models model)
        {
            _currentModel = model;
        }


        #region READ Requests (Selects)

        /// <summary>
        /// Implements the "Search all entities" functionality
        /// </summary>
        /// <returns>A list of objects</returns>
        public List<T> SearchAllEntities()
        {
            return SearchEntitiesByImportantFields(null);
        }

        /// <summary>
        /// Implements the "Search by important fields" functionality
        /// </summary>
        /// <param name="filter">Dictionary with the filters (Key = parameter Name, Value = parameter Value)</param>
        /// <returns>A list of objects</returns>
        public List<T> SearchEntitiesByImportantFields(Dictionary<String, String> filter)
        {
            var result = new List<T>();
            IRestResponse response = null;
            bool finished = false;
            FM_Pagination paginator = null;

            do
            {
                var request = FM_RequestFactory.CreateReadRequest(_currentModel, filter, paginator);
                if (request != null)
                {
                    response = FM_RequestFactory.Client.Execute(request);
                    finished = FM_RequestProcessor.ProcessPaginatedResponse<T>(response, ref result, out paginator);
                }
                else
                { finished = true; }

            } while (!finished);


            return result;
        }
        
        /// <summary>
        /// Implements the "Search by entity id" functionality
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>A list of objects</returns>
        public T SearchByEntityId(int id)
        {
            T result = default(T);
            var request = FM_RequestFactory.CreateReadRequestById(_currentModel, id);
            var response = FM_RequestFactory.Client.Execute(request);
            result = FM_RequestProcessor.ProcessSingleObjectResponse<T>(response);

            return result;
        }
        
        /// <summary>
        /// Implements the "Advanced Search" functionality
        /// </summary>
        /// <param name="query">The advanced search query string</param>
        /// <returns>A list of objects</returns>
        public List<T> SearchEntityAdvanced(String query)
        {
            var result = new List<T>();
            IRestResponse response = null;
            bool finished = false;
            FM_Pagination paginator = null;

            do
            {
                var request = FM_RequestFactory.CreateReadAdvancedRequest(_currentModel, query, paginator);
                if (request != null)
                {
                    response = FM_RequestFactory.Client.Execute(request);
                    finished = FM_RequestProcessor.ProcessPaginatedResponse<T>(response, ref result, out paginator);
                }
                else
                { finished = true; }


            } while (!finished);

            return result;
        }
        
        #endregion


        #region PUT Requests (Updates)
        
        /// <summary>
        /// Updates an entity on the FM System
        /// </summary>
        /// <param name="id">The entity's id</param>
        /// <param name="input">A model representing with the update</param>
        /// <returns>True when updated, otherwise false</returns>
        public bool UpdateEntity(int id, T input)
        {
            bool updated = false;
            IRestResponse response = null;

            if (input != null)
            {
                var request = FM_RequestFactory.CreateUpdateRequest(_currentModel, id, input);
                if (request != null)
                {
                    response = FM_RequestFactory.Client.Execute(request);
                    updated = FM_RequestProcessor.ProcessUpdateRequestResponse(response);
                }
            }

            return updated;
        }
        
        #endregion


        #region POST Requests (Inserts)

        /// <summary>
        /// Inserts an entity on the FM System
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A GenericIdDescription object with the id of the new created entity and a status message.
        /// Null if the object wasn't created</returns>
        public GenericIdDescription CreateEntity(T input)
        {
            GenericIdDescription data = null;
            IRestResponse response = null;

            var request = FM_RequestFactory.CreateInsertRequest(_currentModel, input);
            if (request != null)
            {
                response = FM_RequestFactory.Client.Execute(request);
                data = FM_RequestProcessor.ProcessCreateRequestResponse(response);
            }

            return data;
        }

        #endregion


        #region DEL Requests (Deletes)

        /// <summary>
        /// Deletes an entity on the FM System
        /// </summary>
        /// <param name="id">The entity's id</param>
        /// <returns>True when deleted, otherwise false</returns>
        public bool DeleteEntity(int id, T input = default(T))
        {
            bool deleted = false;
            IRestResponse response = null;

            var request = FM_RequestFactory.CreateDeleteRequest(_currentModel, id, input);
            if (request != null)
            {
                response = FM_RequestFactory.Client.Execute(request);
                deleted = FM_RequestProcessor.ProcessDeleteRequestResponse(response);
            }

            return deleted;
        }

        #endregion

    }
}
