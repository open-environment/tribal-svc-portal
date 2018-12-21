using System.ComponentModel.DataAnnotations;

namespace TribalSvcPortal.ViewModels.HomeViewModels
{
    public class InitializeViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
