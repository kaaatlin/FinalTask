using System;
using System.Transactions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//C:\\Users\\admin\\Desktop\\Students.dat

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    internal class Program
    {
        static Student[] getStudents(string path)
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Student[] students = (Student[])formatter.Deserialize(fs);
                    Console.WriteLine("Объект десериализован");
                    return students;
                }
            }
            return null;
        }
        static void CreateGroups(Student[] students)
        {
            Console.WriteLine("Папка с группами находится на Вашем рабочем столе");
            var folderr = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Получаем путь до рабочего стола
            var folder = Directory.CreateDirectory(folderr + "\\Students"); //Создаем папку на рабочем столе

            foreach (var student in students)
            {
                if (!File.Exists(folder + @"\" + student.Group + ".txt"))
                {
                    using (StreamWriter sw = File.CreateText(folder + @"\" + student.Group + ".txt"))
                    {
                        sw.WriteLine(student.Name + " " + student.DateOfBirth);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(folder + @"\" + student.Group + ".txt"))
                    {
                        sw.WriteLine(student.Name + " " + student.DateOfBirth);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            var path = EnterPath();
            var students = getStudents(path);
            CreateGroups(students);
        }

        static string EnterPath()
        {
            Console.WriteLine("Введите путь до файла: ");
            string path = Console.ReadLine();
            return path;
        }
    }
}
