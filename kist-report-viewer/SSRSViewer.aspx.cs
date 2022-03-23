using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kist_report_viewer
{
    public partial class SSRSViewer : System.Web.UI.Page
    {
        public class CustomReportCredentials : IReportServerCredentials
        {
            private readonly string userName;
            private readonly string password;
            private readonly string domainName;

            public CustomReportCredentials(string userName, string password, string domainName)
            {
                this.userName = userName;
                this.password = password;
                this.domainName = domainName;
            }

            public WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(userName, password, domainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var reportUrl = Request.QueryString["reportUrl"];

                try
                {
                    if (!string.IsNullOrEmpty(reportUrl))
                    {
                        var operatorId = Request.QueryString["operatorId"];
                        var userName = Request.QueryString["userName"];
                        var showToolBar = Request.QueryString["toolbar"];

                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SSRSUsername"]))
                        {
                            IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["SSRSUsername"], ConfigurationManager.AppSettings["SSRSPassword"], "");
                            ssrsViewer.ServerReport.ReportServerCredentials = irsc;
                        }

                        ssrsViewer.ProcessingMode = ProcessingMode.Remote;
                        ssrsViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["SSRSReportsServerUrl"]);
                        ssrsViewer.ServerReport.ReportPath = $"{reportUrl}";
                        ssrsViewer.ShowToolBar = true;
                        ssrsViewer.ShowParameterPrompts = true;

                        if (!string.IsNullOrEmpty(userName))
                        {
                            var parameters = new List<ReportParameter>();
                            var userNameParameter = new ReportParameter("Username", userName, false);
                            parameters.Add(userNameParameter);
                            ssrsViewer.ServerReport.SetParameters(parameters);
                        }

                        ssrsViewer.ServerReport.Refresh();
                    }

                }
                catch (Exception ex)
                {

                }
            }

        }


        
    }
}