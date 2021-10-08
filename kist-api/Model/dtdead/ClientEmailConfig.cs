using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ceup_api.Model.dtdead
{
	public class ClientEmailConfig
	{
		public ClientEmailConfig()
		{
			IsBodyHtml = false;
			EnableSSL = false;
			UseDefaultCredentials = false;
		}

		public string From { get; set; }
		public string Subject { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpHost { get; set; }
		public bool EnableSSL { get; set; }
		public bool UseDefaultCredentials { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string EmailRegEx { get; set; }
		public string EmailRecipient { get; set; }
		public bool IsBodyHtml { get; set; }
	}
}
