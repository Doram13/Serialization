using System;
using SerializablePerson;

namespace Serializable
{ 
    public class Program
    {
        static void Main(string[] args)
        {

        }

        public static void PersonSerialization()
        {
            var person = GetSerializableMentor();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("superMentor.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, mentor);
            Console.WriteLine("Serialization was successful!");
            Console.WriteLine(mentor);

            stream.Close();
        }

        private static Serializable GetSerializableMentor()
        {
            var mentor = new SerializableMentor
            {
                Name = "SuperMentor",
                Specialities = new List<string>
                {
                    "C#",
                    "Java",
                    "Python",
                    "Mentor++"
                },
                DailyCoffeeNeed = 3
            };
            return mentor;
        }
    }
}
