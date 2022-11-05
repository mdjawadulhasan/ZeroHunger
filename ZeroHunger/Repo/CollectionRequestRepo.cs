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

        public static List<CollectionRequestModel> Get(int id)
        {


            var db = new Entities();
            var collectionRequestModel = new List<CollectionRequestModel>();
            var colreqdb = db.CollectionRequests.Where(temp => temp.CrId == id).ToList();

            foreach (var item in colreqdb)
            {

                DateTime dt1 = (DateTime)item.Date;
                DateTime dt2 = dt1.AddHours((double)item.MaxTime);
                DateTime crnt = DateTime.Now;

                
                if (DateTime.Compare(dt2, crnt) <0)
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


        public static void Delete(int id)
        {
            var db = new Entities();
            var existingrestaurant = db.CollectionRequests.Where(temp => temp.ColId == id).FirstOrDefault();
            int count = (int)(from val in db.CollectionRequests where val.ColId == id select val.Status).SingleOrDefault();

            if (count == 0 || count==4)
            {
                db.CollectionRequests.Remove(existingrestaurant);
                db.SaveChanges();
            }
        }




        public static List<CollectionRequestModel> Get()
        {
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
    }
}