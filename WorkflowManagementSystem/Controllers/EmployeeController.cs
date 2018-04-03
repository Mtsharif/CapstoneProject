/*
 * Description: This file represents the Employee Controller class
 * Author: Mtsharif 
 * Date: 20/3/2018
 */

using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.ViewModels;
using WorkflowManagementSystem.Models;
using Microsoft.AspNet.Identity;
using AutoMapper;

namespace WorkflowManagementSystem.Controllers
{
    /// <summary>
    /// The Employee controller is created based on the Employee and EmployeeViewModel classes
    /// This controller manages employees as users (there are no subtypes for the employee) 
    /// This controller creates the views of creating, editing, listing, and deleting employees
    /// Only the admin is authorized to access this controller
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public EmployeeController()
        {
        }

        public EmployeeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// This action lists all the employees.
        /// </summary>
        /// <returns>Employee index view</returns>
        // GET: Employee
        public ActionResult Index()
        {
            var users = db.Employees.ToList();
            var model = new List<EmployeeViewModel>();
            foreach (var user in users)
            {
                model.Add(new EmployeeViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    JobTitle = user.JobTitle,
                    Department = user.Department,
                    EmployeeType = user.EmployeeType,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action retrieves the details of a selected employee. 
        /// It checks if the employee exists in the database
        /// </summary>
        /// <param name="id">The id of the selected employee</param>
        /// <returns>Error page or employee details view</returns>
        // GET: Employee/Details/5   
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                int userId = id ?? default(int);

                var user = UserManager.FindById(userId);

                if (user != null && user is Employee)
                {
                    var employee = (Employee)user;

                    EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                    model.Roles = string.Join(" | ", UserManager.GetRoles(userId).ToArray());

                    return View(model);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        /// <summary>
        /// This action displays the create employee page.
        /// </summary>
        /// <returns>Create employee view</returns>
        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        /// <summary>
        /// This action enables the admin to create new employees as users.
        /// It also assigns users to specific roles.
        /// </summary>
        /// <param name="model">Employee model</param>
        /// <param name="roles">Employee role</param>
        /// <returns>Employee index view or create model view</returns>
        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model, params string[] roles)
        {
            if (ModelState.IsValid)
            {
                Employee employee = Mapper.Map<EmployeeViewModel, Employee>(model);
                employee.UserName = model.UserName;

                var result = UserManager.Create(employee, model.Password);

                if (result.Succeeded)
                {
                    if (roles != null)
                    {
                        // Add user to selected roles
                        var roleResult = UserManager.AddToRoles(employee.Id, roles);

                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, roleResult.Errors.First());

                            // Create a check list object
                            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");

                            return View();
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
                    return View();
                }
            }

            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        /// <summary>
        /// This action displays an employee's information to be edited. 
        /// It verifies if the employee exists. 
        /// </summary>
        /// <param name="id">The id of a specfic employee</param>
        /// <returns>Employee edit model view or error page</returns>
        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);

                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return View("Error");
                }

                EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                var userRoles = UserManager.GetRoles(userId);
                var rolesSelectList = db.Roles.ToList().Select(r => new SelectListItem()
                {
                    Selected = userRoles.Contains(r.Name),
                    Text = r.Name,
                    Value = r.Name
                });

                ViewBag.RolesSelectList = rolesSelectList;

                return View(model);
            }

            return View("Error");
        }

        /// <summary>
        /// This action allows the admin to edit an employee's information.
        /// It checks if the id and employee exists
        /// </summary>
        /// <param name="id">The id of the selected employee</param>
        /// <param name="model">Employee model</param>
        /// <param name="roles">The role of an employee</param>
        /// <returns>Error page, employee index view, or edit model view</returns>
        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeViewModel model, params string[] roles)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {
                var userId = id ?? default(int);

                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                employee.UserName = model.UserName;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.JobTitle = model.JobTitle;
                employee.Department = model.Department;
                employee.EmployeeType = model.EmployeeType;

                var userResult = UserManager.Update(employee);

                if (userResult.Succeeded)
                {
                    var userRoles = UserManager.GetRoles(employee.Id);
                    roles = roles ?? new string[] { };
                    var roleResult = UserManager.AddToRoles(employee.Id, roles.Except(userRoles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    roleResult = UserManager.RemoveFromRoles(employee.Id, userRoles.Except(roles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// This action retrieves the employee's information.
        /// It verifies if the id and employee exist.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>Error page or employee view</returns>
        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);
                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);
                model.Roles = string.Join(" | ", UserManager.GetRoles(userId).ToArray());
                return View(model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// This action enables the admin to delete an employee.
        /// It verifies that the id and the user exists.
        /// </summary>
        /// <param name="id">The id of the user to be deleted</param>
        /// <returns>Error page or employee index view</returns>
        // POST: Employee/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {
                var userId = id ?? default(int);
                var user = UserManager.FindById(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}
