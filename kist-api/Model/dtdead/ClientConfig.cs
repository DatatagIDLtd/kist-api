using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ceup_api.Model.dtdead
{
	public class ClientConfig
	{
		public ClientConfig()
		{
			APIFields = new Dictionary<string, string>();
		}

		#region Mandatory API Fields
		public string ObjectName { get; set; }
		public string ContentType { get; set; }
		public string Endpoint { get; set; }
		public string HttpMethod { get; set; }
		public string APIUserName { get; set; }
		public string APIPassword { get; set; }
		public string APIToken { get; set; }
		#endregion Mandatory API Fields

		//Optional List for Generic API functionality.
		public Dictionary<string, string> APIFields { get; set; }

		//EventData Mandatory Fields
		public string Destination { get; set; }
		public string DatabaseName { get; set; }
		public int EventAdviceID { get; set; }
		public int EventSourceID { get; set; }
		public int EventTypeID { get; set; }
		public string FieldData { get; set; }
		public string KeyName { get; set; }
		public string KeyValue { get; set; }
		public string MembershipType { get; set; }
		public bool Mute { get; set; }
		public string OperatorID { get; set; }
		public string RecordID { get; set; }
		public string SnoozeUntil { get; set; }
		public string TableField { get; set; }
		public string TableName { get; set; }
		public string EmailRecipient { get; set; }
		public string EmailRegex { get; set; }
		public string UserName { get; set; }
		public string CreatedBy { get; set; }
		public string Report { get; set; }
	}
}
