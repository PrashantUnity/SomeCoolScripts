internal class SendMail
{
    public static void Email()
    {
        try
        {
            MailMessage message = new()
            {
                From = new MailAddress("YourMail@mail.com"),
                Subject = "Test",
                IsBodyHtml = true,
                Body = $"Message/ or you can Include Html Page",
                ReplyTo = new MailAddress("TargetMail@mail.com"),
            };

            //message.To.Add(new MailAddress("mailadress"));


            SmtpClient smtp = new()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("YourMail@mail.com", "Token Password"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            for (int i = 0; i < 100; i++)
            {
                message.Body += i.ToString();
                smtp.Send(message);
                Console.WriteLine(i);
            }
        }
        catch (Exception e) { Console.WriteLine("failed" + e.Message); }
    }
}
