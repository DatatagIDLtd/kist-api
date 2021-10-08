using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ceup_api.Model.dtdead
{
	public class EventData
	{
		public string CreatedBy { get; set; }
		public string DatabaseName { get; set; }
		public int EventAdviceID { get; set; }
		public int EventSourceID { get; set; }
		public int EventTypeID { get; set; }
		public string FieldData { get; set; }
		public string KeyName { get; set; }
		public string KeyValue { get; set; }
		public string MembershipType { get; set; }
		public string Mute { get; set; }
		public string OperatorID { get; set; }
		public string RecordID { get; set; }
		public string Report { get; set; }
		public string SnoozeUntil { get; set; }
		public string TableField { get; set; }
		public string TableName { get; set; }
		public string UserName { get; set; }
		public string EmailRecipient { get; set; }
		//public string ServiceName { get; set; }
	}

}
