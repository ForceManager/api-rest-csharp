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
    /// This example shows:
    ///  - How to obtain the list of Available Values (Search List of Values)
    ///  - How to Search List of Values by Resource Name
    ///  - How to Search List of Values by Resource Name and using the Advance Query method
    /// </summary>
    public class Example3 : BaseExample
    {
        public Example3(UserMessage channel = null) : base(channel)
        {

        }

        public override void Execute()
        {
            // Request for the List of Values resource
            ValueResourceRequest valueRequest = new ValueResourceRequest();

            //*********************************************************************************************//
            // This code obtains the list of Available Values
            var lstAvailableResources = valueRequest.GetAvailableResources();

            // Here we'll validate it's defined a Z table called Z_tblTestLinks
            if (lstAvailableResources != null)
            {
                var Z_tblTestLinks = (from r in lstAvailableResources
                                      where r.name.Equals("Z_tblTestLinks", StringComparison.InvariantCultureIgnoreCase)
                                      select r).FirstOrDefault();

                _messageChannel.Write(String.Format("Table Z_tblTestLinks is present?: {0}", (Z_tblTestLinks != null)));
            }

            //*********************************************************************************************//
            // We obtain the List of Values for Countries, this will use its name: tblCountries
            var lstCountries = valueRequest.SearchValues("tblCountries");

            // We obtain the List of Values for Currencies, this will use its name: tblCurrency
            var lstCurrencies = valueRequest.SearchValues("tblCurrency");

            //*********************************************************************************************//
            // We obtain the List of Values for Countries using the recource's name: tblCurrency and
            // filtering those whose name starts with 'ES'
            var lstCountriesNameStartsWith = valueRequest.SearchValuesAdvanced("tblCountries", @"strName LIKE 'ES%'");

        }
    }
}
