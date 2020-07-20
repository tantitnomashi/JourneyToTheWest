using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class EquipmentUsingHistoryDAO
    {
        public List<EquipmentUsingHistory> GetAll()
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.EquipmentUsingHistories.Select(x => x)
                    .ToList();
            }
        }

       /* public EquipmentUsingHistory GetRecordById(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.EquipmentUsingHistories
                        .Where(x => x.Id == id)
                        .Include(x => x.Cast)
                        .Include(x => x.Impersonation)
                        .SingleOrDefault();
            }
        }*/

        public List<EquipmentUsingHistory> GetRecordBySceneId(int sceneId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.EquipmentUsingHistories
                        .Where(x => x.SceneId == sceneId)
                        .Include(x => x.Scene)
                        .Include(x => x.Equipment).ToList();
                       
            }
        }
        public bool AddNew(EquipmentUsingHistory rc)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    db.EquipmentUsingHistories.Add(rc);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                     return false;
                    throw;
                }
            }
        }
        public bool Delete(int sceneId, int equipmentId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    var rc = db.EquipmentUsingHistories
                        .Where(x => x.SceneId == sceneId && x.EquipmentId == equipmentId)
                        .SingleOrDefault();

                    if (rc == null) return false;

                    db.EquipmentUsingHistories.Remove(rc);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool DeleteBySceneId(int sceneId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {

                    List<EquipmentUsingHistory> rc = db.EquipmentUsingHistories
                        .Where(x => x.SceneId == sceneId)
                        .ToList();

                    if (rc == null) return false;

                    foreach (EquipmentUsingHistory r in rc)
                    {
                        db.EquipmentUsingHistories.Remove(r);
                        db.SaveChanges();
                    }

                    //db.EquipmentUsingHistories.Remove(rc);
                   // db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool DeleteByEquipmentId(int eId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {

                    List<EquipmentUsingHistory> rc = db.EquipmentUsingHistories
                        .Where(x => x.EquipmentId == eId)
                        .ToList();

                    if (rc == null) return false;

                    foreach (EquipmentUsingHistory r in rc)
                    {
                        db.EquipmentUsingHistories.Remove(r);
                        db.SaveChanges();
                    }

                    //db.EquipmentUsingHistories.Remove(rc);
                    // db.SaveChanges();

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