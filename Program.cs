using System.Collections.Generic;
using System.IO;
using StaffMembers;
using StaffUtility;
using System;

namespace PayRoll
{
    class Program
    {
        static void Main(string[] args)
        {

            FileReader fr = new FileReader();
            List<Staff> myStaff = fr.ReadFile();
            int month = 0, year = 0;

            while(year == 0) {
                Console.Write("\nPlease enter the year: ");
                try {
                    year = Int16.Parse(Console.ReadLine());
                } catch (FormatException) {
                    Console.WriteLine("\nPlease enter a valid number");
                }
            }

            while (month == 0) {
                Console.Write("\nPlease enter the month: ");
                try {
                    month = Int16.Parse(Console.ReadLine());
                    if(month < 1 || month > 12) {
                        month = 0;
                        Console.WriteLine("Please enter a number between 1-12");
                    }
                } catch (FormatException) {
                    Console.WriteLine("\nPlease enter a valid number");
                }
            }
            for(int i = 0; i < myStaff.Count; i++) {
                try {
                    Console.WriteLine("Please enter the hours worked for " + myStaff[i].NameOfStaff + ":");
                    int input = Int16.Parse(Console.ReadLine());
                    myStaff[i].HoursWorked = input;
                    myStaff[i].CalculatePay();
                    Console.WriteLine("\n"+myStaff[i].ToString());
                }catch(Exception) {
                    Console.WriteLine("Please input a valid integer");
                    i--;
                }
            }

            PaySlip slip = new PaySlip(month, year);
            slip.GeneratePaySlip(myStaff);
            slip.GenerateSummary(myStaff);
            Console.Read();


        }
    }
}
