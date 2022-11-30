using System.ComponentModel.DataAnnotations;

namespace CollectionsOfMine.ViewModels
{

    public class CreateOrUpdateUser
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
