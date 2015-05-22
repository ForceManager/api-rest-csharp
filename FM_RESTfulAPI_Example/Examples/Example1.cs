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
using FM_RESTfulAPI_Example.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Examples
{
    public class Example1 : BaseExample
    {

        public Example1() : base(null)
        {

        }


        public override void Execute()
        {
            StandardRequest<Company> companyRequest = new StandardRequest<Company>(ModelType.Models.Company);

            // First, we create a new company
            Company data = GetSampleObject();
            
            // Save the entity on FM
            GenericIdDescription processed = companyRequest.CreateEntity(data);

            if (processed != null)
            {
                // We search the company by id
                Company tmpCompany = companyRequest.SearchByEntityId(processed.id);

                if (tmpCompany == null)
                { throw new Exception("This is weird, should not happen"); }
                else
                {
                    UpdateSampleObject(ref tmpCompany);
                    bool updated = companyRequest.UpdateEntity(processed.id, tmpCompany);

                    bool deleted = companyRequest.DeleteEntity(processed.id);
                }
            }

        }


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
    }
}
