#region Using Directives

using System;
using System.Configuration;

#endregion

namespace Configuration
{
	public class VsConfig
	{
		private static readonly Lazy<VsConfig> Instance = new Lazy<VsConfig>(() => new VsConfig());
		public static VsConfig I => Instance.Value;

        public const string PEOPLE_DEFAULT_NAME = "Name";
        public const string MONEY_FORMAT = "C2";
        public const decimal DEFAULT_VAT_RATE = 0.24m;
        public decimal RERORT_AMOUNT = 20;
        public const long PHOTO_SIZE_BOTTOM_LIMIT = 100;
        public const long PHOTO_SIZE_TOP_LIMIT = 350000;
        public const string PHOTO_SIZE_BOTTOM_LIMIT_TEXT = "100 bytes";
        public const string PHOTO_SIZE_TOP_LIMIT_TEXT = "350 KB";

        public bool EnableSslEmail { get; }
		public string MailSender { get; }
		public string SmtpClient { get; }
		public string SmtpPassword { get; }
		public int SmtpPort { get; }
		public string SmtpUserName { get; }
		public string DocumentsLocalPath { get; }
		public string ErrorMailTo { get; }
		public string PhotosLocalPath { get; set; }
        public int GreeceId = 1;
        public string GreeceName = "Greece";


        public static string WebAppUrl; 
        //readonly
		private VsConfig()
		{
			this.ErrorMailTo = ConfigurationManager.AppSettings["ErrorMailTo"];
			this.MailSender = ConfigurationManager.AppSettings["MailSender"];
			this.SmtpClient = ConfigurationManager.AppSettings["SMTPClient"];
			this.SmtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
			this.SmtpUserName = ConfigurationManager.AppSettings["SMTPUserName"];
			this.SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
			this.EnableSslEmail = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSslEmail"]);
            WebAppUrl = ConfigurationManager.AppSettings["WebAppUrl"];

            this.DocumentsLocalPath = "App_Data\\Documents\\";
			this.PhotosLocalPath = "UploadedPhotos\\";
		}
	}
}