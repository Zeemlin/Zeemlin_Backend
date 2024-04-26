using Zeemlin.Domain.Commons;
using Zeemlin.Domain.Entities.Assets;
using Zeemlin.Domain.Entities.Questions;
using Zeemlin.Domain.Entities.Users;

namespace Zeemlin.Domain.Entities;

public class Lesson : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }


    public ICollection<Subject> Subjects { get; set; }
    public ICollection<Homework> Homework { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<VideoLessonAsset> VideoLessons { get; set; }
    public ICollection<LessonAttendance> LessonAttendances { get; set; }
}
