using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic; //List


namespace StudentInfo
{

    class Employee
    {
        public string name;
        public int age;

        public Employee(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
    }

    class Student
    {

        // ---------- GENERAL INFO ----------
        //name of the student
        private string name;
        //student ID
        private readonly int studentId;
        //M - male, F - female
        private readonly char gender;
        //how old is this student
        private readonly int age;
        //how much can he/she drink?
        private readonly int maxDrink;
        //where is this person's home ground?
        private readonly string homeLocation;
        //does he/she TongHak?
        private readonly bool moveToSchool;
        //This variable is used in interacting algorithm.
        private readonly int friendly;

        // ---------- ACTION & STATUS ----------
        //is this person currently in relationship or in fight?
        private readonly bool isInteracting;
        //with whom?
        private readonly string whoInteracting;
        //what kind of interaction? -1 = fight / 0 = none / 1 = ssum / 2 = love
        private readonly int whatInteracting;

        // ** Info related to drinking
        //still alive?
        private bool film = true;
        //how much did this student drink?
        private int drink = 0;

        // ** Info related to SongByungHo game
        private int fingers = 5;

        // ---------- INTERACTION WITH ME ----------
        //this gage shows how much this person likes me
        //when you do something, the person will either enjoy or hate it
        //highest = 100 & lowest = 1
        private int status = 50;

        //just uppercase all for public use.
        public string Name() { return name; }
        public int StudentId() { return studentId; }
        public char Gender() { return gender; }
        public int Age() { return age; }
        public int MaxDrink() { return maxDrink; }
        public string HomeLocation() { return homeLocation; }
        public bool MoveToSchool() { return moveToSchool; }
        public int Friendly() { return friendly; }

        public bool IsInteracting() { return isInteracting; }
        public string WhoInteracting() { return whoInteracting; }
        public int WhatInteracting() { return whatInteracting; }

        public void Drink()
        {
            drink += 1;
            if (maxDrink < drink)
            {
                film = false;
            }
        }
        public bool Film() { return film; }

        public int Fingers() { return fingers; }

        public int Status() { return status; }
        public void Enjoy()
        {
            status += friendly * 3;
            if (status > 100) status = 100;
        }
        public void Hate()
        {
            status -= 30 - friendly;
            if (status <= 0) status = 1;
        }


        // ---------- MAIN CHARACTER SETTING ----------
        // USE IT TO MAIN CHARACTER ONLY!
        public void SetName(string nameInput) { this.name = nameInput; }

        public Student(string name, int studentId, char gender, int age, int maxDrink, string homeLocation, bool moveToSchool,
            int friendly, bool isInteracting, string whoInteracting, int whatInteracting)
        {
            this.name = name;
            this.studentId = studentId;
            this.gender = gender;
            this.age = age;
            this.maxDrink = maxDrink;
            this.homeLocation = homeLocation;
            this.moveToSchool = moveToSchool;
            this.friendly = friendly;
            this.isInteracting = isInteracting;
            this.whoInteracting = whoInteracting;
            this.whatInteracting = whatInteracting;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            List<Employee> eList = new List<Employee>();
            List<Student> sList = new List<Student>();

            eList.Add(new Employee("Kim", 25));

            sList.Add(new Student("승수", 201010, 'M', 20, 8, "파주", true, 20, true, "유진", -1));
            sList.Add(new Student("유진", 201013, 'M', 20, 0, "서울", true, 2, true, "승수", -1));
            sList.Add(new Student("세현", 201046, 'M', 20, 1, "부산", false, 4, false, "", 0));
            //상민 == female!!! remember~~
            sList.Add(new Student("상민", 201017, 'F', 22, 10, "춘천", false, 5, false, "", 0));
            sList.Add(new Student("승희", 201077, 'F', 21, 0, "대전", false, 9, true, "승현", 2));
            sList.Add(new Student("승현", 181022, 'M', 23, 3, "서울", false, 6, true, "승희", 2));
            sList.Add(new Student("경동", 201070, 'M', 19, 9, "대구", false, 10, true, "보람", 1));
            sList.Add(new Student("보람", 181051, 'F', 22, 10, "인천", true, 8, true, "경동", 1));
            sList.Add(new Student("효제", 181044, 'M', 22, 9, "제주", false, 3, false, "", 0));
            sList.Add(new Student("강민", 161022, 'M', 26, 5, "서울", true, 1, false, "", 0));

            sList.Add(new Student("주인", 201001, 'M', 20, 15, "서울", true, 0, false, "", 0));

            foreach(Student e in sList)
            {
                Console.WriteLine("{0}, {1}", e.Name(), e.Age());
            }
        }
    }
}
