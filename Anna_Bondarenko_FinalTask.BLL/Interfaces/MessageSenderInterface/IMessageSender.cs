using Anna_Bondarenko_FinalTask.BLL.DTO;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface
{
    public interface IMessageSender
    {
        /// <summary>
        /// Sending message
        /// </summary>
        /// <param name="sendMessage">model with parameters for sending</param>
        void SendToUs(MessageDto sendMessage);

        /// <summary>
        /// Send forgot link by Email
        /// </summary>
        /// <param name="callbackUrl">Forgot link</param>
        /// <param name="email">Recipient's mail</param>
        void SendForgotLink(string callbackUrl, string email);

    }
}