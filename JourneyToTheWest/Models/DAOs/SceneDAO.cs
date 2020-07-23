using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class SceneDAO
    {
        public List<Scene> GetAllScene()
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Scenes.Select(x => x)
                    .ToList();
            }
        }

        public Scene GetSceneById(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Scenes
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
            }
        }

        public List<Scene> GetSceneByName(string name)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Scenes
                        .Where(x => x.Name.Contains(name))
                        .ToList();
            }
        }
        public List<Scene> GetSceneByDate(DateTime date, DateTime dateTo)
        {
            
               //  DateTime tomorrow = date.AddDays(1);
           
                using (var entity = new JOURNEYTOTHEWESTEntities())
                {
                    return entity.Scenes.Where(x => x.StartTime <= date)
                        .Where(x => x.EndTime >= dateTo).ToList();

                }
           
           
        }

        public bool CheckSceneRecordedOrNot(int? sceneId)
        {

            using (var entity = new JOURNEYTOTHEWESTEntities())
            {
                return entity.Scenes.Where(x => x.EndTime >= DateTime.Now && x.Id == sceneId).FirstOrDefault() == null ? true : false; 
            }

        }

        #region Setter
        public bool AddNewScene(Scene scene)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    db.Scenes.Add(scene);
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


        public bool UpdateScene(Scene scene)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                if (scene != null)
                {
                    try
                    {
                        db.Scenes.Attach(scene);
                        db.Entry(scene).State = EntityState.Modified;
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
        public bool DeleteScene(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    new RecordHistoryDAO().DeleteBySceneId(id);
                    new EquipmentUsingHistoryDAO().DeleteBySceneId(id);
                    var scene = db.Scenes
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
                    if (scene == null) return false;

                    db.Scenes.Remove(scene);
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