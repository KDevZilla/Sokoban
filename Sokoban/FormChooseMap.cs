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
         private void LoadDicStage()
        {
            dicStage = new Dictionary<int, string>();
            dicStage.Add(0,
@"
#######
#.@ # #
#$* $ #
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
            dicStage.Add(8,
@"
  ####  
  #  #  
###  ###
# $$ @ #
#    ..#
########");

            dicStage.Add(9,
@"
####    
#  #### 
#  #  # 
#  . $##
## $ .@#
 ### # #
  #    #
  #   ##
  #####");

            dicStage.Add(10,
@"
####    
#  #####
# .  $ #
# $  .@#
###  ###
  ####");


            dicStage.Add(11,
@"
#####    
#   #####
# .  $  #
##$##.@ #
#     ###
#     #  
#######  
");

            dicStage.Add(12,
@"
####    
#  #####
# .  $ #
# $  .@#
###  ###
  ####  
");
            dicStage.Add(13,
@"
#####  
#   #  
# # ###
#  $$ #
###+# #
#   . #
# # ###
#   #
#####
");
        }
        public string GetMapByIndex(int index)
        {
            LoadDicStage();
            return dicStage[index];
        }
        private void FormChooseMap_Load(object sender, EventArgs e)
        {

            LoadDicStage();
            int i;
            for (i = 0; i < dicStage.Count; i++)
            {
                this.comboBox1.Items.Add((i + 1));
            }
            this.comboBox1.SelectedIndex =Global.CurrentSettings.CurrentMap ;
        }
        public string SelectedMap { get; private set; } = "";
        public int MapIndexSelected { get; private set; } = 0;
        private void btnChoose_Click(object sender, EventArgs e)
        {
            int index = this.comboBox1.SelectedIndex;
            SelectedMap = dicStage[index];
            MapIndexSelected = index;
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
