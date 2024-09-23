using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Models
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress(ErrorMessage = "Eamil Is Invalid")]
		[Required(ErrorMessage = "Email Is Required !")]
		public string Email { get; set; }
	}
}
