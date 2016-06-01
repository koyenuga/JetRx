using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetRx.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace JetRx.Common.Data.SqlServer
{
    public class DeviceDataAccess
    {
        public device RegisterDevice(device _device)
        {
            using (SqlCommand cmd = new SqlCommand("RegisterDevice", new SqlConnection(ConfigurationManager.ConnectionStrings["JetRxDbContext"].ConnectionString)))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeviceType", _device.devicetype);
                cmd.Parameters.AddWithValue("@PhoneNumber", _device.phonenumber);
                cmd.Parameters.AddWithValue("@DeviceIdentifier", _device.deviceidentifier);
                cmd.Parameters.AddWithValue("@DeviceName", _device.devicename);
                cmd.Connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return new device(reader["AppDeviceKey"].ToString())
                    {
                        id = (int)reader["Id"],
                        deviceidentifier = reader["DeviceIdentifier"].ToString(),
                        devicename = reader["DeviceName"].ToString(),
                        devicetype = reader["DeviceType"].ToString(),
                        phonenumber = reader["PhoneNumber"].ToString()
                    };
                }

            }

        }

    }
}
