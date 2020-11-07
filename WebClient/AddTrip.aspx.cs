using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class AddTrip : System.Web.UI.Page
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
                DropDownListCars.DataSource = service.GetSelectData("cars");
                DropDownListCars.DataTextField = "name";
                DropDownListCars.DataValueField = "id";
                DropDownListCars.DataBind();

                DropDownListObjects1.DataSource = service.GetSelectData("objects");
                DropDownListObjects1.DataTextField = "name";
                DropDownListObjects1.DataValueField = "id";
                DropDownListObjects1.DataBind();

                DropDownListObjects2.DataSource = service.GetSelectData("objects");
                DropDownListObjects2.DataTextField = "name";
                DropDownListObjects2.DataValueField = "id";
                DropDownListObjects2.DataBind();
            }

            DateLabel.Visible = false;
            ObjLabel.Visible = false;
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            var car_id = int.Parse(DropDownListCars.SelectedValue);
            var obj_from_id = int.Parse(DropDownListObjects1.SelectedValue);
            var obj_to_id = int.Parse(DropDownListObjects2.SelectedValue);
            var date_from = dateFrom.Text;
            var date_to = dateTo.Text;

            if (!string.IsNullOrEmpty(date_from) && !string.IsNullOrEmpty(date_to) && DateTime.Parse(date_from) >= DateTime.Parse(date_to))
            {
                DateLabel.Visible = true;
            }
            if (obj_from_id == obj_to_id)
            {
                ObjLabel.Visible = true;
            }
            var res = service.Insert(car_id, obj_from_id, obj_to_id, date_from, date_to);

            if (res) Response.Redirect("/TripList.aspx");

        }
    }
}