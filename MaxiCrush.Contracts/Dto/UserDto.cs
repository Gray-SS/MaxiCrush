using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MaxiCrush.Contracts.Dto;

public enum Gender
{
    Male,
    Female,
    Other
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    [NotMapped]
    public string Fullname => $"{Firstname} {Lastname}";

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender GenderInterestedIn { get; set; }
    public string Biography { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }

    public RoleDto Role { get; set; }
}