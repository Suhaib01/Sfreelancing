using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VUA.Core.Models;
using VUA.EF;
using System.Collections.Generic;
using VUA.Core.Models.ViewModels;
using VUA.Core.IRepositories;
using VUA.Core.IRepositories.IService;
using VUA.EF.Repositories;
using System.IO;
using System.Threading.Tasks;
using System.Web;
//using Humanizer;
//using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using freelancing.core.Models.ViewModels;



namespace VUA.UI.Areas.Studant.Controllers
{
    [Area("AppUsers")]
    //[Authorize(Roles ="AppUsers")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;
        private readonly IBaseRepository<Course> _cousreRepository;
        private readonly IBaseRepository <UserCourse> _userCourseRepository;
        private readonly IAccountRepositorory _accountRepositorory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBaseRepository<UserPayment> _userPaymentRepository;
        private readonly UserManager<AppllicationUser> _userManager;
        private readonly IBaseRepository<AppllicationUser> _appuserRepository;
		private readonly IBaseRepository<CourseWeeks> _courseWeeksRepository;
        private readonly IBaseRepository<Week> _weeksRepository;
        private readonly IWebHostEnvironment _webhostEnvauroment;
        private readonly SignInManager<AppllicationUser> _signInManager;
        private readonly IBaseRepository<Video> _videoRepository;
        private readonly IBaseRepository<Core.Models.File> _fileRepository;
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        public DashboardController(IAccountRepositorory accountRepositorory,
            IWebHostEnvironment webHostEnvironment,
			IUserService userService,
			IBaseRepository<Course> cousreRepository,
			IConfiguration configuration,
			IBaseRepository<UserPayment> userpaymentRepository,
			UserManager<AppllicationUser> userManager,
			IBaseRepository<UserCourse> userCourseRepository,
			IBaseRepository<AppllicationUser> appuserRepository,
			IBaseRepository<CourseWeeks> courseWeeksRepository,
            IBaseRepository<Week> weekRepository,
            IWebHostEnvironment webHost,
            SignInManager<AppllicationUser> signInManager,
            IBaseRepository<Video> videoUrlRepository,
            IBaseRepository<Core.Models.File> fileRepository,
            IDbContextFactory<AppDbContext> dbFactory,
            AppDbContext db
            )
		{
			_webHostEnvironment = webHostEnvironment;
			_accountRepositorory = accountRepositorory;
			_userService = userService;
			_cousreRepository = cousreRepository;
			_userPaymentRepository = userpaymentRepository;
			_userManager = userManager;
			_userCourseRepository = userCourseRepository;
			_appuserRepository = appuserRepository;
			_courseWeeksRepository = courseWeeksRepository;
            _weeksRepository = weekRepository;
            _webhostEnvauroment = webHost;
            _signInManager = signInManager;
            _videoRepository = videoUrlRepository;
            _fileRepository = fileRepository;
            _dbFactory = dbFactory;
            _db = db;
		}
		public IActionResult Index()
        {
            var user = _accountRepositorory.IsSignInUser(User);
			var course = _userCourseRepository.GetValues("Course").Where(c => c.UserId == user.Id);
            var freelancers = new List<AppllicationUser>();
            if (course.Any()) 
            {
                foreach (var item in course)
                {
                    var co = _cousreRepository.Find(item.CourseId);
                    freelancers = _userManager.Users.Where(f=>f.Major == co.CourseName).ToList();
                }
                ViewBag.freelancers= freelancers;
                return View(user);
            }
            else
            {
                if (user.StudantOrTeacher == "Teacher")
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Recommendations");
                }
                
            }
            
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var user = _accountRepositorory.GetApplicationUser(User).Result;             
            var course = _userCourseRepository.GetValues("Course").Where(c => c.UserId == user.Id);
            var InsCourses = _cousreRepository.GetAll().Where(c=>c.InstructorId == user.Id);
            ViewBag.StudantCourse = course;
            ViewBag.Count = course.Count();
            ViewBag.InsCourse = InsCourses;
            return View(user);
        }

        [HttpGet]
      
        public IActionResult CompleteTeacherProfile()
        {
            
            return View();
        }
        [HttpGet]

        public IActionResult CompleteProfile()
        {
            
            return View();
        }
        [HttpPost]
   
        public async Task<IActionResult> CompleteProfile(CompleteProfileModelView model)
        {
            if(_accountRepositorory.ChekPhoneNumper(model.PhoneNumber!) == true) { ViewBag.err = "● Exist Phone number!"; return View(model); }
            if (ModelState.IsValid)
            {
                if (model.profilePictuer != null)
                {
                    var result = await _accountRepositorory.CompleteUserProfile(model, _webHostEnvironment.WebRootPath);
                    if (result.Succeeded)
                    {
                        ViewBag.sucsses = true;
                        return View();
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                    return View(model);
                }
                return View(model);
            }
            return View(model);

        }
        [HttpPost]

        public async Task<IActionResult> CompleteTeacherProfile(CompleatTeacherProfileViewModel model)
        {
            if (_accountRepositorory.ChekPhoneNumper(model.PhoneNumber!) == true) { ViewBag.err = "● Exist Phone number!"; return View(model); }
            if (ModelState.IsValid)
            {
                if (model.profilePictuer != null)
                {
                    var result = await _accountRepositorory.CompleteTeacherUserProfile(model, _webHostEnvironment.WebRootPath);
                    if (result.Succeeded)
                    {
                        ViewBag.sucsses = true;
                        return View();
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                    return View(model);
                }
                return View(model);
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult ChangePicture()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePicture(ChangePictuerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.profilePictuer != null)
                {
                    string folder = "uploads/ProfilsPuctuers/Chenged/";
                    var studant = _accountRepositorory.GetApplicationUser().Result;

                    studant!.ProfileImage = await _accountRepositorory.UploadImag(folder, model.profilePictuer, _webHostEnvironment.WebRootPath);
                    var result = await _accountRepositorory.UpdateUserAsync(studant);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile", studant);
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                }
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult viewFImg()
        {
            var studant = _accountRepositorory.IsSignInUser(User);
            return View(studant);
        }
        [HttpGet]

        public IActionResult CertificateImg()
        {
            var studant = _accountRepositorory.IsSignInUser(User);
            return View(studant);
        }
        [HttpGet]
        public IActionResult viewBImg()
        {

            var studant = _accountRepositorory.IsSignInUser(User);
            return View(studant);
        }
        [HttpGet]
        public IActionResult viewPImg()
        {

            var studant = _accountRepositorory.IsSignInUser(User);
            return View(studant);
        }
        [HttpGet]
        [Authorize(Roles = "Studant")]
        public IActionResult AddCourse()
        {
            var courses = _userService.GetCourses();
            var userId = _userService.GetUserId();
            var userCourses = _userCourseRepository.GetValues("Course").Where(uc => uc.UserId == userId);
          
            ViewBag.Course =courses.Where(c=>!userCourses.Any(uc=>uc.CourseId == c.CourseId)&&c.InstructorId!=""&& c.InstructorId != null).ToList();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Studant")]
        public async Task<IActionResult> AddCourse(AddCourseViewModel course)
        {
            var co = _cousreRepository.Find(course.Id);
            TempData["ID"] = co.CourseId;
            TempData["Total"] = co.CoursePrice;
            TempData["Name"] = co.CourseName;
            return RedirectToPage("/Checkout");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViweModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepositorory.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Studant")]
        public  IActionResult MyCourse()
        {
            var userId = _userService.GetUserId();
            var courses = _userCourseRepository.GetValues("Course").Where(c => c.UserId == userId);
            return View(courses);
        }
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult TeacherCourses()
        {
            var user = _accountRepositorory.GetApplicationUser().Result;
            var InsCourses = _cousreRepository.GetAll().Where(c => c.InstructorId == user.Id);
            ViewBag.InsCourse = InsCourses;

            return View();
        }

        [Authorize(Roles = "Studant")]
        public IActionResult PaymentHistory()
        {
            var userid = _userService.GetUserId();
            

            var Payments = _userPaymentRepository.GetValues("Payment").Where(p=>p.UserId == userid);

            return View(Payments);

            
        }
       
        [HttpGet]
        public IActionResult CoursePage()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public  IActionResult CreateCourseContant(int id)
        {
            var course = _cousreRepository.Find(id);
            ViewBag.coId= id;
            ViewBag.CoNum = id;
            return View();
        }
        //[HttpGet]
        //[Authorize(Roles = "Teacher")]
        //public IActionResult Redirect(int id)
        //{
        //    var course =  _cousreRepository.Find(id);
        //    return RedirectToAction("CreateCourseContant",course);
        //}
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateCourseContant(CreateCourseContantViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var courseContant = _courseWeeksRepository.GetValues("Week").Where(c => c.CourseId == id);
                string folder = "uploads/Videos/";
                var course = _cousreRepository.Find(id);
                if (course == null)
                {
                    return NotFound();
                }
                var week = new Week
                {
                    WhatStudantShouldDo = model.WhatStudantShouldDo,
                    EndOfThisWeek = model.EndOfThisWeek,
                    Description = model.Description,
                    SubjectName = model.SubjectName,
                    
                };
                week.Videos = new List<Video>();
                for (int i = 0; i < model.Videos!.Count; i++)
                {

                    var video = new Video
                    {
                       
                        VideoUrl = await _accountRepositorory.UploadPowerPoint(model.Videos![i], _webHostEnvironment.WebRootPath),
                        WeekId = week.WeekId,
                        Week = week
                    };
                    //_videoRepository.Add(video);
                    week.Videos!.Add(video);
                }
                week.Files = new List<Core.Models.File>();
                for (int i = 0; i < model.Files!.Count; i++)
                {

                    var file = new Core.Models.File
                    {

                        FileUrl = await _accountRepositorory.UploadPowerPoint(model.Files![i], _webHostEnvironment.WebRootPath),
                        WeekId = week.WeekId,
                        Week= week
                        
                    };
                    //_fileRepository.Add(file);
                    week.Files!.Add(file);
                }
                _weeksRepository.Add(week);
                var courseWeek = new CourseWeeks
                {
                    CourseId = course.CourseId,
                    WeekId = week.WeekId,
                    
                };
                _courseWeeksRepository.Add(courseWeek);


                return RedirectToAction("Contant", new { id = course.CourseId });
               
            }
            return View(model);
        }

        public IActionResult Contant(int id)
        {
            
            var courseContant = _courseWeeksRepository.GetValues("Week").Where(c => c.CourseId ==id);
            if (courseContant.Any())
            {
                var Week = new List<Week>();
                foreach (var course in courseContant)
                {
                    var week = _weeksRepository.Find(course.WeekId);
                    week.Videos = _videoRepository.GetAll().Where(v=>v.WeekId == course.WeekId).ToList();
                    week.Files = _fileRepository.GetAll().Where(v => v.WeekId == course.WeekId).ToList();
                    Week.Add(week);

                }
                ViewBag.CoID = id;
            return View(Week);
            }
            else
            {
               return NoContent();
            }
            
        }
        

        [Authorize(Roles = "Studant")]
        [HttpGet]
        public IActionResult DropCourse(int courseId)
        {
            var co = _cousreRepository.Find(courseId);
            return View(co);
        }
        [Authorize(Roles = "Studant")]
        [HttpPost]
        public IActionResult DropCourseP(int courseId)
        {
            var studant = _userManager.FindByEmailAsync(User.Identity!.Name!).Result;
            _appuserRepository.deleteCourseFromUser(studant!.Id, courseId);
            return RedirectToAction("MyCourse");
        }
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteWeek(int courseId, int weekId)
        {
            var courseWeek = _courseWeeksRepository.GetValues("Week").Where(c => c.CourseId == courseId && c.WeekId==weekId);
           
           
                return View(courseWeek.First());
           
            
        }
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteWeekP(int courseId, int weekId)
        {
            _appuserRepository.deleteWeekFromCourse(weekId, courseId);
            var courseContant = _courseWeeksRepository.GetValues("Week").Where(c => c.CourseId == courseId);
            if (courseContant.Any())
            {
                return RedirectToAction("Contant", new { id = courseId });
            }
            return RedirectToAction("TeacherCourses");

        }
		[HttpGet]
		[Authorize(Roles = "Studant")]
		public IActionResult searchCourses()
		{
			return View("AddCourse");
		}
		[HttpPost]
		[Authorize(Roles = "Studant")]
		public IActionResult searchCourses(string term)
		{
			if (term != null)
			{
				var courses = _userService.GetCourses();
				var userId = _userService.GetUserId();
				var userCourses = _userCourseRepository.GetValues("Course").Where(uc => uc.UserId == userId);

                ViewBag.Course2 = courses.Where(c => !userCourses.Any(uc => uc.CourseId == c.CourseId) && c.InstructorId != "" && c.InstructorId != null && c.CourseName!.Contains(term)).ToList();
                //ViewBag.Course2 = _cousreRepository.GetAll().Where(c => c.CourseName!.Contains(term)).ToList();
				return View("AddCourse");
			}
			return View("AddCourse");
        }
        [HttpGet]
        public IActionResult Recommendations()
        {
            var courses = _db.Courses.ToList();
            return View(courses);
        }
        [HttpPost]
        public  IActionResult Recommendations(List<int> selectedCourses)
        {
            foreach (var item in selectedCourses)
            {
                var Co = _cousreRepository.Find(item);
                UserCourse userCourse = new UserCourse
                {
                    CourseId = Co.CourseId,
                    UserId = _userManager.FindByEmailAsync(User.Identity!.Name!).Result!.Id                
                };
                _userCourseRepository.Add(userCourse);
            }
 

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Posts()
        {
            //ViewBag.id=id;
            return View();
        }
        public IActionResult ShowMyPosts()
        {
            var user = _accountRepositorory.IsSignInUser(User);
            var course = _userCourseRepository.GetValues("Course").Where(c => c.UserId == user.Id);
            if (course == null)
            {
                return NoContent();
            }
            else
            {
                return View(course);
            }
            
        }


    }

}


