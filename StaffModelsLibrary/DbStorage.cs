using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace StaffModelsLibrary
{
    public class DbStorage:IStorage
    {
        List<Staff> staffList = new List<Staff>();
        public static int id = 0;

        public static string connString;
                                
        public DbStorage()
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            connString = config["ConnString"];
            Console.WriteLine(connString);
        }

        public void Add(Staff staff)
        {
            staffList.Add(staff);
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
        public void Bulkinsert( )
        {
            DataTable dt = DataTableCreator();

            foreach (Staff staff in staffList)
            {
                id++;
                DataRow dr = dt.NewRow();
                //dr["empid"] = 1;
                dr["id"] = id;
                dr["name"] = staff.Name;
                dr["salary"] = staff.Salary;
                switch (staff.StaffType)
                {
                    case TypesOfStaffs.Teaching:
                        Teaching teachingObj = (Teaching)staff;
                        dr["empType"] = (int)staff.StaffType;
                        dr["subject"] = teachingObj.Subject;
                        dr["tHours"] = teachingObj.NoOfHrs;
                        break;
                    case TypesOfStaffs.Administrative:
                        Administrative adminitrativeObj = (Administrative)staff;
                        dr["empType"] = (int)staff.StaffType;
                        dr["admNo"] = adminitrativeObj.AdminNo;
                        dr["admDprtmnt"] = adminitrativeObj.AdmDprt;
                        break;
                    case TypesOfStaffs.Supporting:
                        Supporting supportingObj = (Supporting)staff;
                        dr["empType"] = (int)staff.StaffType;
                        dr["superior"] = supportingObj.Superior;
                        dr["field"] = supportingObj.Field;

                        break;
                    default:
                        break;
                }
                dt.Rows.Add(dr);
            }

            using (SqlConnection con = new SqlConnection(connString))
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
            }
            staffList.Clear();
            dt.Clear();

        }

        private static DataTable DataTableCreator()
        {
            DataTable dt = new();
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
            return dt;
        }
    }
}


