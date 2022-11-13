using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.DB;
using ZeroHunger.Models;

namespace ZeroHunger.Repo
{
    public class CollectionRequestRepo
    {

        public static void Create(CollectionRequestModel crm)
        {

            var collectionreq = new CollectionRequest();
            collectionreq.CrId = crm.CrId;
            collectionreq.FoodType = crm.FoodType;
            collectionreq.MaxTime = crm.MaxTime;
            collectionreq.Date = crm.Date;
            collectionreq.Status = 0;

            var db = new Entities();
            db.CollectionRequests.Add(collectionreq);
            db.SaveChanges();

        }

        public static int checkRequest(int id)
        {
            var db = new Entities();
            int count = db.CollectionRequests.Where(temp => temp.CrId == id && temp.Status < 2).ToList().Count;
            return count;
        }


        public static void  UpdateStatus()
        {
            var db = new Entities();
            var colreqdb = db.CollectionRequests.ToList();

            foreach (var item in colreqdb)
            {

                DateTime dt1 = (DateTime)item.Date;
                DateTime dt2 = dt1.AddHours((double)item.MaxTime);
                DateTime crnt = DateTime.Now;


                if (DateTime.Compare(dt2, crnt) < 0)
                {
                    item.ColId = item.ColId;
                    item.CrId = item.CrId;
                    item.FoodType = item.FoodType;
                    item.MaxTime = item.MaxTime;
                    item.Date = item.Date;
                    item.CempId = item.CempId;
                    item.Status = 4;
                    db.SaveChanges();
                }
            }
        }

        public static List<CollectionRequestModel> GetReq(int id)
        {


            var db = new Entities();
            var collectionRequestModel = new List<CollectionRequestModel>();
           
            UpdateStatus();
            var colreqdb = db.CollectionRequests.Where(t=>t.CrId==id).ToList();

            foreach (var item in colreqdb)
            {
                collectionRequestModel.Add(new CollectionRequestModel()
                {
                    ColId = item.ColId,
                    CrId = item.CrId,
                    FoodType = item.FoodType,
                    MaxTime = (int)item.MaxTime,
                    Date = item.Date,
                    CempId = item.CempId,
                    Status = item.Status,
                    


                });
            }
            return collectionRequestModel;
        }


        public static void Delete(int id)
        {
            var db = new Entities();
            var existingrestaurant = db.CollectionRequests.Where(temp => temp.ColId == id).FirstOrDefault();
            int count = (int)(from val in db.CollectionRequests where val.ColId == id select val.Status).SingleOrDefault();

            if (count == 0 || count == 4)
            {
                db.CollectionRequests.Remove(existingrestaurant);
                db.SaveChanges();
            }
        }



        public static List<CollectionRequestModel> Get()
        {
            UpdateStatus();
            var db = new Entities();
            var collectionRequestModel = new List<CollectionRequestModel>();
            var colreqdb = db.CollectionRequests.Where(temp => temp.Status == 0).ToList();

            foreach (var item in colreqdb)
            {
                collectionRequestModel.Add(new CollectionRequestModel()
                {
                    ColId = item.ColId,
                    CrId = item.CrId,
                    FoodType = item.FoodType,
                    MaxTime = (int)item.MaxTime,
                    Date = item.Date,
                    CempId = item.CrId,
                    Status = item.Status,


                });
            }
            return collectionRequestModel;
        }


        public static string AssignEmp(int id)
        {
            var db = new Entities();
            int count = db.Employees.Where(temp => temp.EmpStatus == 0).ToList().Count;

            if (count >= 1)
            {

                var avalemp = db.Employees.Where(temp => temp.EmpStatus == 0).FirstOrDefault();
                avalemp.Empid = avalemp.Empid;
                avalemp.EmpName = avalemp.EmpName;
                avalemp.EmpAge = avalemp.EmpAge;
                avalemp.EmpAdd = avalemp.EmpAdd;
                avalemp.EmpPhone = avalemp.EmpPhone;
                avalemp.EmpPassword = avalemp.EmpPassword;
                avalemp.EmpStatus = 1;
                db.SaveChanges();



                var colreq = db.CollectionRequests.Where(temp => temp.ColId == id).FirstOrDefault();
                colreq.ColId = colreq.ColId;
                colreq.CrId = colreq.CrId;
                colreq.FoodType = colreq.FoodType;
                colreq.MaxTime = colreq.MaxTime;
                colreq.Date = colreq.Date;
                colreq.CempId = avalemp.Empid;
                colreq.Status = 1;
                db.SaveChanges();

                return "assigned";
            }

            return "notassigned";
        }


        public static List<CollectionRequestModel> GetProgress()
        {
            var db = new Entities();
            var collectionRequestModel = new List<CollectionRequestModel>();
            var colreqdb = db.CollectionRequests.Where(temp => temp.Status != 0).ToList();

            foreach (var item in colreqdb)
            {
                collectionRequestModel.Add(new CollectionRequestModel()
                {
                    ColId = item.ColId,
                    CrId = item.CrId,
                    FoodType = item.FoodType,
                    MaxTime = (int)item.MaxTime,
                    Date = item.Date,
                    CempId = item.CrId,
                    Status = item.Status,


                });
            }
            return collectionRequestModel;
        }



        public static void CompleteReq(int id, EmployeeModel em)
        {
            var db = new Entities();

            var colreq = new CollectionRequest { ColId = id, Status = 2 };
            db.CollectionRequests.Attach(colreq);
            db.Entry(colreq).Property(x => x.Status).IsModified = true;
            db.SaveChanges();





            var avalemp = db.Employees.Where(temp => temp.Empid == em.Empid).FirstOrDefault();
            avalemp.Empid = avalemp.Empid;
            avalemp.EmpName = avalemp.EmpName;
            avalemp.EmpAge = avalemp.EmpAge;
            avalemp.EmpAdd = avalemp.EmpAdd;
            avalemp.EmpPhone = avalemp.EmpPhone;
            avalemp.EmpPassword = avalemp.EmpPassword;
            avalemp.EmpStatus = 0;
            db.SaveChanges();



        }




        public static List<CollectionRequestModel> GetAvlReq(int id)
        {


            var db = new Entities();
            var collectionRequestModel = new List<CollectionRequestModel>();
            var colreqdb = db.CollectionRequests.Where(temp => temp.CempId == id && temp.Status == 1).ToList();


            foreach (var item in colreqdb)
            {
                collectionRequestModel.Add(new CollectionRequestModel()
                {
                    ColId = item.ColId,
                    CrId = item.CrId,
                    FoodType = item.FoodType,
                    MaxTime = (int)item.MaxTime,
                    Date = item.Date,
                    CempId = item.CrId,
                    Status = item.Status,


                });
            }
            return collectionRequestModel;
        }





        public static CollectionRequest Getcollection(int id)
        {
            var db = new Entities();
            var collectionRequest = new CollectionRequest();
            var item = db.CollectionRequests.Find(id);
            collectionRequest.ColId = item.ColId;
            collectionRequest.CrId = item.CrId;
            collectionRequest.FoodType = item.FoodType;
            collectionRequest.MaxTime = item.MaxTime;
            collectionRequest.Date = item.Date;
            collectionRequest.CempId = item.CempId;
            collectionRequest.Status = item.Status;
            return collectionRequest;


        }



        public static void CompleteDistribution(int id)
        {
            var db = new Entities();

            var colreq = new CollectionRequest { ColId = id, Status = 3 };
            db.CollectionRequests.Attach(colreq);
            db.Entry(colreq).Property(x => x.Status).IsModified = true;
            db.SaveChanges();

        }
    }
}