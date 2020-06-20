using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KalyanaInfo.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq.Expressions;
using KalyanaInfo.Areas.Validators;
using System.Net.Mail;
using Microsoft.Data.SqlClient.Server;

namespace KalyanaInfo.Areas.PeopleArea.Controllers
{
    [Area("PeopleArea")]
    public class PeopleController : Controller
    {
        private readonly kalyanadiaryContext _context;
        private readonly IActionContextAccessor accessor;
        private readonly IHostEnvironment _ENV;
        public PeopleController(kalyanadiaryContext context,IActionContextAccessor ac, IHostEnvironment env)
        {
            _context = context;
            accessor = ac;
            _ENV = env;
        }

        // GET: PeopleArea/People
        public async Task<IActionResult> Index(string query,string By)
        {

            if (query != null)
            {
                ViewBag.b = By;

                var kalyanadiaryContext = _context.Person.Include(p => p.EducationNavigation).Include(p => p.FamilyNavigation).Include(p => p.GenderNavigation).Include(p => p.HasMobileNavigation).Include(p => p.ProfessionNavigation).Include(p => p.VehicleNavigation).Where(p=>p.Name.Contains(query));
                return View(await kalyanadiaryContext.ToListAsync());
                
            }
            else
            { 

            var kalyanadiaryContext = _context.Person.Include(p => p.EducationNavigation).Include(p => p.FamilyNavigation).Include(p => p.GenderNavigation).Include(p => p.HasMobileNavigation).Include(p => p.ProfessionNavigation).Include(p => p.VehicleNavigation);
            return View(await kalyanadiaryContext.ToListAsync());
            }
        }

        // GET: PeopleArea/People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.EducationNavigation)
                .Include(p => p.FamilyNavigation)
                .Include(p => p.GenderNavigation)
                .Include(p => p.HasMobileNavigation)
                .Include(p => p.ProfessionNavigation)
                .Include(p => p.VehicleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            var logs = await _context.UserLog.Where(a=>a.UserId==id).FirstOrDefaultAsync();
            ViewBag.log = logs;
            if (person == null)
            {
                return NotFound();
            }
           
          
            return View(person);
        }

        // GET: PeopleArea/People/Create
        public IActionResult Create()
        {
            
            
            ViewData["Education"] = new SelectList(_context.Education, "Id", "Type");
            ViewData["Family"] = new SelectList(_context.Family, "Id", "Name");
            ViewData["Gender"] = new SelectList(_context.Gender, "Id", "GenderName");
            ViewData["HasMobile"] = new SelectList(_context.Mobile, "Id", "MobileType");
            ViewData["Profession"] = new SelectList(_context.Profession, "Id", "Name");
            ViewData["Vehicle"] = new SelectList(_context.Vehicle, "Id", "Name");
            return View();
        }

