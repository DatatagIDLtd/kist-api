using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model

{


	public class SetAllocationAuditRequest
    {
		//@userId as bigint , @operatorid as bigint , @assetid as bigint , @auditId bigint, @status as bigint ,@allocationAuditId bigint @ note//
		//	@userId as bigint , @operatorid as bigint , @assetid as bigint , @auditType nvarchar(30) ,@auditDate as datetime
		public long? userid { get; set; }
		public long? operatorId { get; set; }
		public long? assetId { get; set; }
		public long? auditId { get; set; }
		public long? status { get; set; }
		public long? allocationAuditId { get; set; }

		public string note { get; set; }
	


		public SetAllocationAuditRequest()
		{
			note = "";
			
		}

		//@OperatorRef = N'CL1',
		//@Name = N'New Gang',
		//@OemRef = NULL,
		//@ModelRef = NULL,
		//@Colour = NULL,
		//@Type = N'Gang',
		//@Status = N'Active',
		//@Description = N'New Gang',
		//@PurchaseDate = NULL,
		//@SerialNumber = NULL,
		//@VRN = NULL,
		//@EngineNumber = NULL,
		//@VIN_Chassis = NULL,
		//@UniqueReference = NULL,
		//@ParentAssetId = NULL,
		//@DTIdNumber = NULL,
		//@DTMembershipNumber = NULL



	}
}
