using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class AdminDAO
    {
        public Admin CheckLogin(string username, string password)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Admins
                        .Where(x => x.Username == username && x.Password == password)
                        .SingleOrDefault();
            }
        }
        public Admin GetAdminById(string username)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Admins
                        .Where(x => x.Username == username)
                        .SingleOrDefault();
            }
        }
    }
}