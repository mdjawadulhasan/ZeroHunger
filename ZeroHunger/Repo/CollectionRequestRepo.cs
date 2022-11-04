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
            
            var db = new Entities();
            db.CollectionRequests.Add(collectionreq);
            db.SaveChanges();

        }
    }
}