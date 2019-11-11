using ETZWeb.Core.Interfaces.Repositories;
using ETZWeb.Entities;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETZWeb.Infrastructure.GoogleFirebase
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IFirebaseConfig _config;
        private readonly IFirebaseClient _client;
        public EmployeeRepository(string authSecret, string basePath)
        {
            _config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath,
            };

            _client = new FirebaseClient(_config);
        }
        public List<Employee> GetEmployees()
        {
            var listEmployee = new List<Employee>();

            FirebaseResponse response = _client.Get("employees/");

            var json = JsonConvert.DeserializeObject(response.Body);
            foreach (JToken item in ((JToken)(json)).Children())
            {
                var auxEmployee = new Employee();
                var prop = ((JToken)(JsonConvert.DeserializeObject(((JProperty)(item)).Value.ToString()))).Children();
                
                foreach (var obj in prop)
                {
                    var ser = (JProperty)(obj);
                    if (ser.Name == "ID")
                        auxEmployee.ID = ser.Value.ToString();
                    else if (ser.Name == "Name")
                        auxEmployee.Name = ser.Value.ToString();
                    else if (ser.Name == "Location")
                        auxEmployee.Location = ser.Value.ToString();
                    else if (ser.Name == "Position")
                        auxEmployee.Position = ser.Value.ToString();
                    else if (ser.Name == "BirthDate")
                        auxEmployee.BirthDate = Convert.ToDateTime(ser.Value.ToString());
                    else if (ser.Name == "Sex")
                        auxEmployee.Sex = (SexEnum)Convert.ToInt32(ser.Value.ToString()); 
                }

                listEmployee.Add(auxEmployee);
            }

            return listEmployee;
        }

        public void AddEmployee()
        {
            var employee = new Employee()
            {
                ID = Guid.NewGuid().ToString(),
                Location = "Glasgow",
                Name = "Miss Lisa Morrow",
                Position = "Business Manager",
                Sex = SexEnum.Female,
                BirthDate = DateTime.Now
            };

            var responseSet = _client.Set("employees/" + employee.ID, employee);
            //var employee = response.ResultAs<Employee>();
        }
    }
}
