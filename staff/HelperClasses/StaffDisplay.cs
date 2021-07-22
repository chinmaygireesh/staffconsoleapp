using System;
using System.Collections.Generic;
using StaffModelsLibrary;
namespace staff
{
    class StaffDisplay
    {
        public static void Display(dynamic staff)
        {
            switch((int)staff.StaffType)
            {
                case 1:
                    TableDisplay.PrintLine();
                    TableDisplay.PrintRow("Name", "EmpId","Staff Type","Salary", "Subject","Teching hrs");
                    TableDisplay.PrintLine();
                    TableDisplay.PrintRow(staff.Name, $"{staff.EmpId}","Teching", $"{staff.Salary}", staff.Subject,$"{staff.NoOfHrs}");
                    TableDisplay.PrintLine();
                    Console.WriteLine();
                    break;

                case 2:
                    TableDisplay.PrintLine();
                    TableDisplay.PrintRow("Name", "EmpId","Staff Type","Salary", "Admin. No.","Adminisrating Dprtmnt.");
                    TableDisplay.PrintLine();
                    TableDisplay.PrintRow(staff.Name, $"{staff.EmpId}","Administrative", $"{staff.Salary}", staff.AdminNo,staff.AdmDprt);
                    TableDisplay.PrintLine();
                    Console.WriteLine();    
                    break;
                case 3:
                    TableDisplay.PrintLine();
                    TableDisplay.PrintRow("Name", "EmpId","Staff Type","Salary", "Superior","Supporting field");
                    TableDisplay.PrintLine();
                    TableDisplay.PrintRow(staff.Name, $"{staff.EmpId}","Supporting", $"{staff.Salary}", staff.Superior,staff.Field);
                    TableDisplay.PrintLine();
                    Console.WriteLine();
                    break;    
                default:
                    break;    
            }

        }
    }
}    