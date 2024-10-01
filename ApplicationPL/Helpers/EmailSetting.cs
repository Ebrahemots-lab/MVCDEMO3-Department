using ApplicationDAL.Models;
using System.Net;
using System.Net.Mail;

namespace ApplicationPL.Helpers
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email) 
        {
            //Get the Smtp server
            var client = new SmtpClient("smtp.gmail.com",587);
            //this will encrypt the request to the client server
            client.EnableSsl = true;
            //create your credintial
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ebrahemvirus@gmail.com", "obcvwaeapihtihry");
            //send the mail
            client.Send("ebrahemvirus@gmail.com", email.To, email.Subject, email.Body);

            

        } 
    }
}
