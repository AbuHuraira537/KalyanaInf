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
           
            if (By!=null&&query!=null)
            {
                if(By.Contains("Name"))
                {
                   var kalyanadiaryContext = _context.Person.Include(p => p.EducationNavigation).Include(p => p.FamilyNavigation).Include(p => p.GenderNavigation).Include(p => p.HasMobileNavigation).Include(p => p.ProfessionNavigation).Include(p => p.VehicleNavigation).Where(p => p.Name.Contains(query));
                    return View(await kalyanadiaryContext.ToListAsync());

                }
                else if (By.Contains("Address"))
                {
                    var kalyanadiaryContext = _context.Person.Include(p => p.EducationNavigation).Include(p => p.FamilyNavigation).Include(p => p.GenderNavigation).Include(p => p.HasMobileNavigation).Include(p => p.ProfessionNavigation).Include(p => p.VehicleNavigation).Where(p => p.Address.Contains(query));
                    return View(await kalyanadiaryContext.ToListAsync());
                }
                 
                else if (By.Contains("Contact"))
                {
                    var kalyanadiaryContext = _context.Person.Include(p => p.EducationNavigation).Include(p => p.FamilyNavigation).Include(p => p.GenderNavigation).Include(p => p.HasMobileNavigation).Include(p => p.ProfessionNavigation).Include(p => p.VehicleNavigation).Where(p => p.Mobile.Contains(query));
                    return View(await kalyanadiaryContext.ToListAsync());
                }
                else if (By.Contains("Profession"))
                {
                    var kalyanadiaryContext = _context.Profession.Where(p=>p.Name.Contains(query));
                    return View(await kalyanadiaryContext.ToListAsync());
                }
             
            }            
            var kalyanadiaryContex = _context.Person.Include(p => p.EducationNavigation).Include(p => p.FamilyNavigation).Include(p => p.GenderNavigation).Include(p => p.HasMobileNavigation).Include(p => p.ProfessionNavigation).Include(p => p.VehicleNavigation);               
                return View(await kalyanadiaryContex.ToListAsync());
          
        }
       public List<Person> GetPeople()
        {
            return _context.Person.ToList();
            
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
            var posts =await _context.Post.Where(p => p.UserId == id).ToListAsync();
            ViewBag.pts = posts;
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
        public async Task<IActionResult> Create([Bind("Name,Email,Password,SonOrDaughterOf,Address,Hobby,Mobile,DateOfBirth,HasMobile,Gender,Education,Profession,Vehicle,Family,About,Married,RecoverEmail,Private")] Person person,IFormFile image)
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
            if (person.RecoveryEmail == null)
            {
                person.RecoveryEmail = "Not Provided";
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,SonOrDaughterOf,Address,Hobby,Mobile,DateOfBirth,HasMobile,Gender,RecoveryEmail,Education,Profession,Vehicle,Family,About,Married,CreatedDate,ModifiedDate,City,Image,Private")] Person person,IFormFile image)
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
                    if(string.IsNullOrEmpty(person.RecoveryEmail))
                    {
                        person.RecoveryEmail = "3640212345678";
                    }
                    person.ModifiedDate = DateTime.Now;
                    
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


            //belongings of persons are detleted casecading through code
            var logs= _context.UserLog.Where(user => user.UserId == id);
            if(logs!=null)
            { 
            _context.RemoveRange(await logs.ToListAsync());
            }
            var mosq = _context.Mosque.Where(mos => mos.UserId == id);
            if(mosq!=null)
            {
                _context.RemoveRange(await mosq.ToListAsync());
            }
            var posts = _context.Post.Where(pos => pos.UserId == id);
            if(posts!=null)
            {
                 _context.RemoveRange(await posts.ToListAsync());
            }
            var video = _context.Video.Where(vid => vid.UserId == id);
            if(video!=null)
            {
                _context.RemoveRange(await video.ToListAsync());
            }
            var shopes = _context.Shope.Where(shop => shop.UserId == id);
            if(shopes!=null)
            {
                _context.RemoveRange(await shopes.ToListAsync());
            }
            var schls = _context.School.Where(sl => sl.UserId == id);
            if(schls!=null)
            {
                _context.RemoveRange(await schls.ToListAsync());
            }
             await _context.SaveChangesAsync();
            //session clear
            HttpContext.Session.Clear();
            //then delete person
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
            Administrator administrator = new Administrator();
            if(administrator.Login(mob,pass))
            {
                HttpContext.Session.SetString("mobile",administrator.Key);
                HttpContext.Session.SetString("name", administrator.Name);
                HttpContext.Session.SetString("img", "noimage");
                HttpContext.Session.SetInt32("id",-90);
                return RedirectToAction("Index", "People", new { query = "", by = "" });
            }
            
            Person p = await _context.Person.Where(a => a.Mobile == mob && a.Password == pass).FirstOrDefaultAsync();
            
            if(p!=null)
            {
                HttpContext.Session.SetString("mobile",p.Mobile);
                HttpContext.Session.SetString("name", p.Name);
                HttpContext.Session.SetString("img", p.Image);
                HttpContext.Session.SetInt32("id", p.Id);
                UserLog log= await _context.UserLog.Where(user => user.UserId == p.Id).FirstOrDefaultAsync();
                List<Post> posts= await _context.Post.Where(per => per.UserId == p.Id).ToListAsync();
                List<Shope> shopes = await _context.Shope.Where(per => per.UserId == p.Id).ToListAsync();
                HttpContext.Session.SetInt32("postcount",posts.Count());
                HttpContext.Session.SetInt32("shopecount", shopes.Count());
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
            else {

                ViewBag.nf = "Invalid Mobile or Password";
            return View(p);
            }
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
            Person p = await _context.Person.Where(p => p.Mobile == mob && p.RecoveryEmail == email).FirstOrDefaultAsync();
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
        public async Task<string> DeletePerson(int id)
        {

            try
            {
                var post = _context.Person.Find(id);
                Person p = post;
                if (p != null)
                {
                    //belongings of persons are detleted casecading through code
                    var logs = _context.UserLog.Where(user => user.UserId == id);
                    if (logs != null)
                    {
                        _context.RemoveRange(await logs.ToListAsync());
                    }
                    var mosq = _context.Mosque.Where(mos => mos.UserId == id);
                    if (mosq != null)
                    {
                        _context.RemoveRange(await mosq.ToListAsync());
                    }
                    var posts = _context.Post.Where(pos => pos.UserId == id);
                    if (posts != null)
                    {
                        _context.RemoveRange(await posts.ToListAsync());
                    }
                    var video = _context.Video.Where(vid => vid.UserId == id);
                    if (video != null)
                    {
                        _context.RemoveRange(await video.ToListAsync());
                    }
                    var shopes = _context.Shope.Where(shop => shop.UserId == id);
                    if (shopes != null)
                    {
                        _context.RemoveRange(await shopes.ToListAsync());
                    }
                    var schls = _context.School.Where(sl => sl.UserId == id);
                    if (schls != null)
                    {
                        _context.RemoveRange(await schls.ToListAsync());
                    }
                    await _context.SaveChangesAsync();
                    //session clear
                    HttpContext.Session.Clear();
                    //then delete person
                    _context.Remove(p);
                    _context.SaveChanges();
                    return "1";
                }
                return "0";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
