﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Domain.Entities;
using Zeemlin.Domain.Entities.Questions;
using Zeemlin.Domain.Entities.Events;
using Zeemlin.Domain.Entities.Assets;

namespace Zeemlin.Data.DbContexts.EntityConfigurations;

public class SchoolConfiguration
{
    public class SchoolConfigurations : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("Schools");
            builder.HasKey(s => s.Id);

            // Define property configurations
            builder.Property(s => s.SchoolNumber).IsRequired();
            builder.Property(s => s.SchoolType).IsRequired();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(2000);
            builder.Property(s => s.Country).IsRequired();
            builder.Property(s => s.Region).IsRequired();
            builder.Property(s => s.DistrictName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.GeneralAddressMFY).IsRequired().HasMaxLength(50);
            builder.Property(s => s.StreetName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.CallCenter);
            builder.Property(s => s.EmailCenter);
            builder.Property(s => s.Website);

            // Define relationships
            builder.HasMany(s => s.Asset)
              .WithOne(a => a.School)
              .HasForeignKey(a => a.SchoolId);

            builder.HasMany(s => s.Courses)
              .WithOne(c => c.School)
              .HasForeignKey(c => c.SchoolId);

            // One-to-One relationship with cascade delete for SchoolLogoAsset
            builder.HasOne(s => s.SchoolLogoAsset)
              .WithOne(a => a.School)
              .HasForeignKey<SchoolLogoAsset>(a => a.SchoolId)
              .OnDelete(DeleteBehavior.Cascade); 
        }
    }


    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired(); ;
            builder.Property(e => e.Description);
            builder.Property(e => e.price).IsRequired();
            builder.Property(e => e.SchoolId).IsRequired();

            builder.HasMany(e => e.Groups)
                .WithOne(g => g.Course)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description);
            builder.Property(e => e.CourseId).IsRequired();
            

            builder.HasMany(e => e.StudentGroups)
                .WithOne(sg => sg.Group)
                .HasForeignKey(sg => sg.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.TeacherGroups)
                .WithOne(tg => tg.Group)
                .HasForeignKey(tg => tg.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.ToTable("StudentGroups");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StudentId).IsRequired();
            builder.Property(e => e.GroupId).IsRequired();
        }
    }

    public class TeacherGroupConfiguration : IEntityTypeConfiguration<TeacherGroup>
    {
        public void Configure(EntityTypeBuilder<TeacherGroup> builder)
        {
            builder.ToTable("TeacherGroups");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TeacherId).IsRequired();
            builder.Property(e => e.GroupId).IsRequired();
            builder.Property(e => e.Role).IsRequired();


        }
    }

    public class ParentStudentConfiguration : IEntityTypeConfiguration<ParentStudent>
    {
        public void Configure(EntityTypeBuilder<ParentStudent> builder)
        {
            builder.ToTable("ParentStudents");
            builder.HasKey(e => e.Id);

            builder.Property(ps => ps.ParentId).IsRequired();
            builder.Property(ps => ps.StudentId).IsRequired();
        }
    }

    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");
            builder.HasKey(e => e.Id);

            // Define properties with data types
            builder.Property(e => e.Title).IsRequired().HasMaxLength(255); // Enforce title length
            builder.Property(e => e.Description);
            builder.Property(e => e.StartDate).IsRequired(); // Make sure start date is required
            builder.Property(e => e.EndDate).IsRequired(); // Make sure end date is required
            builder.Property(e => e.GroupId).IsRequired();
            builder.Property(e => e.TeacherId).IsRequired();

            // Foreign key relationships (One-to-Many)
            builder.HasMany(e => e.Homework)
                .WithOne(h => h.Lesson)
                .HasForeignKey(h => h.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete on Lesson deletion

            builder.HasMany(e => e.Subjects)
                .WithOne(s => s.Lesson)
                .HasForeignKey(s => s.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete on Lesson deletion

            // One-to-Many relationship with Question
            builder.HasMany(e => e.Questions)
                .WithOne(q => q.Lesson)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete on Lesson deletion

            // One-to-Many relationship with VideoLessonAsset
            builder.HasMany(e => e.VideoLessons)
                .WithOne(v => v.Lesson)
                .HasForeignKey(v => v.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete on Lesson deletion
        }
    }



    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.ToTable("Homeworks");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title);
            builder.Property(e => e.Description);
            builder.Property(e => e.Deadline).IsRequired();
            builder.Property(e => e.LessonId).IsRequired();

            builder.HasMany(e => e.Assets)
                .WithOne(a => a.Homework)
                .HasForeignKey(a => a.HomeworkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name);
            builder.Property(e => e.Description);
            builder.Property(e => e.LessonId).IsRequired();
        }
    }

    public class LessonAttendanceConfiguration : IEntityTypeConfiguration<LessonAttendance>
    {
        public void Configure(EntityTypeBuilder<LessonAttendance> builder)
        {
            builder.ToTable("LessonAttendances");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.LessonId).IsRequired();
            builder.Property(e => e.StudentId).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();
            builder.Property(e => e.LessonAttendanceType).IsRequired();
        }
    }

    public class LessonTableConfiguration : IEntityTypeConfiguration<LessonTable>
    {
        public void Configure(EntityTypeBuilder<LessonTable> builder)
        {
            builder.ToTable("LessonTables");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Title).IsRequired().HasMaxLength(100);
            builder.Property(e => e.LessonId).IsRequired();
            builder.Property(e => e.TeacherId).IsRequired();
            builder.Property(e => e.Classroom).IsRequired().HasMaxLength(100);

        }
    }

    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.LessonId).IsRequired();
            builder.Property(e => e.DateTimeCreated).IsRequired();
            builder.Property(e => e.AssessmentType).IsRequired();
            builder.Property(e => e.Value).IsRequired();
        }
    }

    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text).IsRequired();
            builder.Property(q => q.Description);
            builder.Property(q => q.CreatedAt); // Consider adding creation date property

            builder.HasMany(q => q.QuestionAssets) // Corrected to HasMany
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Text).IsRequired();
            builder.Property(a => a.IsCorrect).IsRequired();

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);
        }
    }

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(a => a.Id);

            // Define required properties
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Orginizer).IsRequired();
            builder.Property(e => e.Location).IsRequired();
            builder.Property(e => e.Contact).IsRequired();

            // Define relationship with EventAsset
            builder.HasOne(e => e.EventAsset)
                .WithOne(a => a.Event)
                .HasForeignKey<EventAsset>(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Delete EventAsset when Event is deleted

            // One-to-Many relationship with EventRegistration
            builder.HasMany(e => e.Registrations)
                .WithOne(er => er.Event)
                .HasForeignKey(er => er.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class EventRegistrationConfiguration : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {
            builder.ToTable("EventRegistrations");
            builder.HasKey(er => er.Id);

            builder.Property(er => er.EventId).IsRequired();

            builder.HasOne(er => er.Event)
                .WithMany(e => e.Registrations) // Event has many EventRegistrations
                .HasForeignKey(er => er.EventId);

            builder.Property(er => er.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(er => er.LastName).IsRequired().HasMaxLength(50);
            builder.Property(er => er.Email).IsRequired().HasMaxLength(255);
            builder.Property(er => er.RegistrationDate).IsRequired();

            // RegistrationCode property configuration
            builder.Property(er => er.RegistrationCode).IsRequired().HasMaxLength(10);

        }
    }
}
