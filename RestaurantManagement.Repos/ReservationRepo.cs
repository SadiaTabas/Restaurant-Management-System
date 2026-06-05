using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class ReservationRepo(RmDbContext context)
    {
        public RestaurantResult<List<Reservation>> GetAll()
        {
            var result = new RestaurantResult<List<Reservation>>();
            try
            {
                result.Data = context.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.RestaurantTable)
                    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public RestaurantResult<Reservation> GetById(int id)
        {
            var result = new RestaurantResult<Reservation>();
            try
            {
                result.Data = context.Reservations
                    .Include(r => r.Customer)
                    .Include(r => r.RestaurantTable)
                    .FirstOrDefault(r => r.ID == id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public RestaurantResult<Reservation> Save(Reservation model)
        {
            var result = new RestaurantResult<Reservation>();
            try
            {
                var objToSave = context.Reservations.Find(model.ID);

                if (objToSave == null)
                {
                    objToSave = new Reservation();
                    context.Reservations.Add(objToSave);

                    objToSave.Created_at = DateTime.Now;
                }

                objToSave.Customer_ID = model.Customer_ID;
                objToSave.Table_ID = model.Table_ID;
                objToSave.Reservation_time = model.Reservation_time;
                objToSave.Number_Of_people = model.Number_Of_people;
                objToSave.Status = model.Status;

                objToSave.Updated_at = DateTime.Now;
                objToSave.Updated_by = 1;

                context.SaveChanges();

                result.Data = objToSave;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Reservation> Delete(int id)
        {
            var result = new RestaurantResult<Reservation>();
            try
            {
                var obj = context.Reservations.Find(id);

                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Reservation not found";
                    return result;
                }

                context.Reservations.Remove(obj);
                context.SaveChanges();

                result.Data = obj;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}