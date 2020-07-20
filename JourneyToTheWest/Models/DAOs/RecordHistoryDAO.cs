using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class RecordHistoryDAO
    {
        public List<RecordHistory> GetAll()
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.RecordHistories.Select(x => x)
                    .ToList();
            }
        }

        public RecordHistory GetRecordById(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.RecordHistories
                        .Where(x => x.Id == id)
                        .Include(x => x.Cast)
                        .Include(x => x.Impersonation)
                        .SingleOrDefault();
            }
        }
        public List<RecordHistory> GetRecordBySceneId(int sceneId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.RecordHistories
                        .Where(x => x.SceneId == sceneId)
                        .Include(x => x.Cast)
                        .Include(x => x.Impersonation)
                        .ToList();
            }
        }
        public List<RecordHistory> GetRecordedByCastId(string username)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                List<RecordHistory> list = db.RecordHistories
                        .Where(x => x.AssignedCastId == username)
                        .ToList();

                foreach (var item in list)
                {
                    if (new SceneDAO().CheckSceneRecordedOrNot(item.SceneId))
                    {

                    }
                    else
                    {
                        list.Remove(item);
                    }
                }
                return list;
            }
        }
       
      public List<int?> GetListImpersonation(int sceneId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                List<int?> list = db.RecordHistories
                        .Where(x => x.SceneId == sceneId)
                        .Select(y => y.ImpersonationId)
                        .ToList();
                return list;
            }
        }


        public List<RecordHistory> GetListSceneByCast(string username)
        {
            
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                List<RecordHistory> list = db.RecordHistories
                        .Where(x => x.AssignedCastId == username)
                        .Include(y => y.Scene)
                        .Include(y => y.Cast)
                        .Include(y => y.Impersonation)
                        .ToList();
                return list;
            }
        }


        public bool AddNew(RecordHistory rc)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    db.RecordHistories.Add(rc);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                   // return false;
                    throw;
                }
                return false;
            }
        }
        public bool Delete(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    var rc = db.RecordHistories
                        .Where(x => x.Id == id)
                        .SingleOrDefault();

                    if (rc == null) return false;

                    db.RecordHistories.Remove(rc);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool DeleteBySceneId(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    List<RecordHistory> list = db.RecordHistories
                        .Where(x => x.SceneId == id)
                        .ToList();

                    if (list == null) return false;

                    foreach (RecordHistory rc in list)
                    {
                        db.RecordHistories.Remove(rc);
                        db.SaveChanges();
                    }
                    
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool DeleteByImperId(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    List<RecordHistory> list = db.RecordHistories
                        .Where(x => x.ImpersonationId == id)
                        .ToList();

                    if (list == null) return false;

                    foreach (RecordHistory rc in list)
                    {
                        db.RecordHistories.Remove(rc);
                        db.SaveChanges();
                    }

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