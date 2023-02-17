using MaxiCrush.MAUI.MVVM.Models;

namespace MaxiCrush.MAUI.Services;

public class UserBuilder : IUserBuilder
{
    public string Email { get; private set; }

    public string Firstname { get; private set; }

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public SexualOrientation SexualOrientation { get; private set; }


    public void WithBirthday(DateOnly birthday)
    {
        Birthday = birthday;
    }

    public void WithEmail(string email)
    {
        Email = email;
    }

    public void WithFirstname(string firstname)
    {
        Firstname = firstname;
    }

    public void WithGender(Gender gender)
    {
        Gender = gender;
    }

    public void WithSexualOrientation(SexualOrientation sexualorientation)
    {
        SexualOrientation = sexualorientation;
    }
}
