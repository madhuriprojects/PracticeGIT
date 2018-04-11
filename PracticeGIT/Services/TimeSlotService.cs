using PracticeGIT.Controllers;
using PracticeGIT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PracticeGIT.Services
{
    public class TimeSlotService
    {
        
        public IReadOnlyList<TimeSlot> GetAllSlots()
        {
            var cmd = new SqlCommand("GetMasterSlots");
            return DatabaseHelper.GetDataAsList<TimeSlot>(cmd, (IDataRecord dataRecord) =>
            {
                var slot = new TimeSlot();
                slot.SlotId = dataRecord.GetInt32(dataRecord.GetOrdinal("id"));
                slot.StartTime = TimeSpan.Parse(dataRecord["StartTime"].ToString());
                slot.EndTime = TimeSpan.Parse(dataRecord["EndTime"].ToString());
                return slot;
            });
        }

        internal IReadOnlyList<TimeSlotPreference> GetUserSlots(int userId)
        {
            var cmd = new SqlCommand("GetUserSlots");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            return DatabaseHelper.GetDataAsList<TimeSlotPreference>(cmd, (IDataRecord dataRecord) =>
            {
                var slot = new TimeSlotPreference();
                slot.SlotId = dataRecord.GetInt32(dataRecord.GetOrdinal("SlotId"));
                slot.SlotDate = dataRecord.GetDateTime(dataRecord.GetOrdinal("Date"));
                slot.Created = dataRecord.GetDateTime(dataRecord.GetOrdinal("CreatedDateTime"));
                return slot;
            });
        }

        internal void AddSlot(int userId, TimeSlotPreference preference)
        {
            var cmd = new SqlCommand("AddSlot");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@SlotId", preference.SlotId);
            cmd.Parameters.AddWithValue("@Date", preference.SlotDate);
            DatabaseHelper.ExecuteNonQuery(cmd);
        }

        internal object GetAvailableSlots(int userId)
        {
            var cmd = new SqlCommand("GetAvailableSlots");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            return DatabaseHelper.GetDataAsList<TimeSlotPreference>(cmd, (IDataRecord dataRecord) =>
            {
                var slot = new TimeSlotPreference();
                slot.SlotId = dataRecord.GetInt32(dataRecord.GetOrdinal("SlotId"));
                slot.SlotDate = dataRecord.GetDateTime(dataRecord.GetOrdinal("Date"));
                return slot;
            });
        }
    }
}