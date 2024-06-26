﻿using System.Text.Json.Serialization;
using Zeemlin.Service.DTOs.Group;
using Zeemlin.Service.DTOs.Homework;
using Zeemlin.Service.DTOs.Subjects;
using Zeemlin.Service.DTOs.Users.Teachers;

namespace Zeemlin.Service.DTOs.Lesson;

public class LessonForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public long GroupId { get; set; }
    public GroupForResultDto Group { get; set; }
    public long TeacherId { get; set; }
    public TeacherForResultDto Teacher { get; set; }
    [JsonIgnore]
    public ICollection<HomeworkForResultDto> Homework  { get; set; }
    [JsonIgnore]
    public ICollection<SubjectForResultDto> Subjects { get; set; }
}
