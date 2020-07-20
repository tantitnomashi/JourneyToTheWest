using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class CastDAO
    {   

        public Cast CheckLogin(string username, string password)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Casts
                        .Where(x => x.Username == username && x.Password == password && x.Role != 3)
                        .SingleOrDefault();
            }
        }
        public List<Cast> GetAllCast()
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Casts.Where(x => x.Role != 3).Select(x => x)
                    .ToList();
            }
        }

        public Cast GetCastById(string username)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Casts
                        .Where(x => x.Username == username)
                        .SingleOrDefault();
            }
        }

        public List<Cast> GetCastByName(string name)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Casts
                        .Where(x => x.FullName.Contains(name))
                        .ToList();
            }
        }


        #region Setter
        public bool AddNewCast(Cast cast)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    db.Casts.Add(cast);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }

            }
        }

      
        public bool UpdateCast(Cast cast)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                if (cast != null)
                {
                    try
                    {
                        db.Casts.Attach(cast);
                        db.Entry(cast).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw;
                    }
                }
                return true;

            }
        }
        public bool DeleteCast(string username)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    Cast cast = db.Casts
                        .Where(x => x.Username == username)
                        .SingleOrDefault();
                    if (cast == null) return false;
                    // db.Casts.Remove(cast);
                    cast.Role = 3;
                    db.Casts.Attach(cast);
                    db.Entry(cast).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        #endregion
    }


}
