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
    public partial class FormChooseMap : Form
    {
        public FormChooseMap()
        {
            InitializeComponent();
        }
        Dictionary<int, String> dicStage = new Dictionary<int, string>();
        //Credit
        /*
         * Marti Homs Caussa
         * https://www.sokobanonline.com/play/web-archive/marti-homs-caussa/
         * http://www.sourcecode.se/sokoban/levels
         */
        private void FormChooseMap_Load(object sender, EventArgs e)
        {

            dicStage = new Dictionary<int, string>();
            dicStage.Add(0,
@"
#######
#.@ # #
#$* $       #
#   $ #
# ..  #
#  *  #
#######
");
            dicStage.Add(1,
                @"
 #####
 #.. #
###  #
# $  #
# $  #
#@  #
#####");

            dicStage.Add(2,
                @"
  ##### 
###   #   
#. @$ #
### $.#
#.##$ #
# # . ##
#$ *$$.#
#   .  #
########
");
            dicStage.Add(3,
@"
  ####
  #  #
  #  #
###  ###
# $*.@ #
#      #
########");

            dicStage.Add(4,
@"
###    
#.#####
#..   #
# $$$@#
#    ##
######
");
            dicStage.Add(5,
@"
######
#....####
#  $##  #
# $$    #
#@$ #   #
##  #####
 ####");

            dicStage.Add(6,
@"
########
#  *   #
#  $.  #
### @###
  #  #
  ####");
            dicStage.Add(7,
@"
  #####
  #   #
### # #
# $$.@#
# .  ##
##  ##
 #  #
 ####");

            int i;
            for (i = 0; i < dicStage.Count; i++)
            {
                this.comboBox1.Items.Add((i + 1));
            }
            this.comboBox1.SelectedIndex = 0;
        }
        public string SelectedMap { get; private set; } = "";
        private void btnChoose_Click(object sender, EventArgs e)
        {
            int index = this.comboBox1.SelectedIndex;
            SelectedMap = dicStage[index];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
