using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is Required !")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password Min Lenght Is 5")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Confirm Password Is Required !")]
		[Compare(nameof(Password), ErrorMessage = "Confirmed Password Does Not Match Password")]
		public string ConfirmPassword { get; set; }
	}
}
