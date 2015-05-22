using FM_RESTfulAPI_Example.Models;
using FM_RESTfulAPI_Example.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_RESTfulAPI_Example.Examples
{
    public class Test_To_Delete_1
    {
        public void Execute()
        {
            StandardRequest<Company> companyRequest = new StandardRequest<Company>(ModelType.Models.Company);

            var data = companyRequest.SearchEntityAdvanced("province_name='Barcelona' AND mobile_phone IS NULL");

            if (data != null)
            {
                Console.WriteLine("N companies: " + data.Count);

                foreach (Company company in data)
                {
                    var c1 = companyRequest.SearchByEntityId(Int32.Parse(company.id));

                    if (c1 != null)
                    {
                        Console.WriteLine("Ok, company obtained: " + c1.id);
                    }
                    else
                    {
                        Console.WriteLine("NOk, company NOT obtained: " + company.id);
                    }
                }

            }
        }
    }
}
