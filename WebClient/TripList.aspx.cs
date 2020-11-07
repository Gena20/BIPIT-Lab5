using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class TripList : System.Web.UI.Page
    {
        ServiceReference1.Service1Client service;
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            { 
                service = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
                Uri uri = new Uri("http://localhost:8733/Design_Time_Addresses/Service/Service1/");
                ServiceHost host = new ServiceHost(service, uri);

                service.PrintConnectionInfo(
                    host.Description.Name,
                    host.BaseAddresses[0].Port.ToString(),
                    host.BaseAddresses[0].LocalPath,
                    host.BaseAddresses[0].AbsoluteUri,
                    host.BaseAddresses[0].Scheme
                );
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{ex.Message}')", true);
                return;
            }

            if (!this.IsPostBack)
            {
                GridView1.DataSource = service.GetData();
                GridView1.DataBind();
            }
        }
    }
}