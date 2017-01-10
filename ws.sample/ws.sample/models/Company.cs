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
using ws.sample.models;

namespace ws.sample
{
    public class Company : GenericModel
    {

        public string account_type_id { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string branch_id { get; set; }
        public string city_name { get; set; }
        public string comment { get; set; }
        public string country_id { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string deleted { get; set; }
        public string deleted_by { get; set; }
        public string deleted_date { get; set; }
        public string email { get; set; }
        public string ext_id { get; set; }
        public string fax { get; set; }
        public string geocode_latitude { get; set; }
        public string geocode_longitude { get; set; }
        public string geocoded { get; set; }
        public string geolocalisation_accuracy { get; set; }
        public string mobile_phone { get; set; }
        public string modified_by { get; set; }
        public string modified_date { get; set; }
        public string name { get; set; }
        public string permission_level { get; set; }
        public string phone { get; set; }
        public string phone_2 { get; set; }
        public string postcode { get; set; }
        public string province_name { get; set; }
        public string sales_rep_2_id { get; set; }
        public string sales_rep_3_id { get; set; }
        public string sales_rep_4_id { get; set; }
        public string sales_rep_5_id { get; set; }
        public string sales_rep_id { get; set; }
        public string segment_id { get; set; }
        public string vat_number { get; set; }
        public string visible_to_all { get; set; }
        public string website { get; set; }
    }
}
