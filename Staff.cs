using System;
namespace StaffMembers{
    abstract class Staff {

        private float hourlyRate;
        private int hWorked;

        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }
        public int HoursWorked { get { return hWorked; }
            set {
                hWorked = value > 0 ? value : 0;
            }
        }

        public Staff(string name, float rate) {

            this.NameOfStaff = name;
            this.hourlyRate = rate;
        }

        public virtual void CalculatePay() {
            Console.WriteLine("Calculating Pay...");
            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        public override string ToString() {
            return "NameOfStaff = " + NameOfStaff + "\nHoursWorked = " + HoursWorked + "\nBasic Pay = " + BasicPay + "\n\nTotalPay = " + TotalPay;
        }

    }

    class Manager : Staff {

        private const float managerHourlyRate = 50F;
        public int Allowance { get; set; }


        public Manager(string name) : base(name, managerHourlyRate) {

        }

        public override void CalculatePay() {
            Allowance = 1000;
            if(HoursWorked > 160)
                TotalPay += Allowance;
            base.CalculatePay();

        }

        public override string ToString() {
            return "NameOfStaff = " + NameOfStaff + "\nmanagerHourlyRate = " + managerHourlyRate +"\nHoursWorked = " + HoursWorked + "\nBasic Pay = " + BasicPay + "\n\nTotalPay = " + TotalPay;

        }

    }

    class Admin : Staff {
        private const float overtimeRate = 15.5F;
        private const float adminHourlyRate = 30F;

        public float Overtime { get; private set; }

        public Admin(string name) : base(name, adminHourlyRate) {

        }

        public override void CalculatePay() {
            if(HoursWorked > 160) {
                Overtime = overtimeRate * (HoursWorked - 160);
                TotalPay += Overtime;
            }
            base.CalculatePay();

        }

        public override string ToString() {
            return "NameOfStaff = " + NameOfStaff + "\nadminHourlyRate = " + adminHourlyRate + "\nHoursWorked = " + HoursWorked + "\nBasic Pay = " + BasicPay + "\nOvertime = " + Overtime +"\n\nTotalPay = " + TotalPay;
        }

    }
}