using Microsoft.AspNetCore.Mvc;
using VUA.Core.IRepositories;
using VUA.Core.Models;

namespace VUA.UI.ViewComponentes
{
    public class CourseViewComponent :ViewComponent
    {
        public IBaseRepository<Course> _CourseRepository;
        
        public CourseViewComponent(IBaseRepository<Course> CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }
        public  IViewComponentResult Invoke()
        {
            var courses=   _CourseRepository.GetAll();
            return View(courses);
        }
    }
    public class NewsViewComponent : ViewComponent
    {
        private IBaseRepository<News> _NewsRepository;
        public NewsViewComponent(IBaseRepository<News> NewsRepository)
        {
            _NewsRepository = NewsRepository;
        }
        public IViewComponentResult Invoke()
        {
            var news = _NewsRepository.GetAll();
            return View(news);

        }
    }
    public class SliderViewComponent : ViewComponent
    {
        public IBaseRepository<Slider> _SliderRepository;
        public SliderViewComponent(IBaseRepository<Slider> SliderRepository)
        {
            _SliderRepository = SliderRepository;
        }
        public IViewComponentResult Invoke()
        {
            var sliders = _SliderRepository.GetAll();
            return View(sliders);
        }
    }
    public class TestimonialsViewComponent : ViewComponent
    {
        public IBaseRepository<Testimonial> _Testimonialsrepository;
        public TestimonialsViewComponent(IBaseRepository<Testimonial> Testimonialsrepository)
        {
            _Testimonialsrepository = Testimonialsrepository;
        }
        public IViewComponentResult Invoke()
        {
            var testimoials = _Testimonialsrepository.GetAll();
            return View(testimoials);
        }
    }
    public class WhyAcademicsWorksViewComponent : ViewComponent
    {
        public IBaseRepository<WhyAcademicsWorks> _WAWRepository;
        public WhyAcademicsWorksViewComponent(IBaseRepository<WhyAcademicsWorks> WAWRepository)
        {
            _WAWRepository = WAWRepository;
        }
        public IViewComponentResult Invoke()
        {
            var WAW = _WAWRepository.GetAll();
            return View(WAW);
        }
    }
    public class TeacherViewComponent : ViewComponent
    {
        private IBaseRepository<AppllicationUser> _TeacherRepository;
        public TeacherViewComponent(IBaseRepository<AppllicationUser> TeacherRepository)
        {
            _TeacherRepository = TeacherRepository;
        }
        public IViewComponentResult Invoke()
        {
            var teachers = _TeacherRepository.GetAll().Where(x=> x.StudantOrTeacher =="Teacher");
            return View(teachers);
        }
    }



}
