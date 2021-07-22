using System;
using StaffModelsLibrary;

namespace staff
{
    class StaffUpdate
    {
        public static void Update(dynamic staff)
        {
            switch((int)staff.StaffType)
            {
                case 1:
                    UpdateTeaching(staff);
                    break;

                case 2:
                    UpdateAdministrative(staff);   
                    break;
                case 3:
                    UpdateSupporting(staff);
                    break;    
                default:
                    break;    
            }
        }
        public static void UpdateTeaching(dynamic staff)
        {
            Console.WriteLine("select the atribute that you want to change\n1.name\n2.salary\n3.Teaching subject\n4.No. of teaching hours in a week");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    UpdateName(staff);
                    break;
                case 2:
                    UpdateSalary(staff);
                    break;
                case 3:
                    Console.WriteLine("Enter the new subject");
                    staff.Subject = Console.ReadLine();
                    break; 
                case 4:
                    Console.WriteLine("Enter the time");
                    staff.NoOfHrs = Convert.ToInt32(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("select a valid opyion");     
                    break;             
            }
        }

        public static void UpdateAdministrative(dynamic staff)
        {
            Console.WriteLine("select the atribute that you want to change\n1.name\n2.salary\n3.Administrative no\n4.Administrating department");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    UpdateName(staff);
                    break;
                case 2:
                    UpdateSalary(staff);
                    break;
                case 3:
                    Console.WriteLine("Enter the new Admin no.");
                    staff.AdminNo =  Console.ReadLine();
                    break; 
                case 4:
                    Console.WriteLine("Enter the new Department");
                    staff.AdmDprt = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("select a valid opyion");     
                    break;             
            }   
        }

        public static void UpdateSupporting(dynamic staff)
        {
            Console.WriteLine("select the atribute that you want to change\n1.name\n2.salary\n3.Name of superior\n4.Supporting field");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    UpdateName(staff);
                    break;
                case 2:
                    UpdateSalary(staff);
                    break;
                case 3:
                    Console.WriteLine("Enter the new superior");
                    staff.Superior = Console.ReadLine();
                    break; 
                case 4:
                    Console.WriteLine("Enter the new Supporting field");
                    staff.Field = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("select a valid opyion");     
                    break;             
            }
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