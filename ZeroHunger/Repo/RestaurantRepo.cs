using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.DB;
using ZeroHunger.Models;

namespace ZeroHunger.Repo
{
    public class RestaurantRepo
    {
        public static void Create(RestaurantModel dbr)
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");

            var restaurant = new Restaurant();
            restaurant.ResName = dbr.ResName;
            restaurant.ResLocation = dbr.ResLocation;
            restaurant.ResUname = dbr.ResUname + r[2];
            restaurant.ResPass = r;

            var db = new Entities();
            db.Restaurants.Add(restaurant);
            db.SaveChanges();

        }

        public static List<RestaurantModel> Get()
        {
            var db = new Entities();
            var restaurants = new List<RestaurantModel>();
            foreach (var item in db.Restaurants)
            {
                restaurants.Add(new RestaurantModel()
                {
                    Rid = item.Rid,
                    ResName = item.ResName,
                    ResLocation = item.ResLocation,
                    ResUname = item.ResUname,

                });
            }
            return restaurants;
        }


        public static RestaurantModel Get(int id)
        {
            var db = new Entities();
            var restaurant = new RestaurantModel();
            var dbr = db.Restaurants.Find(id);
            restaurant.Rid = dbr.Rid;
            restaurant.ResName = dbr.ResName;
            restaurant.ResLocation = dbr.ResLocation;
            restaurant.ResUname = dbr.ResUname;
            restaurant.ResPass = dbr.ResPass;
            return restaurant;
        }

        public static RestaurantModel Get(string uname,string pass)
        {
            var db = new Entities();
            var restaurant = new RestaurantModel();
            var dbr = (from p in db.Restaurants
                              where p.ResUname == uname && p.ResPass == pass
                          select p).SingleOrDefault();


            if (dbr != null)
            {
                restaurant.Rid = dbr.Rid;
                restaurant.ResName = dbr.ResName;
                restaurant.ResLocation = dbr.ResLocation;
                restaurant.ResUname = dbr.ResUname;
                restaurant.ResPass = dbr.ResPass;
            }

            return restaurant;

        }

        public static void Edit(RestaurantModel dbr)
        {

            var db = new Entities();
            var existingrestaurant = db.Restaurants.Where(temp => temp.Rid == dbr.Rid).FirstOrDefault();
            existingrestaurant.ResName = dbr.ResName;
            existingrestaurant.ResLocation = dbr.ResLocation;
            existingrestaurant.ResUname = dbr.ResUname;
            existingrestaurant.ResPass = dbr.ResPass;
            db.SaveChanges();

        }

        public static void Delete(RestaurantModel dbr)
        {
            var db = new Entities();

            int count = db.CollectionRequests.Where(temp => temp.CrId == dbr.Rid ).ToList().Count;
            if (count == 0)
            {
                var existingrestaurant = db.Restaurants.Where(temp => temp.Rid == dbr.Rid).FirstOrDefault();
                db.Restaurants.Remove(existingrestaurant);
                db.SaveChanges();
            }

            
        }

    }
}