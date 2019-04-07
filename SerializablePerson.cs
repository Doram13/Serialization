using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializable
{
    [Serializable]
    public class SerializablePerson : IDeserializationCallback
    {

        private string _name;
        private int _age;
        private Genders _gender;
        private DateTime birthDate;

        public string Name { get => _name; set => _name = value; }
        public Genders Gender { get => _gender; set => _gender = value; }
        public int Age { get => _age; set => _age = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }

        public enum Genders
        {
            Male,
            Female
        }

        public SerializablePerson(string name, Genders gender, DateTime birthDate)
        {
            this.Name = name;
            this.Gender = gender;
            this.BirthDate = BirthDate;
            SetAge(birthDate);
        }

        public void SetAge(DateTime birthDate)
        {
            Age = Math.Abs(DateTime.Today.Year - birthDate.Year);
        }




        public void Serialize(string output)
        {
            SerializablePerson personToSerialize = new SerializablePerson("Peter", Genders.Male, new DateTime(1901, 10, 20));

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, personToSerialize);
            }
        }

        public static SerializablePerson Deserialize(string input)
        {
            SerializablePerson deserializedPerson;

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                deserializedPerson = (SerializablePerson)formatter.Deserialize(stream);
            }

            return deserializedPerson;
        }

        public override string ToString()
        {
            return $"Person's name is: {Name}, age is: {Age}, gender is: {Gender}";
        }

        public void OnDeserialization(object sender)
        {
            SetAge(BirthDate);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
