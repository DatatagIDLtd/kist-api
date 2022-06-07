using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace kist_api.Model
{
    public class AssetView : Asset
    {

        public string assetType { get; set; }
        public string assetStatus { get; set; }
        public string contract { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string fleetNo { get; set; }
        public string label { get; set; }
        public Boolean disabled { get; set; }

        public string? uniqueID { get; set; }
        public string? allocationStatus { get; set; }
        public string? allocationId { get; set; }
        public DateTime? effectiveFrom { get; set; }
        public DateTime? effectiveTo { get; set; }
        public string? imgUrl { get; set; }
        public string allocation { get; set; }
        public string policeInterest { get; set; }
        public string image { get; set; }
        public string memId { get; set; }
        public string partNo { get; set; }
        public string serialNumber { get; set; }
        public string status { get; set; }
        public string vin { get; set; }
        public string vrn { get; set; }
        public string enginNo { get; set; }
        public string lastScandData { get; set; }
        public string allocatedTo { get; set; }
        public string allocatedToType { get; set; }

        public string allocationAuditsJson { get; set; }

        public List<AssetAllocationAudit> allocationAudits
        {
            get
            {
                if (!string.IsNullOrEmpty(allocationAuditsJson))
                {
                    return JsonConvert.DeserializeObject<List<AssetAllocationAudit>>(allocationAuditsJson);
                }
                else
                {
                    return new List<AssetAllocationAudit>();
                }
            }
        }

        //public long allocationdAuditId { get; set; }
        //public int allocationAuditStatusId { get; set; }
        //public int auditId { get; set; }
        //public int assetStatusId { get; set; }
        //public DateTime checkedOn { get; set; }
        //public string checkedBy { get; set; }
        //public string assignTo { get; set; }
        //public int siteId { get; set; }
        //public string notes { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string showTabs { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public AssetAllocationAudit selectedAllocationAudit { get; set; }
    }


    public class AssetAllocationAudit
    {
        public long allocationdAuditId { get; set; }
        public int allocationAuditStatusId { get; set; }
        public int auditId { get; set; }
        public int assetStatusId { get; set; }
        public DateTime checkedOn { get; set; }
        public string checkedBy { get; set; }
        public string assignTo { get; set; }
        public int siteId { get; set; }
        public string notes { get; set; }
    }
}
