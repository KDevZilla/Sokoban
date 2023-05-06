using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class FormChooseSkin : Form
    {
        public FormChooseSkin()
        {
            InitializeComponent();
        }
        public string ChoosenSkin { get; private set; } = Global.CurrentSettings.Skin;
        private void btnChoose_Click(object sender, EventArgs e)
        {

            this.ChoosenSkin = this.comboSkin.Items[comboSkin.SelectedIndex].ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void FormChooseSkin_Load(object sender, EventArgs e)
        {
            this.comboSkin.SelectedIndex = 0;

        }
    }
}
