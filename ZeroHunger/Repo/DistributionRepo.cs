using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.DB;
using ZeroHunger.Models;

namespace ZeroHunger.Repo
{
    public class DistributionRepo
    {
        public static void Create(DistributionListModel dbr)
        {
            
            var DModel = new DistributionList();
            DModel.FoodType = dbr.FoodType;
            DModel.Date = dbr.Date;
            DModel.Place = dbr.Place;
            var db = new Entities();
            db.DistributionLists.Add(DModel);
            db.SaveChanges();

        }


        public static List<DistributionListModel> getAll()
        {
            var db = new Entities();
            var dmodel = new List<DistributionListModel>();
            foreach (var item in db.DistributionLists)
            {
                dmodel.Add(new DistributionListModel()
                {
                    DisId=item.DisId,
                    FoodType=item.FoodType,
                    Date=item.Date,
                    Place=item.Place

                });
            }
            return dmodel;
        }

    }
}