using System;
using StaffModelsLibrary;

namespace staff
{
    class StaffUpdate
    {
        public static Staff Update(dynamic staff)
        {
            Staff updatedStaff = null;
            switch((int)staff.StaffType)
            {
                case 1:
                    updatedStaff = UpdateTeaching(staff);
                    break;

                case 2:
                    updatedStaff = UpdateAdministrative(staff);   
                    break;
                case 3:
                    updatedStaff =  UpdateSupporting(staff);
                    break;    
                default:
                    break;    
            }
            return updatedStaff;
        }
        public static Teaching UpdateTeaching(Teaching s)
        {

            Teaching teacher = new Teaching(s.Name,s.EmpId,s.Salary,s.Subject, s.NoOfHrs);     
            Console.WriteLine("select the atribute that you want to change\n1.name\n2.salary\n3.Teaching subject\n4.No. of teaching hours in a week");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    UpdateName(teacher);
                    break;
                case 2:
                    UpdateSalary(teacher);
                    break;
                case 3:
                    Console.WriteLine("Enter the new subject");
                    teacher.Subject = Console.ReadLine();
                    break; 
                case 4:
                    Console.WriteLine("Enter the time");
                    teacher.NoOfHrs = Convert.ToInt32(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("select a valid opyion");     
                    break;             
            }
            StaffDisplay.Display(teacher);
            return teacher;
        }

        public static Administrative UpdateAdministrative(Administrative s)
        {

            Administrative administrative = new Administrative(s.Name, s.EmpId, s.Salary, s.AdminNo, s.AdmDprt);

            Console.WriteLine("select the atribute that you want to change\n1.name\n2.salary\n3.Administrative no\n4.Administrating department");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    UpdateName(administrative);
                    break;
                case 2:
                    UpdateSalary(administrative);
                    break;
                case 3:
                    Console.WriteLine("Enter the new Admin no.");
                    administrative.AdminNo =  Console.ReadLine();
                    break; 
                case 4:
                    Console.WriteLine("Enter the new Department");
                    administrative.AdmDprt = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("select a valid opyion");     
                    break;             
            }
            StaffDisplay.Display(administrative);
            return administrative;
        }

        public static Supporting UpdateSupporting(Supporting s)
        {
            Supporting supporting = new Supporting(s.Name, s.EmpId, s.Salary, s.Superior, s.Field);
            Console.WriteLine("select the atribute that you want to change\n1.name\n2.salary\n3.Name of superior\n4.Supporting field");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    UpdateName(supporting);
                    break;
                case 2:
                    UpdateSalary(supporting);
                    break;
                case 3:
                    Console.WriteLine("Enter the new superior");
                    supporting.Superior = Console.ReadLine();
                    break; 
                case 4:
                    Console.WriteLine("Enter the new Supporting field");
                    supporting.Field = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("select a valid opyion");     
                    break;             
            }
            StaffDisplay.Display(supporting);
            return supporting;
        }
        
        public static void UpdateName(dynamic staff)
        {
            Console.WriteLine("\n\nEnter the new name");
            staff.Name = Console.ReadLine();
        }
        public static void UpdateSalary(dynamic staff)
        {
            Console.WriteLine("Enter the new Salary");
            staff.Salary = Convert.ToInt32(Console.ReadLine());
        }
    }
}    