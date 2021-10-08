using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ceup_api.Model.dtdead
{
	public class Enums
	{
		public enum httpVerb
		{
			GET,
			POST,
			PUT,
			DELETE
		}

		public enum authenticationType
		{
			Basic,
			NTLM
		}

		public enum authenticationTechnique
		{
			Own,
			NetworkCredential
		}
	}
}