        // POST: PeopleArea/People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Password,SonOrDaughterOf,Address,Hobby,Mobile,DateOfBirth,HasMobile,Gender,Education,Profession,Vehicle,Family,About,Married,IdCardOrBForm")] Person person,IFormFile image)
        {
            string res = "";
            if (image!=null)
            {
                string DirPath = _ENV.ContentRootPath+"\\wwwroot\\Images\\PersonImages\\";
                string FileEx = Path.GetExtension(image.FileName);
                string fn = Guid.NewGuid()+FileEx;
                string FinalFileName = DirPath+fn;
                person.Image = fn;
             
                
                FileStream stream = new FileStream(FinalFileName, FileMode.Create);
               await image.CopyToAsync(stream);
            }
            if (person.IdCardOrBForm == null)
            {
                person.IdCardOrBForm = "Not Provided";
            }
            if (ModelState.IsValid)
            {
                IList <Person> p= _context.Person.ToList();
                foreach(Person per in p)
                {
                    if(per.Mobile==person.Mobile)
                    {
                        ModelState.AddModelError(string.Empty, "This Mobile Number Already Exist");
                        return View(person);
                    }
                }

                {
                    //  log.UserIp = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
                    person.City = "Kalyana";
                    
                    person.CreatedDate = DateTime.Now;
                    person.ModifiedDate = DateTime.Now;
                    Validations validation = new Validations();
                    res = validation.PersonValidation(person);
                    if (res == "")
                    {
                        _context.Add(person);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, res);
                        return View(person);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Education"] = new SelectList(_context.Education, "Id", "Upto", person.Education);
            ViewData["Family"] = new SelectList(_context.Family, "Id", "Name", person.Family);
            ViewData["Gender"] = new SelectList(_context.Gender, "Id", "GenderName", person.Gender);
            ViewData["HasMobile"] = new SelectList(_context.Mobile, "Id", "MobileType", person.HasMobile);
            ViewData["Profession"] = new SelectList(_context.Profession, "Id", "Name", person.Profession);
            ViewData["Vehicle"] = new SelectList(_context.Vehicle, "Id", "Name", person.Vehicle);
            return View(person);
        }

        // GET: PeopleArea/People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["Education"] = new SelectList(_context.Education, "Id", "Type", person.Education);
            ViewData["Family"] = new SelectList(_context.Family, "Id", "Name", person.Family);
            ViewData["Gender"] = new SelectList(_context.Gender, "Id", "GenderName", person.Gender);
            ViewData["HasMobile"] = new SelectList(_context.Mobile, "Id", "HasMobile", person.HasMobile);
            ViewData["Profession"] = new SelectList(_context.Profession, "Id", "Name", person.Profession);
            ViewData["Vehicle"] = new SelectList(_context.Vehicle, "Id", "Name", person.Vehicle);
            ViewBag.d = person.DateOfBirth;
            return View(person);
        }

        // POST: PeopleArea/People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,SonOrDaughterOf,Address,Hobby,Mobile,DateOfBirth,HasMobile,Gender,Education,Profession,Vehicle,Family,About,Married,CreatedDate,ModifiedDate,City,Image")] Person person,IFormFile image)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                  
                    {
                        string DirPath = _ENV.ContentRootPath + "\\wwwroot\\Images\\PersonImages\\";
                        string FileEx = Path.GetExtension(image.FileName);
                        string fn = Guid.NewGuid() + FileEx;
                        string FinalFileName = DirPath + fn;
                        person.Image = fn;
                        FileStream stream = new FileStream(FinalFileName, FileMode.Create);
                        await image.CopyToAsync(stream);
                      //person.City = "Kalyana";
                      
                    }
                    
                }
                try
                {
                    person.ModifiedDate = DateTime.Now;
                    person.IdCardOrBForm = "3640212345678";
                    person.City = "Kalyana";
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Education"] = new SelectList(_context.Education, "Id", "Type", person.Education);
            ViewData["Family"] = new SelectList(_context.Family, "Id", "Name", person.Family);
            ViewData["Gender"] = new SelectList(_context.Gender, "Id", "GenderName", person.Gender);
            ViewData["HasMobile"] = new SelectList(_context.Mobile, "Id", "HasMobile", person.HasMobile);
            ViewData["Profession"] = new SelectList(_context.Profession, "Id", "Name", person.Profession);
            ViewData["Vehicle"] = new SelectList(_context.Vehicle, "Id", "Name", person.Vehicle);
            return View(person);
        }

        // GET: PeopleArea/People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.EducationNavigation)
                .Include(p => p.FamilyNavigation)
                .Include(p => p.GenderNavigation)
                .Include(p => p.HasMobileNavigation)
                .Include(p => p.ProfessionNavigation)
                .Include(p => p.VehicleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: PeopleArea/People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

        public async Task<IActionResult> LogIn(string mob,string pass)
        {
            Person p = await _context.Person.Where(a => a.Mobile == mob && a.Password == pass).FirstOrDefaultAsync();
            if(p!=null)
            {
                HttpContext.Session.SetString("mobile",p.Mobile);
                HttpContext.Session.SetString("name", p.Name);
                HttpContext.Session.SetString("img", p.Image);
                HttpContext.Session.SetInt32("id", p.Id);
                UserLog log= await _context.UserLog.Where(user => user.UserId == p.Id).FirstOrDefaultAsync();
                if (log == null)
                {
                    log = new UserLog
                    {
                        UserId = p.Id,
                        UserIp = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                        UserLoginTime = DateTime.Now,
                        Location = p.Address
                    };
                    await _context.UserLog.AddAsync(log);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    log.UserId = p.Id;
                    log.UserIp = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
                    log.UserLoginTime = DateTime.Now;
                    log.Location = p.Address;
                    _context.Update(log);
                   await _context.SaveChangesAsync();
                }
              
                return RedirectToAction("Index","People",new { query ="",by=""});
            }
            
            
            return View(p);

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View(nameof(Index));
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> ForgetPassword(string mob,string email)
        {
            Person p = await _context.Person.Where(p => p.Mobile == mob && p.IdCardOrBForm == email).FirstOrDefaultAsync();
            if(p!=null)
            {
                try
                {
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress("kalyanainfo967@gmail.com", "KalyanaInfo")
                    };
                    mail.To.Add(p.Email);
                    mail.Subject = "New Password";
                    mail.Body = "<h1>Well Come To KalyanaInfo Once again:</h1></br>" +
                        "<h2>Your password is: </h2><h2>" + p.Password + "</h2>";
                    mail.IsBodyHtml = true;

                    SmtpClient server = new SmtpClient
                    {   
                        Host = "smtp.gmail.com",
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("kalyanainfo967@gmail.com", "bsef17m537"),
                        Port = 587,
                        EnableSsl = true
                       
                    };
                    server.Send(mail);
                    return View("Success");
                }
                catch(Exception e)
                {
                    ViewBag.er = e.Message;
                    return View();
                }
            }
            ViewBag.er = "Recovery Email or Mobile is invalid";
            return View();
        }


    }
}
