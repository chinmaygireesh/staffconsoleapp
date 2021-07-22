using System;
using StaffModelsLibrary;
namespace staff
{
    class StaffRegister
    {
        public static Staff Register(int userChoice,int empId)
        {
            Staff staff = null;
            string name;
            int salary;
            Console.WriteLine("\n\nEnter ur name");
            name = Console.ReadLine();
            Console.WriteLine("Enter ur Salary");
            salary = Convert.ToInt32(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    Console.WriteLine("Enter the subject that u teach");
                    string subject = Console.ReadLine();
                    Console.WriteLine("Enter the no. of teaching Hours that u have in a week");
                    int noOfHrs = Convert.ToInt32(Console.ReadLine());
                    staff = new Teaching(name,empId,salary,subject,noOfHrs);
                    break;
                case 2:
                    Console.WriteLine("Enter the admin no.");
                    string admDprt = Console.ReadLine();
                    Console.WriteLine("Enter the adminstrating Department");
                    string admNo = Console.ReadLine();
                    staff = new Administrative(name,empId,salary,admNo,admDprt);
                    break;
                case 3:
                    Console.WriteLine("Enter the Name of your superior");
                    string superior = Console.ReadLine();
                    Console.WriteLine("Enter the Supporting field");
                    string field = Console.ReadLine();
                    staff = new Supporting(name,empId,salary,superior,field);
                    break;            
            }    
            return staff;

        }
    }
}    