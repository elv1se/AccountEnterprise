using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AccountEnterprise.Domain.Entities;

public class User : IdentityUser
{
    [Display(Name = "Имя")]
    public string? FirstName { get; set; }

    [Display(Name = "Фамилия")]
    public string? LastName { get; set; }


    [Display(Name = "Дата регистрации")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }


}
