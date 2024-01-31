using Basic_Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Api.BL
{
    /// <summary>
    /// Business Logic class for managing student data.
    /// </summary>
    public class StudentBL
    {
        public static List<Student> lstStudents;
        static StudentBL()
        {
            lstStudents = new List<Student>
        {
            new Student { Id = 1,Name="Dimple Mithiya",Age=23,Course="MCA"},
            new Student { Id = 2,Name="Pankaj Mithiya",Age=28,Course="CE"},
            new Student { Id = 3,Name="Krishna Ram",Age=26,Course="MBA"},
            new Student { Id = 4,Name="Ankit Katarmal",Age=26,Course="MSC"},
            new Student { Id = 5,Name="Shiva Shakti",Age=32,Course="BBA"},
        };
        }
        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>List of all students.</returns>
        public List<Student> GetAllStudents()
        {
            return lstStudents;
        }

        /// <summary>
        /// Gets a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The student with the specified ID.</returns>
        public Student GetStudentById(int id)
        {
            return lstStudents.FirstOrDefault(stu => stu.Id == id);
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objStudent">The student to be added.</param>
        /// <returns>Result of the add operation.</returns>
        public string AddStudent(Student objStudent)
        {
            if (objStudent == null)
            {
                return "Invalid Input Data !!!";
            }

            // Creating a new student object
            Student newStudent = new Student();
            newStudent.Id = lstStudents.Count + 1;
            newStudent.Name = objStudent.Name;
            newStudent.Age = objStudent.Age;
            newStudent.Course = objStudent.Course;

            // Adding the new student to the list
            lstStudents.Add(newStudent);

            return "Student Added Successfully";
        }

        /// <summary>
        /// Edits an existing student.
        /// </summary>
        /// <param name="id">The ID of the student to be edited.</param>
        /// <param name="objStudent">The updated student information.</param>
        /// <returns>Result of the edit operation.</returns>
        public string EditStudent(int id, Student objStudent)
        {
            if (objStudent == null)
            {
                return "Invalid Input Data !!!";
            }

            // Finding the student to be edited
            Student editableStudent = lstStudents.FirstOrDefault(stu => stu.Id == id);
            if (editableStudent != null)
            {
                editableStudent.Name = objStudent.Name;
                editableStudent.Age = objStudent.Age;
                editableStudent.Course = objStudent.Course;

                return $"Student with {id} Updated Successfully";
            }

            return $"Student with {id} not found";
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>Result of the delete operation.</returns>
        public string DeleteStudent(int id)
        {
            Student studentToRemove = lstStudents.FirstOrDefault(stu => stu.Id == id);
            if (studentToRemove != null)
            {
                // Removing the student with the specified ID
                lstStudents.Remove(studentToRemove);
                return $"Student with {id} Deleted Successfully";
            }

            return $"Student with {id} not found";
        }
    }
}
