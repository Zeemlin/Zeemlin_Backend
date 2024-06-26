﻿using Zeemlin.Domain.Enums;
using Zeemlin.Service.DTOs.Lesson;
using Zeemlin.Service.DTOs.Users.Students;

namespace Zeemlin.Service.DTOs.LessonAttendances;

public class LessonAttendanceForResultDto
{
    public long Id { get; set; }
    public long LessonId { get; set; }
    public LessonForResultDto LessonForResultDto { get; set; }
    public long StudentId { get; set; }
    public StudentForResultDto StudentForResultDto { get; set; }
    public DateTime Date { get; set; }
    public string LessonAttendanceType { get; set; }
}
