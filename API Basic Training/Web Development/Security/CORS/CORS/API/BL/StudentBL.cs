using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.BL
{
    /// <summary>
    /// Business logic class for managing student data.
    /// </summary>
    public class StudentBL
    {
        private static List<Students> lstStudents = new List<Students>
        {
            new Students { Id = 1, Name = "Dimple", Course = "MCA", Sem = 4 },
            new Students { Id = 2, Name = "Ankit", Course = "MSC", Sem = 3 },
            new Students { Id = 3, Name = "Pankaj", Course = "BCA", Sem = 2 },
            new Students { Id = 4, Name = "Krishna", Course = "EC", Sem = 1 },
            new Students { Id = 5, Name = "Abc", Course = "MCA", Sem = 4 },
        };

        /// <summary>
        /// Gets a list of all students.
        /// </summary>
        /// <returns>List of students.</returns>
        public List<Students> GetAllStudents()
        {
            return lstStudents;
        }

        /// <summary>
        /// Adds a new student to the list.
        /// </summary>
        /// <param name="objStu">The student to be added.</param>
        /// <returns>The added student.</returns>
        public Students AddStudent(Students objStu)
        {
            objStu.Id = lstStudents.Count + 1;
            lstStudents.Add(objStu);
            return objStu;
        }

        /// <summary>
        /// Edits details of an existing student.
        /// </summary>
        /// <param name="id">The ID of the student to be edited.</param>
        /// <param name="objStu">The updated student information.</param>
        /// <returns>The edited student.</returns>
        public Students EditStudentDetails(int id, Students objStu)
        {
            Students existingStu = lstStudents.FirstOrDefault(x => x.Id == id);
            existingStu.Name = objStu.Name;
            existingStu.Sem = objStu.Sem;
            existingStu.Course = objStu.Course;

            return existingStu;
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>The deleted student.</returns>
        public Students DeleteStudent(int id)
        {
            Students deletedStu = lstStudents.FirstOrDefault(stu => stu.Id == id);
            lstStudents.Remove(deletedStu);
            return deletedStu;
        }
    }
}
