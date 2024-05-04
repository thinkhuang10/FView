using DevExpress.XtraVerticalGrid.Events;
using Model;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment.TagManager
{
    public partial class TagPropertyForm : DevExpress.XtraEditors.XtraForm
    {
        public object SelectedObject;

        public TagPropertyForm(object obj)
        {
            InitializeComponent();

            propertyGrid.SelectedObject = obj;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            SelectedObject = propertyGrid.SelectedObject;

            DialogResult = DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TagProperty_Load(object sender, EventArgs e)
        {
            propertyGrid.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
        }

        private void PropertyGrid_CustomPropertyDescriptors(object sender, CustomPropertyDescriptorsEventArgs e)
        {
            if (propertyGrid.SelectedObject is OPCDATagInfo)
            {
                e.Properties = e.Properties.Sort(new string[] { "Name", "DeviceName", "TagType", "Value" });
            }
        }
    }
}