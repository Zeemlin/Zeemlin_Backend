using Zeemlin.Service.DTOs.Assets.TeacherAssets;

namespace Zeemlin.Service.DTOs.Users.Teachers;

public class FilteredTeacherDTO
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Consider hiding DateOfBirth unless necessary for specific purposes
    public string DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string DistrictName { get; set; }
    public string ScienceType { get; set; }


    public TeacherAssetForResultDto TeacherAssetForResultDto { get; set; }
}
