using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Models
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="User Name Is Required !")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "First Name Is Required !")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required !")]
		public string LastName { get; set; }

		[EmailAddress(ErrorMessage ="Eamil Is Invalid")]
		[Required(ErrorMessage = "Email Is Required !")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required !")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage ="Password Min Lenght Is 5")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Confirm Password Is Required !")]
		[Compare(nameof(Password), ErrorMessage ="Confirmed Password Does Not Match Password")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Please Check On Is Agree (Required !!)")]
		public bool IsAgree { get; set; }
	}
}
