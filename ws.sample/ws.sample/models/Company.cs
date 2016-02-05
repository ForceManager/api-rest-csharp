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

        // Standard fields //
        private String account_type_id;
        private String address_1;
        private String address_2;
        private String branch_id;
        private String city_name;
        private String comment;
        private String country_id;
        private String created_by;
        private DateTime created_date;
        private String deleted;
        private String deleted_by;
        private String deleted_date;
        private String email;
        private String ext_id;
        private String fax;
        private String geocode_latitude;
        private String geocode_longitude;
        private String geocoded;
        private String geolocalisation_accuracy;
        private String id;
        private String mobile_phone;
        private String modified_by;
        private DateTime modified_date;
        private String name;
        private String permission_level;
        private String phone;
        private String phone_2;
        private String postcode;
        private String province_name;
        private String RowNumber;
        private String sales_rep_2_id;
        private String sales_rep_3_id;
        private String sales_rep_4_id;
        private String sales_rep_5_id;
        private String sales_rep_id;
        private String segment_id;
        private String vat_number;
        private String visible_to_all;
        private String website;

        // Extra fields //
        private String Z_DateExtraField;
        private String Z_Decimal;
        private String Z_DeCurrency;
        private String Z_DPA_MultiValue;
        private String Z_parentnonull;
        private String Z_parentnull;
        private String Z_TestCurrency;

        public String getAccount_type_id()
        {
            return account_type_id;
        }

        public void setAccount_type_id(String account_type_id)
        {
            this.account_type_id = account_type_id;
        }

        public String getAddress_1()
        {
            return address_1;
        }

        public void setAddress_1(String address_1)
        {
            this.address_1 = address_1;
        }

        public String getAddress_2()
        {
            return address_2;
        }

        public void setAddress_2(String address_2)
        {
            this.address_2 = address_2;
        }

        public String getBranch_id()
        {
            return branch_id;
        }

        public void setBranch_id(String branch_id)
        {
            this.branch_id = branch_id;
        }

        public String getCity_name()
        {
            return city_name;
        }

        public void setCity_name(String city_name)
        {
            this.city_name = city_name;
        }

        public String getComment()
        {
            return comment;
        }

        public void setComment(String comment)
        {
            this.comment = comment;
        }

        public String getCountry_id()
        {
            return country_id;
        }

        public void setCountry_id(String country_id)
        {
            this.country_id = country_id;
        }

        public String getCreated_by()
        {
            return created_by;
        }

        public void setCreated_by(String created_by)
        {
            this.created_by = created_by;
        }

        public DateTime getCreated_date()
        {
            return created_date;
        }

        public void setCreated_date(DateTime created_date)
        {
            this.created_date = created_date;
        }

        public String getDeleted()
        {
            return deleted;
        }

        public void setDeleted(String deleted)
        {
            this.deleted = deleted;
        }

        public String getDeleted_by()
        {
            return deleted_by;
        }

        public void setDeleted_by(String deleted_by)
        {
            this.deleted_by = deleted_by;
        }

        public String getDeleted_date()
        {
            return deleted_date;
        }

        public void setDeleted_date(String deleted_date)
        {
            this.deleted_date = deleted_date;
        }

        public String getEmail()
        {
            return email;
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

        public String getExt_id()
        {
            return ext_id;
        }

        public void setExt_id(String ext_id)
        {
            this.ext_id = ext_id;
        }

        public String getFax()
        {
            return fax;
        }

        public void setFax(String fax)
        {
            this.fax = fax;
        }

        public String getGeocode_latitude()
        {
            return geocode_latitude;
        }

        public void setGeocode_latitude(String geocode_latitude)
        {
            this.geocode_latitude = geocode_latitude;
        }

        public String getGeocode_longitude()
        {
            return geocode_longitude;
        }

        public void setGeocode_longitude(String geocode_longitude)
        {
            this.geocode_longitude = geocode_longitude;
        }

        public String getGeocoded()
        {
            return geocoded;
        }

        public void setGeocoded(String geocoded)
        {
            this.geocoded = geocoded;
        }

        public String getGeolocalisation_accuracy()
        {
            return geolocalisation_accuracy;
        }

        public void setGeolocalisation_accuracy(String geolocalisation_accuracy)
        {
            this.geolocalisation_accuracy = geolocalisation_accuracy;
        }

        public String getId()
        {
            return id;
        }

        public void setId(String id)
        {
            this.id = id;
        }

        public String getMobile_phone()
        {
            return mobile_phone;
        }

        public void setMobile_phone(String mobile_phone)
        {
            this.mobile_phone = mobile_phone;
        }

        public String getModified_by()
        {
            return modified_by;
        }

        public void setModified_by(String modified_by)
        {
            this.modified_by = modified_by;
        }

        public DateTime getModified_date()
        {
            return modified_date;
        }

        public void setModified_date(DateTime modified_date)
        {
            this.modified_date = modified_date;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getPermission_level()
        {
            return permission_level;
        }

        public void setPermission_level(String permission_level)
        {
            this.permission_level = permission_level;
        }

        public String getPhone()
        {
            return phone;
        }

        public void setPhone(String phone)
        {
            this.phone = phone;
        }

        public String getPhone_2()
        {
            return phone_2;
        }

        public void setPhone_2(String phone_2)
        {
            this.phone_2 = phone_2;
        }

        public String getPostcode()
        {
            return postcode;
        }

        public void setPostcode(String postcode)
        {
            this.postcode = postcode;
        }

        public String getProvince_name()
        {
            return province_name;
        }

        public void setProvince_name(String province_name)
        {
            this.province_name = province_name;
        }

        public String getRowNumber()
        {
            return RowNumber;
        }

        public void setRowNumber(String rowNumber)
        {
            RowNumber = rowNumber;
        }

        public String getSales_rep_2_id()
        {
            return sales_rep_2_id;
        }

        public void setSales_rep_2_id(String sales_rep_2_id)
        {
            this.sales_rep_2_id = sales_rep_2_id;
        }

        public String getSales_rep_3_id()
        {
            return sales_rep_3_id;
        }

        public void setSales_rep_3_id(String sales_rep_3_id)
        {
            this.sales_rep_3_id = sales_rep_3_id;
        }

        public String getSales_rep_4_id()
        {
            return sales_rep_4_id;
        }

        public void setSales_rep_4_id(String sales_rep_4_id)
        {
            this.sales_rep_4_id = sales_rep_4_id;
        }

        public String getSales_rep_5_id()
        {
            return sales_rep_5_id;
        }

        public void setSales_rep_5_id(String sales_rep_5_id)
        {
            this.sales_rep_5_id = sales_rep_5_id;
        }

        public String getSales_rep_id()
        {
            return sales_rep_id;
        }

        public void setSales_rep_id(String sales_rep_id)
        {
            this.sales_rep_id = sales_rep_id;
        }

        public String getSegment_id()
        {
            return segment_id;
        }

        public void setSegment_id(String segment_id)
        {
            this.segment_id = segment_id;
        }

        public String getVat_number()
        {
            return vat_number;
        }

        public void setVat_number(String vat_number)
        {
            this.vat_number = vat_number;
        }

        public String getVisible_to_all()
        {
            return visible_to_all;
        }

        public void setVisible_to_all(String visible_to_all)
        {
            this.visible_to_all = visible_to_all;
        }

        public String getWebsite()
        {
            return website;
        }

        public void setWebsite(String website)
        {
            this.website = website;
        }

        public String getZ_DateExtraField()
        {
            return Z_DateExtraField;
        }

        public void setZ_DateExtraField(String z_DateExtraField)
        {
            Z_DateExtraField = z_DateExtraField;
        }

        public String getZ_Decimal()
        {
            return Z_Decimal;
        }

        public void setZ_Decimal(String z_Decimal)
        {
            Z_Decimal = z_Decimal;
        }

        public String getZ_DeCurrency()
        {
            return Z_DeCurrency;
        }

        public void setZ_DeCurrency(String z_DeCurrency)
        {
            Z_DeCurrency = z_DeCurrency;
        }

        public String getZ_DPA_MultiValue()
        {
            return Z_DPA_MultiValue;
        }

        public void setZ_DPA_MultiValue(String z_DPA_MultiValue)
        {
            Z_DPA_MultiValue = z_DPA_MultiValue;
        }

        public String getZ_parentnonull()
        {
            return Z_parentnonull;
        }

        public void setZ_parentnonull(String z_parentnonull)
        {
            Z_parentnonull = z_parentnonull;
        }

        public String getZ_parentnull()
        {
            return Z_parentnull;
        }

        public void setZ_parentnull(String z_parentnull)
        {
            Z_parentnull = z_parentnull;
        }

        public String getZ_TestCurrency()
        {
            return Z_TestCurrency;
        }

        public void setZ_TestCurrency(String z_TestCurrency)
        {
            Z_TestCurrency = z_TestCurrency;
        }
    }
}
