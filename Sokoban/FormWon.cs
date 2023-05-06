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
    public partial class FormWon : Form
    {
        public FormWon()
        {
            InitializeComponent();
        }

        private void FormWon_Load(object sender, EventArgs e)
        {
            this.lblMessage.Text =$"You solved the map {Global.CurrentSettings.CurrentMap+1}";

        }
        public enum UserDecision
        {
            ReplayTheMap,
            GoToTheNextMap
        }
        public UserDecision Decision { get; private set; } = UserDecision.ReplayTheMap;
        private void btnChoose_Click(object sender, EventArgs e)
        {
            this.Decision = UserDecision.ReplayTheMap;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Decision = UserDecision.GoToTheNextMap;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
