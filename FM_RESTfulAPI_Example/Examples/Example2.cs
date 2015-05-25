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

using FM_RESTfulAPI_Example.Helpers;
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
    /// This example shows how to search using:
    ///  - Important Fields
    ///  - Advanced Search
    /// I this case, we'll search companies 
    /// </summary>
    public class Example2 : BaseExample
    {
        public Example2(UserMessage channel = null) : base(channel)
        {

        }

       
        public override void Execute()
        {
            // Request for the Company resource
            StandardRequest<Company> companyRequest = new StandardRequest<Company>(ModelType.Models.Company);

            //*********************************************************************************************//
            // Searching companies by postcode using Important Fields method
            // For companies, the postcode is defined as one of important fields
            String postCode = "08034";
            Dictionary<String, String> filter = new Dictionary<string, string> { { "postcode", postCode } };

            _messageChannel.Write("Searching companies with postcode = " + postCode);
            var lstCompaniesPostCode = companyRequest.SearchEntitiesByImportantFields(filter);


            //*********************************************************************************************//
            // Searching companies by postcode which starts with 08 using Advance Search method
            String advancedQuery = @"postcode LIKE '08%'";
            _messageChannel.Write("Searching companies with query = " + advancedQuery);
            var lstCompaniesPostCodeStartsWith = companyRequest.SearchEntityAdvanced(advancedQuery);

        }
    }
}
