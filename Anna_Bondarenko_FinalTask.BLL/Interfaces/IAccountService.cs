using System.Threading.Tasks;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using System.Web;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Generate and 
        /// </summary>
        /// <param name="model">User data</param>
        /// <returns>Forgot code data</returns>
        Task<UserForgotCode> GenerateForgotCode(ForgotPasswordDto model);

        /// <summary>
        /// Send forgot code to user
        /// </summary>
        /// <param name="callbackUrl">Calbak link</param>
        /// <param name="email">User email</param>
        void SendForgotLink(string callbackUrl, string email);

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="model">Reset password data</param>
        /// <returns>
        /// true-successful password reset
        /// false-failed password reset
        /// </returns>
        Task<bool> ResetPassword(ResetPassword model);

        /// <summary>
        ///Verify the image
        /// </summary>
        /// <param name="enrolleeDto"> Enrollee </param>
        /// <param name="image"> document for registration</param>
        EnrolleeDto VerifyImage(EnrolleeDto enrolleeDto, HttpPostedFileBase image);



    }
}
