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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Examples
{
    /// <summary>
    /// This example shows how to:
    ///  - Create an object in FM System
    ///  - Search the same object by id
    ///  - Perform some update on the searched object and save the changes on FM
    ///  - Delete the object on FM
    /// </summary>
    public class Example1 : BaseExample
    {
        public Example1(UserMessage channel = null) : base(channel)
        {

        }


        public override void Execute()
        {
            _messageChannel.Write("Running example 1");

            // Request for the Company resource
            StandardRequest<Company> companyRequest = new StandardRequest<Company>(ModelType.Models.Company);

            // First, company is created on memory
            Company companyData = GetSampleObject();
            
            // The object is saved on FM
            GenericIdDescription processed = companyRequest.CreateEntity(companyData);
            companyData = null;

            if (processed != null)
            {
                _messageChannel.Write(String.Format("The company was created with id: {0}", processed.id));
                
                // The company is searched by id (the id we obtained in the creation)
                Company tmpCompany = companyRequest.SearchByEntityId(processed.id);

                if (tmpCompany == null)
                { throw new Exception("This is weird, should not happen"); }
                else
                {
                    // The object as changed on memory
                    UpdateSampleObject(ref tmpCompany);

                    // The object is updated in FM
                    bool updated = companyRequest.UpdateEntity(processed.id, tmpCompany);

                    // The company is deleted on FM
                    bool deleted = companyRequest.DeleteEntity(processed.id);
                }
            }
            else
            {
                _messageChannel.Write("The system was unable to create the company", UserMessage.MessageLevel.Error);
            }
        }


        #region Internal Methods


        protected Company GetSampleObject()
        {
            Company result = new Company()
            {
                // Mandatory fields
                name = "Test RESTful API",
                account_type_id = "23",

                // Optional fields
                phone = "817271761",
                branch_id = "17",
                ext_id = "112341"
            };

            return result;
        }

       
        protected void UpdateSampleObject(ref Company tmpCompany)
        {
            if (tmpCompany != null)
            {
                // We update some fields
                tmpCompany.city_name = "Barcelona";
                tmpCompany.mobile_phone = "0034567123456";
                tmpCompany.ext_id = "112399";

                // And also delete this one
                tmpCompany.phone = "";
            }
        }


        #endregion

    }
}
