using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                    .Include(e => e.Equipment)
                    .Include(e => e.Scene)
                    .ToList();
            }
        }
        public List<EquipmentUsingHistory> GetByDate(DateTime dateFrom, DateTime dateTo)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                List<EquipmentUsingHistory> resultList = new List<EquipmentUsingHistory>();
                List<Scene> list = new SceneDAO().GetSceneByDate(dateFrom, dateTo);
                if(list == null)
                {
                    return null;
                }
                else
                {
                    var tmpList = new List<EquipmentUsingHistory>(); ;
                    foreach (var item in list)
                    {
                        tmpList = this.GetEquipmentBySceneId(item.Id);
                        tmpList.ForEach(x => resultList.Add(x));
                    }
                    return resultList;
                }
              
            }
        }

        public List<EquipmentUsingHistory> GetEquipmentBySceneId(int sceneId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.EquipmentUsingHistories
                        .Where(x => x.SceneId == sceneId)
                        .Include(x => x.Scene)
                        .Include(x => x.Equipment).ToList();
                       
            }
        }
        public EquipmentUsingHistory GetEquipment(int sceneId, int eId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.EquipmentUsingHistories
                        .Where(x => x.SceneId == sceneId && x.EquipmentId == eId)
                        .Include(x => x.Scene)
                        .Include(x => x.Equipment).SingleOrDefault();
                       
            }
        }
        public bool AddNew(EquipmentUsingHistory rc)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    UpdateQuantityEquipment(rc, false);
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
        public bool Update(EquipmentUsingHistory rc)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                if (rc != null)
                {
                    try
                    {
                        UpdateQuantityEquipmentForEdit(rc);
                        db.EquipmentUsingHistories.Attach(rc);
                        db.Entry(rc).State = EntityState.Modified;
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

                    UpdateQuantityEquipment(rc, true);       
                    
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
                        UpdateQuantityEquipment(r, true);
                        db.EquipmentUsingHistories.Remove(r);
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
                        UpdateQuantityEquipment(r, true);
                        db.EquipmentUsingHistories.Remove(r);
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
        public bool UpdateQuantityEquipment(EquipmentUsingHistory eq, bool isPush)
        {   
            
            try
            {
                Equipment e = new EquipmentDAO().GetEquipmentById(eq.EquipmentId);
                if(e != null)
                {
                    if (isPush)
                    {
                        e.Quantity = e.Quantity + eq.Quantity;

                    }
                    else
                    {
                        e.Quantity = e.Quantity - eq.Quantity;

                    }
                    new EquipmentDAO().UpdateEquipment(e);
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }
        public bool UpdateQuantityEquipmentForEdit(EquipmentUsingHistory eq)
        {

            try
            {
                Equipment e = new EquipmentDAO().GetEquipmentById(eq.EquipmentId);
                EquipmentUsingHistory current = new EquipmentUsingHistoryDAO().GetEquipment(eq.SceneId, eq.EquipmentId);

                if (eq.Quantity > current.Quantity)
                {
                    e.Quantity = e.Quantity - (eq.Quantity - current.Quantity);

                }
                else
                {
                    e.Quantity = e.Quantity + (current.Quantity - eq.Quantity);

                }

                new EquipmentDAO().UpdateEquipment(e);
            }

            catch (Exception)
            {

                throw;
            }
            return true;
        }
    }
}