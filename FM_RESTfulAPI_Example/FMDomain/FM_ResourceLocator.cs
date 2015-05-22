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
using FM_RESTfulAPI_Example.Models;

namespace FM_RESTfulAPI_Example.FMDomain
{

    /// <summary>
    /// This class will resolve the resource address based on the model we want to operate
    /// </summary>
    public static class ResourceLocator
    {

        // Constants for the resources
        private const String COMPANY_RESOURCE                               = "companies";
        private const String CONTACT_RESOURCE                               = "contacts";
        private const String ACTIVITY_RESOURCE                              = "activities";
        private const String OPPORTUNITY_RESOURCE                           = "opportunities";
        private const String PRODUCT_RESOURCE                               = "products";
        private const String VALUE_RESOURCE                                 = "values";
        private const String VALUE_INFO_RESOURCE                            = "values/info";
        private const String INTERNALID_RESOURCE                            = "internalids";


        /// <summary>
        /// Obtains the resource address based on the Model we will use
        /// </summary>
        /// <param name="modelType">The model we'll use</param>
        /// <returns>A string with the address</returns>
        public static String GetResourceLocator(ModelType.Models modelType)
        {
            String resource = String.Empty;
            switch (modelType)
            {
                case ModelType.Models.Company:
                    resource = COMPANY_RESOURCE;
                    break;
                case ModelType.Models.Contact:
                    resource = CONTACT_RESOURCE;
                    break;
                case ModelType.Models.Activity:
                    resource = ACTIVITY_RESOURCE;
                    break;
                case ModelType.Models.Opportunity:
                    resource = OPPORTUNITY_RESOURCE;
                    break;
                case ModelType.Models.Product:
                    resource = PRODUCT_RESOURCE;
                    break;
                case ModelType.Models.Value:
                    resource = VALUE_RESOURCE;
                    break;
                case ModelType.Models.ValueInfo:
                    resource = VALUE_INFO_RESOURCE;
                    break;
                case ModelType.Models.InternalId:
                    resource = INTERNALID_RESOURCE;
                    break;
                default:
                    // This should never happen
                    throw new Exception("GetResourceLocator is unable to find the resource for the specified type");
            }

            return resource;
        }

    }
}
