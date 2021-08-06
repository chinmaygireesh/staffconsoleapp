using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace StaffModelsLibrary
{
    public class DbStorage:IStorage
    {
        DataTable dt = new DataTable();
        

        public static string connString;
                                
        public DbStorage()
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            connString = config["ConnString"];
            Console.WriteLine(connString);
           
            dt.Clear();
            dt.Columns.Add("empid", typeof(int));
            dt.Columns["empid"].DefaultValue = DBNull.Value;
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("salary", typeof(int));
            dt.Columns.Add("empType", typeof(int));
            dt.Columns.Add("subject", typeof(string));
            dt.Columns["subject"].DefaultValue = DBNull.Value;
            dt.Columns.Add("tHours", typeof(int));
            dt.Columns["tHours"].DefaultValue = DBNull.Value;
            dt.Columns.Add("admNo", typeof(string));
            dt.Columns["admNo"].DefaultValue = DBNull.Value;
            dt.Columns.Add("admDprtmnt", typeof(string));
            dt.Columns["admDprtmnt"].DefaultValue = DBNull.Value;
            dt.Columns.Add("superior", typeof(string));
            dt.Columns["superior"].DefaultValue = DBNull.Value;
            dt.Columns.Add("field", typeof(string));
            dt.Columns["field"].DefaultValue = DBNull.Value;

            DataRow dr = dt.NewRow();
            //dr["empid"] = 1;
            dr["id"] = 2;
            dr["name"] = "Tesla";
            dr["salary"] = 445454;
            dr["empType"] = 1;
            dr["subject"] = "dsdsd";
            dr["tHours"] = 41;
            dr["admNo"] = "54545";
            dr["admDprtmnt"] = "544";
            dr["superior"] = "dsds";
            dr["field"] = "dsdsdsd";
            dt.Rows.Add(dr);

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine();
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    Console.Write(row[x].ToString() + " ");
                }
            }

         /*   using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("Proc_bulkInsert"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@StaffDetails", dt);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }*/


        }

        public void Add(Staff staff)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();
                SqlCommand command = new  SqlCommand("Proc_InsertAstaff", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name",staff.Name);
                command.Parameters.AddWithValue("@salary",staff.Salary);

                switch (staff.StaffType)
                {
                    case TypesOfStaffs.Teaching:
                        Teaching teachingObj = (Teaching)staff;
                        command.Parameters.AddWithValue("@empType", (int)staff.StaffType);
                        command.Parameters.AddWithValue("@subject ", teachingObj.Subject);
                        command.Parameters.AddWithValue("@tHours", teachingObj.NoOfHrs);
                        break;
                    case TypesOfStaffs.Administrative:
                        Administrative adminitrativeObj = (Administrative)staff;
                        command.Parameters.AddWithValue("@empType", (int)staff.StaffType);
                        command.Parameters.AddWithValue("@admNo", adminitrativeObj.AdminNo);
                        command.Parameters.AddWithValue("@admDprtmnt",adminitrativeObj.AdmDprt);
                        break;
                    case TypesOfStaffs.Supporting:
                        Supporting supportingObj = (Supporting)staff;
                        command.Parameters.AddWithValue("@empType", (int)staff.StaffType);
                        command.Parameters.AddWithValue("@superior", supportingObj.Superior);
                        command.Parameters.AddWithValue("@field ", supportingObj.Field);
                        break;
                    default:
                        break;

                }
                int i = command.ExecuteNonQuery();
                connection.Close();

                
            }
        }

        public Staff GetStaff(int empId)
        {
            Staff staff = null;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connString;
                connection.Open(); 
                SqlCommand command = new SqlCommand($"Proc_GetstaffWithEmpId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empId", empId);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    switch((int)dataReader["EmpType"])//switch on emp type
                    {
                        case 1:
                            staff = CreateTeachingObject(dataReader);
                            break;
                        case 2:
                            staff = CreatAdministarativeObject(dataReader);
                            break;
                        case 3:
                            staff = CreateSupportingObject(dataReader);
                            break;
                        default:
                            Console.WriteLine("Error in getting emp type");
                            break;
                    }
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
                return staff;
            }
        }

        public void Delete(int empId)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();
                SqlCommand command = new SqlCommand("Proc_DeleteAStaffWithEmpId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empId", empId);
                int i = command.ExecuteNonQuery();
                connection.Close();
               
            }
        }
        public void Upadate(Staff staff)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();
                SqlCommand command = new SqlCommand("Proc_UpdateAstaff", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empId", staff.EmpId);
                command.Parameters.AddWithValue("@Name", staff.Name);
                command.Parameters.AddWithValue("@salary", staff.Salary);

                switch (staff.StaffType)
                {
                    case TypesOfStaffs.Teaching:
                        Teaching teachingObj = (Teaching)staff;
                        command.Parameters.AddWithValue("@empType", (int)staff.StaffType);
                        command.Parameters.AddWithValue("@subject ", teachingObj.Subject);
                        command.Parameters.AddWithValue("@tHours", teachingObj.NoOfHrs);
                        break;
                    case TypesOfStaffs.Administrative:
                        Administrative adminitrativeObj = (Administrative)staff;
                        command.Parameters.AddWithValue("@empType", (int)staff.StaffType);
                        command.Parameters.AddWithValue("@admNo", adminitrativeObj.AdminNo);
                        command.Parameters.AddWithValue("@admDprtmnt", adminitrativeObj.AdmDprt);
                        break;
                    case TypesOfStaffs.Supporting:
                        Supporting supportingObj = (Supporting)staff;
                        command.Parameters.AddWithValue("@empType", (int)staff.StaffType);
                        command.Parameters.AddWithValue("@superior", supportingObj.Superior);
                        command.Parameters.AddWithValue("@field ", supportingObj.Field);
                        break;
                    default:
                        break;

                }
                int i = command.ExecuteNonQuery();
                connection.Close();

               
            }
        }

        public List<Staff> GetAllStaffs()
        {
            List<Staff> staffList = new List<Staff>();
            Staff staff = null;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();
                SqlCommand command;

                command = new SqlCommand("Proc_GetALLStaffs", connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    switch ((int)dataReader["EmpType"])//switch on emp type
                    {
                        case 1:
                            staff = CreateTeachingObject(dataReader);
                            break;
                        case 2:
                            staff = CreatAdministarativeObject(dataReader);
                            break;
                        case 3:
                            staff = CreateSupportingObject(dataReader);
                            break;
                        default:
                            Console.WriteLine("Error in getting emp type");
                            break;
                    }
                    staffList.Add(staff);
                }

                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            return staffList;
        }
        public static void StaffDetailsAdder(SqlDataReader dataReader, Staff staff)
        {
            staff.Name = (string)dataReader["Name"];
            staff.EmpId = (int)dataReader["EmpId"];
            staff.Salary = (int)dataReader["Salary"];
        }
        public static Staff CreateSupportingObject(SqlDataReader dataReader)
        {
            Supporting supporting = new Supporting();
            supporting.StaffType = TypesOfStaffs.Supporting;
            StaffDetailsAdder(dataReader, supporting);
            supporting.Superior = (string)dataReader["Superior"];
            supporting.Field = (string)dataReader["Supporting_Field"];
            return supporting;
        }

        public static Staff CreatAdministarativeObject(SqlDataReader dataReader)
        {
            Administrative administrative = new Administrative();
            administrative.StaffType = TypesOfStaffs.Administrative;
            StaffDetailsAdder(dataReader, administrative);
            administrative.AdminNo = (string)dataReader["Admin_NO"];
            administrative.AdmDprt = (string)dataReader["Admn_Dprtmnt"];
            return administrative;
        }

        public static Staff CreateTeachingObject(SqlDataReader dataReader)
        {
            Teaching teaching = new Teaching();
            teaching.StaffType = TypesOfStaffs.Teaching;
            StaffDetailsAdder(dataReader, teaching);
            teaching.Subject = (string)dataReader["Subject"];
            teaching.NoOfHrs = (int)dataReader["Teaching_Hours"];
            return teaching;
        }
    }
}

