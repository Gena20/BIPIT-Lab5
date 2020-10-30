using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        ServiceReference1.Service1Client service;

        public Form1()
        {
            InitializeComponent();

            service = new ServiceReference1.Service1Client();
        }

        private void FillComboBox(ComboBox comboBox, string tabelName)
        {
            comboBox.DataSource = service.GetSelectData(tabelName);
            comboBox.DisplayMember = "name";
            comboBox.ValueMember = "id";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvTrips.DataSource = service.GetData();
            FillComboBox(cbCar, "cars");
            FillComboBox(cbObjFrom, "objects");
            FillComboBox(cbObjTo, "objects");
        }

        private bool ValidateFields()
        {
            if (dateFrom.Value >= dateTo.Value)
            {
                MessageBox.Show("dateTo must be more than dateFrom");
                return false;
            }
            if ((int)cbObjFrom.SelectedValue == (int)cbObjTo.SelectedValue)
            {
                MessageBox.Show("objTo and objFrom must be different");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (! ValidateFields()) return;
            service.Insert(
                (int)cbCar.SelectedValue,
                (int)cbObjFrom.SelectedValue,
                (int)cbObjTo.SelectedValue,
                dateFrom.Value.ToString("yyyy-MM-dd"),
                dateTo.Value.ToString("yyyy-MM-dd")
            );

            Form1_Load(null, null);
        }
    }
}
