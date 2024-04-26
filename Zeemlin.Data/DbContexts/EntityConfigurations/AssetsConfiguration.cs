using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeemlin.Domain.Entities.Assets;

namespace Zeemlin.Data.DbContexts.EntityConfigurations
{
    public class AssetsConfiguration
    {
        public class SchoolLogoAssetConfiguration : IEntityTypeConfiguration<SchoolLogoAsset>
        {
            public void Configure(EntityTypeBuilder<SchoolLogoAsset> builder)
            {
                builder.ToTable("SchoolLogoAssets");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Path).IsRequired();
                builder.Property(e => e.UploadedDate).IsRequired();

                builder.HasOne(e => e.School)
                    .WithMany()
                    .HasForeignKey(e => e.SchoolId);
            }
        }
        public class SchoolAssetConfiguration : IEntityTypeConfiguration<SchoolAsset>
        {
            public void Configure(EntityTypeBuilder<SchoolAsset> builder)
            {
                builder.ToTable("SchoolAssets");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Path).IsRequired();
                builder.Property(e => e.UploadedDate).IsRequired();

                builder.HasOne(e => e.School)
                    .WithMany()
                    .HasForeignKey(e => e.SchoolId);
            }
        }

        public class HomeworkAssetConfiguration : IEntityTypeConfiguration<HomeworkAsset>
        {
            public void Configure(EntityTypeBuilder<HomeworkAsset> builder)
            {
                builder.ToTable("HomeworkAssets");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Path).IsRequired();
                builder.Property(e => e.UploadedDate).IsRequired();

                builder.HasOne(e => e.Homework)
                    .WithMany()
                    .HasForeignKey(e => e.HomeworkId);
            }
        }

        public class TeacherAssetConfiguration : IEntityTypeConfiguration<TeacherAsset>
        {
            public void Configure(EntityTypeBuilder<TeacherAsset> builder)
            {
                builder.ToTable("TeacherAssets");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Path).IsRequired();
                builder.Property(e => e.UploadedDate).IsRequired();

                builder.HasOne(e => e.Teacher)
                    .WithMany()
                    .HasForeignKey(e => e.TeacherId);
            }
        }

        public class EventAssetConfiguration : IEntityTypeConfiguration<EventAsset>
        {
            public void Configure(EntityTypeBuilder<EventAsset> builder)
            {
                builder.ToTable("EventAssets");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Path).IsRequired();
                builder.Property(e => e.UploadedDate).IsRequired();

                builder.HasOne(e => e.Event)
                    .WithMany()
                    .HasForeignKey(e => e.EventId);
            }
        }


        public class QuestionAssetConfiguration : IEntityTypeConfiguration<QuestionAsset>
        {
            public void Configure(EntityTypeBuilder<QuestionAsset> builder)
            {
                builder.ToTable("QuestionAssets");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Path).IsRequired();
                builder.Property(e => e.UploadedDate).IsRequired();

                builder.HasOne(e => e.Question)
                    .WithMany()
                    .HasForeignKey(e => e.QuestionId);
            }
        }

        public class VideoLessonAssetConfiguration : IEntityTypeConfiguration<VideoLessonAsset>
        {
            public void Configure(EntityTypeBuilder<VideoLessonAsset> builder)
            {
                builder.ToTable("VideoLessonAssets"); // Set table name
                builder.HasKey(a => a.Id); // Set primary key

                // Define properties with data types and validation (if needed)
                builder.Property(e => e.Title).HasMaxLength(255); // Optional title, limit length
                builder.Property(e => e.Description); // Optional description
                builder.Property(e => e.Path).IsRequired(); // Required path to the video file
                builder.Property(e => e.UploadedDate).IsRequired(); // Required upload date
                builder.Property(e => e.ContentType); // Optional content type (e.g., video/mp4)
                builder.Property(e => e.Size); // Optional size of the video file in bytes

                // Foreign key relationship with Lesson (One-to-Many)
                builder.HasOne(e => e.Lesson)
                    .WithMany(l => l.VideoLessons) // Lesson has many VideoLessonAssets
                    .HasForeignKey(e => e.LessonId);
            }
        }
    }
}
