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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FM_RESTfulAPI_Example.FMDomain
{
    public class FM_Authentication
    {
        // App configuration
        protected const String FM_PUBLIC_KEY_CONFIG_HOLDER              = "FM_Public_Key";
        protected const String FM_PRIVATE_KEY_CONFIG_HOLDER             = "FM_Private_Key";

        // Authetication
        public const String FM_API_KEY_HEADER                           = "X-FM-PublicKey";
        public const String FM_API_HASH_HEADER                          = "X-FM-Signature";
        public const String FM_API_TS_HEADER                            = "X-FM-UnixTimestamp";

        // Standard headers
        public const String FM_API_ACCEPT_HEADER                        = "Accept";
        public const String FM_API_CONTENT_TYPE_HEADER                  = "Content-Type";

        // Standard values for headers
        public const String FM_API_COMM_FORMAT                          = "application/json";


        /// <summary>
        /// Calculates the headers required for authentication
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> GetAuthenticationHeaders()
        {
            String publicKey = String.Empty;
            String privateKey = String.Empty;
            long currentTimestamp = 0;
            String messageHash = String.Empty;
            Dictionary<String, String> result = new Dictionary<String, String>();

            if (CreateMessageHash(out publicKey, out currentTimestamp, out messageHash))
            {
                result.Add(FM_API_KEY_HEADER, publicKey);
                result.Add(FM_API_HASH_HEADER, messageHash);
                result.Add(FM_API_TS_HEADER, currentTimestamp.ToString());
            }

            return result;
        }


        /// <summary>
        /// Get several standard headers used in FM RESTful API
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> GetRegularHeaders()
        {
            Dictionary<String, String> result = new Dictionary<String, String>();

            result.Add("Accept", "application/json");
            result.Add("Content-Type", "application/json");

            return result;
        }


        /// <summary>
        /// Calculates the message signature or hash
        /// </summary>
        /// <param name="publicKey">Public RESTful API Key</param>
        /// <param name="currentTimestamp">Current Unix Timestamp</param>
        /// <param name="messageHash">Private RESTful API Key</param>
        /// <returns></returns>
        protected bool CreateMessageHash(out String publicKey, out long currentTimestamp, out String messageHash)
        {
            publicKey = String.Empty;
            currentTimestamp = 0;
            messageHash = String.Empty;
            bool created = false;

            try
            {
                // Both keys should be stored encrypted, then, on this point, they should be decrypted.
                publicKey = ConfigurationManager.AppSettings.Get(FM_PUBLIC_KEY_CONFIG_HOLDER).ToString();        // decrypt it
                String privateKey = ConfigurationManager.AppSettings.Get(FM_PRIVATE_KEY_CONFIG_HOLDER).ToString();      // decrypt it
                currentTimestamp = DateTimeHelper.GetUnixTimeFromDateTime(DateTime.Now);

                String toHash = String.Format("{0}{1}{2}", currentTimestamp, publicKey, privateKey);
                messageHash = EncryptHelper.SHA1HashStringForUTF8String(toHash);
                created = true;
            }
            catch (Exception)
            {
                // Error handling                
            }
            
            return created;
        }

    }
}
