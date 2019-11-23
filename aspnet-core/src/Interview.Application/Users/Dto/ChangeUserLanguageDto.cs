using System.ComponentModel.DataAnnotations;

namespace Interview.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}