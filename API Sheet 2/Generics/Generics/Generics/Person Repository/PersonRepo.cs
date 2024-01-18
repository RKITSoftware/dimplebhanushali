using Generics.IPerson;
using Generics.Models;
using System.Collections.Generic;
using System.Linq;

namespace Generics.Person_Repository
{
    /// <summary>
    /// Repository class for managing persons.
    /// </summary>
    public class PersonRepo : IPerson<Person>
    {
        // Static list to store persons.
        private static List<Person> _persons = new List<Person>
        {
            new Person{Id=1,Name="Dimple Bhanushali",City="Rajkot",PhoneNumber=9624863508 },
            new Person{Id=1,Name="Pankaj Bhanushali",City="Rajkot",PhoneNumber=9909572718}
        };

        /// <summary>
        /// Gets a person by ID.
        /// </summary>
        public Person GetById(int id)
        {
            return _persons.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Gets all persons.
        /// </summary>
        public List<Person> GetAll()
        {
            return _persons;
        }

        /// <summary>
        /// Adds a new person.
        /// </summary>
        public List<Person> Add(Person objPerson)
        {
            objPerson.Id = _persons.Count + 1;
            _persons.Add(objPerson);
            return _persons;
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        public List<Person> Update(int id, Person objPerson)
        {
            Person existingPerson = _persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson != null)
            {
                existingPerson.Name = objPerson.Name;
                existingPerson.City = objPerson.City;
                existingPerson.PhoneNumber = objPerson.PhoneNumber;
            }
            return _persons;
        }

        /// <summary>
        /// Deletes a person by ID.
        /// </summary>
        public List<Person> Delete(int id)
        {
            Person personToDelete = _persons.FirstOrDefault(p => p.Id == id);
            if (personToDelete != null)
            {
                _persons.Remove(personToDelete);
            }
            return _persons;
        }
    }
}