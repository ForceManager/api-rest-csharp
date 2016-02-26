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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ws.sample.models;
using ws.sample.utility;

namespace ws.sample.factories
{
    public class RequestFactory
    {
        public static string CreateReadRequest(string fmEntity)
		{
		    return CreateReadRequest(fmEntity, null);
	    }

	    public static string CreateReadRequest(string fmEntity, string query)
        {
            try
            {
		        string url;
		        if (query != null) {
                    url = Constants.API_URL + fmEntity + "?" + Constants.PARAMETER_ADVANCED_SEARCH + "=" + query;
		        } else {
			        url = Constants.API_URL + fmEntity;
		        }

                HttpClient client = new HttpClient();
                HttpRequestMessage request = getHttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Get;

                HttpResponseMessage response = client.SendAsync(request).Result;
                string result = response.Content.ReadAsStringAsync().Result;
			    return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
	    }

        public static bool InsertEntity(string fmEntity, GenericModel obj)
        {
            try
            {
                string url;
                url = Constants.API_URL + fmEntity;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = getHttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                string postBody = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                request.Content = new StringContent(postBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                return true;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateEntity(string fmEntity, GenericModel obj)
        {
            try
            {
                string url;
                url = Constants.API_URL + fmEntity + "/" + obj.getId();

                HttpClient client = new HttpClient();
                HttpRequestMessage request = getHttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Put;
                string postBody = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                request.Content = new StringContent(postBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                return true;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteEntity(string fmEntity, GenericModel obj)
        {

            try
            {
                string url;
                url = Constants.API_URL + fmEntity + "/" + obj.getId();

                HttpClient client = new HttpClient();
                HttpRequestMessage request = getHttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Delete;

                HttpResponseMessage response = client.SendAsync(request).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                return true;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static HttpRequestMessage getHttpRequestMessage() {
            // get ApiURL, Public Key and Private Key from "fm-prop.properties" file
            // by PropUtils class
            String publicKey = Constants.publicKey;
            String privateKey = Constants.privateKey;

            long currentTimestamp = (long)DateTimeToUnixTimestamp(DateTime.Now);

            // generate signature by utility HashUtils.createMessageHash
            String signature = HashUtils.createMessageHash(publicKey, currentTimestamp.ToString(), privateKey);

            HttpRequestMessage request = new HttpRequestMessage();

            // Set request Headers (ContentType, Accept, Public Key, Timestamp,
            // Signature
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Headers.Add(Constants.FM_PUBLIC_KEY, publicKey);
            request.Headers.Add(Constants.FM_UNIX_TIMESTAMP, currentTimestamp.ToString());
            request.Headers.Add(Constants.FM_SIGNATURE, signature);

            return request;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
