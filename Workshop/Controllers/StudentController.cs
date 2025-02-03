using Microsoft.AspNetCore.Mvc;
using Workshop.Models;
using System.Collections.Generic;

namespace Workshop.Controllers
{
    public class StudentController : Controller
    {
        private readonly Student studentModel = new Student();

        // ✅ List All Students
        public IActionResult Index()
        {
            List<Student> students = studentModel.getData("");
            return View(students);
        }

        // ✅ Show Add Student Form
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        // ✅ Handle Add Student Submission
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                bool isAdded = studentModel.insert(student);
                if (isAdded)
                {
                    TempData["SuccessMessage"] = "Student added successfully!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Failed to add student.");
            }
            return View(student);
        }

        // ✅ Show Edit Form
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            List<Student> students = studentModel.getData(id.ToString());
            if (students == null || students.Count == 0)
            {
                TempData["ErrorMessage"] = "Student not found!";
                return RedirectToAction("Index");
            }
            return View(students[0]);
        }

        // ✅ Handle Edit Submission
        [HttpPost]
        public IActionResult EditStudent(Student updatedStudent)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = studentModel.update(updatedStudent);
                if (isUpdated)
                {
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Failed to update student.");
            }
            return View(updatedStudent);
        }

        // ✅ Show Delete Confirmation Page
        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {
            List<Student> students = studentModel.getData(id.ToString());
            if (students == null || students.Count == 0)
            {
                TempData["ErrorMessage"] = "Student not found!";
                return RedirectToAction("Index");
            }
            return View(students[0]);
        }

        // ✅ Handle Delete Confirmation
        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult DeleteStudentConfirmed(int id)
        {
            bool isDeleted = studentModel.delete(id);
            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Student deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Failed to delete student.";
            return RedirectToAction("Index");
        }
    }
}
