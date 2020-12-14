using StaffMembers;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace StaffUtility {
    class FileReader {
        public List<Staff> ReadFile() {
            List<Staff> myStaff = new List<Staff>();
            string path = "staff.txt";
            string[] separator = { ", " };
            if (File.Exists(path)) {
                try {
                    StreamReader reader = new StreamReader(path);
                    using (reader) {
                        while (!reader.EndOfStream) {
                            string[] staff = reader.ReadLine().Split(separator, StringSplitOptions.None);
                            if (staff[1].Equals("Admin"))
                                myStaff.Add(new Admin(staff[0]));
                            else if (staff[1].Equals("Manager"))
                                myStaff.Add(new Manager(staff[0]));
                        }
                    }
                } catch (FileNotFoundException) {
                    Console.WriteLine("File not found");
                }
            }
            return myStaff;
        }
    }

    class PaySlip {
        private int month, year;
        private enum MonthsOfYear {
            JAN = 1,
            FEB = 2,
            MAR = 3,
            APR = 4,
            MAY = 5,
            JUN = 6,
            JUL = 7,
            AUG = 8,
            SEP = 9,
            OCT = 10,
            NOV = 11,
            DEC = 12
        };

        public PaySlip(int payMonth, int payYear) {
            this.month = payMonth;
            this.year = payYear;
        }

        public void GeneratePaySlip(List<Staff> myStaff) {

            string path;
            foreach (Staff staff in myStaff){
                path = staff.NameOfStaff + ".txt";

                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                writer.WriteLine("==========================================");
                writer.WriteLine("Name of Staff: " + staff.NameOfStaff);
                writer.WriteLine("Hours Worked: " + staff.HoursWorked);
                writer.WriteLine("");
                writer.WriteLine("Basic Pay: {0:C}", staff.BasicPay);
                if(staff.GetType() == typeof(Manager)) {
                    writer.WriteLine("Allowance: {0:C}", ((Manager) staff).Allowance);
                }else if(staff.GetType() == typeof(Admin)) {
                    writer.WriteLine("Overtime: " + ((Admin)staff).Overtime);
                }
                writer.WriteLine("");
                writer.WriteLine("==========================================");
                writer.WriteLine("TotalPay: {0:C}", staff.TotalPay);
                writer.WriteLine("==========================================");

                writer.Close();
            }

        }

        public void GenerateSummary(List<Staff> myStaff) {

            var under10 = from staff in myStaff
                          where staff.HoursWorked < 10
                          orderby staff.NameOfStaff ascending
                          select new { staff.NameOfStaff, staff.HoursWorked };

            string path = "summary.txt";
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("Staff with less than 10 working hours");
            writer.WriteLine("");
            foreach(var staff in under10) {
                writer.WriteLine("Name of Staff: " + staff.NameOfStaff + ", Hours Worked: " + staff.HoursWorked);
            }
            writer.Close();

        }

        public override string ToString() {
            return "PaySlip{month="+ this.month +",year="+ this.year + "}";
        }


    }

}