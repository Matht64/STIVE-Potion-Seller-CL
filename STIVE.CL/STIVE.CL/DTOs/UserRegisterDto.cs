using System.ComponentModel.DataAnnotations;

namespace STIVE.CL.DTOs;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
    [StringLength(50, ErrorMessage = "Le nom d'utilisateur doit contenir entre 3 et 50 caractères.", MinimumLength = 3)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "L'adresse mail est requise.")]
    [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Le mot de passe doit contenir au moins {2} caractères.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required(ErrorMessage = "La confirmation du mot de passe est requise.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
    public string ConfirmPassword { get; set; }
}