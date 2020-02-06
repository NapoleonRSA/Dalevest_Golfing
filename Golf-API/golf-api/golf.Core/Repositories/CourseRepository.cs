using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO.CourseDTO_s;
using golf.Core.Interfaces;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace golf.Core.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly golfdbContext _context;

        public CourseRepository(golfdbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewCourse(DTONewCourse course)
        {
            try
            {
                var courseHoles = new List<Hole>();
                
                foreach (var hole in course.Holes)
                {
                    var courseHole = new Hole
                    {
                        hole_nr = hole.HoleNumber,
                        Par = hole.Par,
                        Stroke = hole.Stroke,
                        Score = 0
                    };
                    courseHoles.Add(courseHole);
                }
                var newCourse = new Course
                {
                    CourseName = course.CourseName,
                    Holes = courseHoles
                };
                _context.Course.Add(newCourse);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditCourse(DTOEditCourse courseToEdit)
        {
            try
            {
                Course course1 = await _context.Course.Where(a => a.Id == courseToEdit.Id).Include(b => b.Holes).FirstOrDefaultAsync();

                course1.CourseName = courseToEdit.CourseName;
                foreach (var hole in course1.Holes)
                {
                    var newValues = courseToEdit.Holes.Where(b => b.HoleNumber == hole.hole_nr).FirstOrDefault();
                    if (newValues != null) {
                        hole.Par = newValues.Par;
                        hole.Stroke = newValues.Stroke;
                    }

                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<DTOEditCourse> GetCourse(int id)
        {
            try
            {
                var course = await _context.Course.Include(y => y.Holes).Where(x => x.Id == id).SingleAsync();

                var courseObj = new DTOEditCourse()
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Holes = new List<DTONewCourseHole>()
                };

                foreach (var hole in course.Holes)
                {
                    DTONewCourseHole newhole = new DTONewCourseHole()
                    {
                        HoleNumber = hole.hole_nr,
                        Par = hole.Par,
                        Stroke = hole.Stroke
                    };
                    courseObj.Holes.Add(newhole);
                }
                courseObj.Holes = courseObj.Holes.OrderBy(y => y.HoleNumber).ToList();
                return courseObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<CourseDTO>> GetAllCourses()
        {
            try
            {
                var courseList = new List<CourseDTO>();
                var dbCourseList = await _context.Course.ToListAsync();
                foreach (var course in dbCourseList)
                {
                    var courseName = new CourseDTO
                    {
                        Id = course.Id,
                        CourseName = course.CourseName
                    };
                    courseList.Add(courseName);
                }

                return courseList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        
    }
}
