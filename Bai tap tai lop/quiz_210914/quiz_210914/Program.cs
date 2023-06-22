using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz_210914
{
    class Student
    {
        private static Random random = new Random();
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public float GPA { get; set; }
        public string Telephone { get; set; }
        public Student()
        {
            string[] firstnames = { "Nguyen", "Tran", "Le", "Vu", "Do", "Ly", "Huynh", "Mac", "Doan" };
            string[] middlenames = { "Duc", "Tan", "Hoang", "Minh", "Van", "Trong", "Kim" };
            string[] lastnames = { "Quang", "Tien", "Tho", "Kha", "Doan", "Huy", "Tri", "Thinh", "Tham" };
            string[] streets = { "Ham Nghi", "Vo Thi Sau", "To Vinh Dien", "Phan Van Hon", "Mac Cuu" };
            string[] districts = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                "11", "12", "Binh Tan", "Tan Phu", "Phu Nhuan"};

            ID = random.Next(18, 21).ToString();
            for (int i = 0; i < 6; ++i)
                ID += random.Next(10).ToString();

            FullName = firstnames[random.Next(firstnames.Length)] + " " + middlenames[random.Next(middlenames.Length)]
                + " " + lastnames[random.Next(lastnames.Length)];

            Address = random.Next(100).ToString() + " ";
            Address += streets[random.Next(streets.Length)] + ", ";
            Address += "Ward " + random.Next(10).ToString() + ", ";
            Address += "District " + districts[random.Next(districts.Length)];

            GPA = random.Next(101) * 1.0f / 10;

            Telephone = "0";
            for(int i = 0; i < 9; ++i)
                Telephone += random.Next(10).ToString();

        }
        public override string ToString()
        {
            return $"ID: {ID}. Fullname: {FullName}. Address: {Address}. GPA: {GPA}. Tel: {Telephone}";
        }
    }
    class Program
    {

        static void Main()
        {
            const int n = 10;
            var students = new List<Student>();
            for (int i = 0; i < n; ++i)
                students.Add(new Student());
            
            float sum = 0;
            for (int i = 0; i < n; ++i)
                sum += students[i].GPA;

            sum /= n;
            Console.WriteLine("Diem trung binh cua tat ca hoc sinh: " + sum);

            for (int i = 0; i < n - 1; i++)     
                for (int j = 0; j < n - i - 1; j++)
                    if (students[j].GPA < students[j + 1].GPA)
                    {
                        Student temp = students[j];
                        students[j] = students[j + 1];
                        students[j + 1] = temp;
                    }

            Console.WriteLine("Danh sach 3 hoc sinh diem cao nhat:");
            Console.WriteLine(" - " + students[0].ToString());
            Console.WriteLine(" - " + students[1].ToString());
            Console.WriteLine(" - " + students[2].ToString());
        }
    }


}
