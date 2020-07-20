using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class ImpersonationDAO
    {
        public List<Impersonation> GetAllImpersonation()
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Impersonations.Include(x => x.Cast).ToList();
            }
        }

        public Impersonation GetImpersonationById(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Impersonations
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
            }
        }

        public List<Impersonation> GetImpersonationByName(string name)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Impersonations
                        .Where(x => x.Name.Contains(name))
                        .ToList();
            }
        }

        public bool AddNewImpersonation(Impersonation impersonation)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    db.Impersonations.Add(impersonation);
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


        public bool UpdateImpersonation(Impersonation impersonation)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                if (impersonation != null)
                {
                    try
                    {
                        db.Impersonations.Attach(impersonation);
                        db.Entry(impersonation).State = EntityState.Modified;
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
        public bool DeleteImpersonation(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    new RecordHistoryDAO().DeleteByImperId(id);
                    var impersonation = db.Impersonations
                        .Where(x => x.Id == id)
                        .SingleOrDefault();

                    if (impersonation == null) return false;

                    db.Impersonations.Remove(impersonation);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


    }
}