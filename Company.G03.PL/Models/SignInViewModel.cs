using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Models
{
	public class SignInViewModel
	{
		[EmailAddress(ErrorMessage = "Eamil Is Invalid")]
		[Required(ErrorMessage = "Email Is Required !")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required !")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password Min Lenght Is 5")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
