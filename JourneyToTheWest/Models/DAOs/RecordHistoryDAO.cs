using System;
using System.Collections.Generic;
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
                        .SingleOrDefault();
            }
        }
        public List<RecordHistory> GetRecordBySceneId(int sceneId)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.RecordHistories
                        .Where(x => x.Id == sceneId)
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
        public List<RecordHistory> GetFutureRecordByCastId(string username)
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
                        list.Remove(item);
                    }

                }
                return list;
            }
        }

    }
}