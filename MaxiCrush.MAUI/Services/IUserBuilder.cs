using MaxiCrush.MAUI.MVVM.Models;
using Microsoft.Maui.Platform;

namespace MaxiCrush.MAUI.Services;

public interface IUserBuilder
{
    string Email { get; }
    string Firstname { get; }
    DateOnly Birthday { get; }
    Gender Gender { get; }
    SexualOrientation SexualOrientation { get; }

    void WithEmail(string email);
    void WithFirstname(string firstname);
    void WithBirthday(DateOnly birthday);
    void WithGender(Gender gender);
    void WithSexualOrientation(SexualOrientation orientation);
}
