using Generic_Collection.Models;
using System.Collections.Generic;
using System.Linq;

namespace Generic_Collection.BL
{
    /// <summary>
    /// This Class Represents Persons CRUD.
    /// </summary>
    public class PersonsBL
    {
        // Static stack to store Person objects
        private static Stack<Person> _persons = new Stack<Person> { };

        /// <summary>
        /// Constructor to initialize the stack with 10 Person records.
        /// </summary>
        public PersonsBL()
        {
            // Initialize the stack before using it
            _persons = new Stack<Person>();

            // Push persons onto the stack
            for (int i = 1; i < 11; i++)
            {
                _persons.Push(new Person
                {
                    Id = i,
                    Name = "Person " + i,
                    Age = 23,
                });
            }
        }

        /// <summary>
        /// Get all Person records from the stack.
        /// </summary>
        public Stack<Person> GetAllPersonDetails()
        {
            return _persons;
        }

        /// <summary>
        /// Get the top Person record from the stack without removing it.
        /// </summary>
        public Person GetPerson()
        {
            return _persons.Peek();
        }

        /// <summary>
        /// Add a new Person to the stack.
        /// </summary>
        public Person AddPerson(Person objPerson)
        {
            objPerson.Id = _persons.Count + 1;
            _persons.Push(objPerson);
            return objPerson;
        }

        /// <summary>
        /// Remove the top Person record from the stack.
        /// </summary>
        public Stack<Person> RemovePerson()
        {
            _persons.Pop();
            return _persons;
        }
    }
}