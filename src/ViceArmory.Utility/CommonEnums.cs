namespace ViceArmory.Utility
{
    public class CommonEnums
    {
        public enum ConfigKeys
        {
            WebApiBaseUrl, //Web Api Base url address string to be picked from config.
            AttachmentLocation, // Attachment location for email attachments when queued, string to be picked from config.
            SendEmailRetryCount, // retry count while sending email, it is the count of attempts that will be made to send an email, int to be picked from config.
            DefaultExceptionPolicy, // Default exception policy to be picked from config.
            EmailServiceProvider, // This comprises of the third party emailing service provider. i.e. currently we are using sendgrid emailing service.
            SendgridApiKey, // Sendgrid api key which serves as credential to send email using sendgrid server.
            FromEmailId,
            EncryptionKey // Encryptin key
        }
    }
}
