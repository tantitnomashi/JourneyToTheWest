using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneyToTheWest.Models.DAOs
{
    public class EquipmentDAO
    {
        #region Getters

        public List<Equipment> GetAllEquipment()
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Equipments.Select(x => x)
                    .ToList();
            }
        }

        public List<Equipment> GetAvailbleEquipment()
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Equipments
                    .Where(x => x.Quantity > 0)
                    .ToList();
            }
        } 
        public List<Equipment> GetAvailbleEquipmentById(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Equipments
                    .Where(x => x.Id == id && x.Quantity > 0)
                    .ToList();
            }
        }
        public bool isAvailbleQuantity(int id, int quantity)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Equipments
                    .Where(x => x.Id == id && x.Quantity >= quantity).SingleOrDefault() != null ? true :false;
                   
            }
        }
      
        public Equipment GetEquipmentById(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Equipments
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
            }
        }

        public List<Equipment> GetEquipmentByName(string name)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                return db.Equipments
                        .Where(x => x.Name.Contains(name))
                        .ToList();
            }
        }


        #endregion


        #region Setter
         public bool AddNewEquipment(Equipment equipment)
        {
            using( var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    db.Equipments.Add(equipment);
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
        
        public bool UpdateEquipmentQuantityById(int id, int quantity)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                Equipment equipment = GetEquipmentById(id);
                    equipment.Quantity = equipment.Quantity - quantity;

                    if (equipment != null)
                    {
                        try
                        {
                            db.Equipments.Attach(equipment);
                            db.Entry(equipment).State = EntityState.Modified;
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
        public bool UpdateEquipment(Equipment equipment)
        {
            using (var db = new JOURNEYTOTHEWESTEntities())
            {           
                if (equipment != null)
                {
                    try
                    {
                        db.Equipments.Attach(equipment);
                        db.Entry(equipment).State = EntityState.Modified;
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
        public bool DeleteEquiptment(int id)
        {

            using (var db = new JOURNEYTOTHEWESTEntities())
            {
                try
                {
                    var equipment = db.Equipments
                        .Where(x => x.Id == id)
                        .SingleOrDefault();

                    if (equipment == null) return false;

                    db.Equipments.Remove(equipment);
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