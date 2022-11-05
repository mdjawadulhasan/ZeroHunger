using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.DB;
using ZeroHunger.Models;

namespace ZeroHunger.Repo
{
    public class EmployeeRepo
    {
        public static void Create(EmployeeModel empr)
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");

            var emp = new Employee();
            emp.EmpName = empr.EmpName;
            emp.EmpAge = empr.EmpAge;
            emp.EmpAdd = empr.EmpAdd;
            emp.EmpPhone = empr.EmpPhone;
            emp.EmpUserName = empr.EmpUserName + r[2];
            emp.EmpPassword = r;
            emp.EmpStatus = 0;

            var db = new Entities();
            db.Employees.Add(emp);
            db.SaveChanges();

        }



        public static List<EmployeeModel> Get()
        {
            var db = new Entities();
            var employees = new List<EmployeeModel>();
            foreach (var empr in db.Employees)
            {
                employees.Add(new EmployeeModel()
                {
                    EmpName = empr.EmpName,
                    EmpAge = empr.EmpAge,
                    EmpAdd = empr.EmpAdd,
                    EmpPhone = empr.EmpPhone,
                    EmpUserName = empr.EmpUserName,
                    EmpPassword = empr.EmpPassword,
                    EmpStatus = empr.EmpStatus,

                });
            }
            return employees;
        }
    }
}