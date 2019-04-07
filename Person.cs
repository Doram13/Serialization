using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static Serializable.SerializablePerson;

namespace Serialization
{

    [Serializable]
    class Person : IDeserializationCallback, ISerializable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        private Genders _gender;
        public Genders Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }


        [NonSerialized]
        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public Person()
        {

        }

        public Person(string name, DateTime birthDate, Genders gender)
        {
            Name = name;
            BirthDate = birthDate;
            Gender = gender;

            SetAge(birthDate);
        }

        public void SetAge(DateTime birthDate)
        {
            Age = Math.Abs(DateTime.Today.Year - birthDate.Year);
        }

        public void Serialize(string output)
        {
            Person personToSerialize = new Person("Peter", new DateTime(1901, 10, 20), Genders.Male);

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, personToSerialize);
            }
        }

        public static Person Deserialize(string input)
        {
            Person deserializedPerson;

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                deserializedPerson = (Person)formatter.Deserialize(stream);
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