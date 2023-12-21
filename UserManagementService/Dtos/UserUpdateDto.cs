using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
