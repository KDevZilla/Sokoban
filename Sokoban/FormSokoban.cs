using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sokoban.SokobanMap;

namespace Sokoban
{
    public partial class FormSokoban : Form
    {
        /*
         * Credit 
         * Images::https://github.com/borgar/sokoban-skins
         * http://visual-sokoban.narod.ru/skins_1.htm
         */
        private Dictionary<Skin.SkinFactory.SkinType, BaseSkin> dicSkin = null;
        private BaseSkin GetSkin(String skin)
        {
            return GetSkin(Skin.SkinFactory.FromStringToSkinType(skin));
        }
        private  BaseSkin GetSkin(Skin.SkinFactory.SkinType skintype)
        {

            if(dicSkin == null)
            {
                dicSkin = new Dictionary<Skin.SkinFactory.SkinType, BaseSkin>();
            }
            if(!dicSkin.ContainsKey(skintype))
            {
                dicSkin.Add(skintype, Skin.SkinFactory.CreateSkin(skintype));
            }
            return dicSkin[skintype];
        }
        public FormSokoban()
        {
            InitializeComponent();
        }
        
        String Level =
@"#######
#.@ # #
#$* $ #
#   $ #
# ..  #
#  *  #
#######";

      
       

        SokobanMap sokoMap = null;
        private void FormSokoban_Load(object sender, EventArgs e)
        {
            /*
            Global.CurrentSettings.Skin = "Boxxle";
            Global.SaveSettings();
            */
            FormChooseMap f = new FormChooseMap();

            this.KeyPreview = true;
            pictureBox1.Paint += PictureBox1_Paint;
            this.KeyDown -= Form1_KeyDown;
            this.KeyDown += Form1_KeyDown;

            this.Level = f.GetMapByIndex(Global.CurrentSettings.CurrentMap);
            RestartGame();
          

        }
        private void RestartGame()
        {
            sokoMap = new SokobanMap(Level);
            this.pictureBox1.Height = sokoMap.Row * ElementWidth;
            this.pictureBox1.Width = sokoMap.Col * ElementWidth;
            this.pictureBox1.Invalidate();
            this.Width = this.pictureBox1.Left + this.pictureBox1.Width + 17;
            this.Height = this.pictureBox1.Top + this.pictureBox1.Height + 40;


        }
        int ElementWidth = 64;
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
            int i;
            int j;

             e.Graphics.Clear(Color.Black);
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            BaseSkin skin = GetSkin(Global.CurrentSettings.Skin);
            for (i = 0; i < sokoMap.Level2d.GetLength(0); i++)
            {
                //string line = LevelLines[i].Trim();
                for (j = 0; j < sokoMap.Level2d.GetLength(1); j++)
                {
                    //Level2d[i, j] = line.Substring (j,1);
                    Image img = null;
                    if (dicElemntType[sokoMap.Level2d[i, j]] == Sokoelement.worker ||
                        dicElemntType[sokoMap.Level2d[i, j]] == Sokoelement.worker_dock)
                    {

                        // img = GetImage(dicElemntType[sokoMap.Level2d[i, j]]);
                        img = skin.GetWorkderDirectionImage (sokoMap.WorkerCurrentDirection);
                    }
                    else
                    {
                        img = skin.GetElementImage(dicElemntType[sokoMap.Level2d[i, j]]);
                        //img = GetImage(dicElemntType[sokoMap.Level2d[i, j]]);
                    }
                    int offset = 0;
                    Rectangle rec = new Rectangle(j * ElementWidth , 
                        i * ElementWidth , 
                        ElementWidth , 
                        ElementWidth );
                    

                    
                    using (ImageAttributes wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                      
                        e.Graphics.DrawImage(img, rec, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                    
                   
                }
            }


        }
        private void RefreshImage()
        {
            this.pictureBox1.Invalidate();
        }
        private SokobanMap.Direction GetDirectionFromKey(Keys key)
        {
            if (key == Keys.Left) return SokobanMap.Direction.Left;
            if (key == Keys.Right) return SokobanMap.Direction.Right;
            if (key == Keys.Up ) return SokobanMap.Direction.Up ;
            if (key == Keys.Down) return SokobanMap.Direction.Down;

            throw new Exception($"{key} is invalid");

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // throw new NotImplementedException();


            if (e.KeyCode != Keys.Left &&
                 e.KeyCode != Keys.Right &&
                 e.KeyCode != Keys.Up &&
                 e.KeyCode != Keys.Down)
            {
                return;
            }

            if (sokoMap.IsSolve)
            {
                return;
            }
            //sokoMap.PlayerWalk(( e.KeyCode);
            sokoMap.PlayerWalk(GetDirectionFromKey(e.KeyCode));
            pictureBox1.Invalidate();

            if(sokoMap.IsSolve)
            {
                ShowWon();
            }

        }
        private void ShowWon()
        {
            //  MessageBox.Show("Finish");
            FormWon f = new FormWon();
            //f.TopLevel = false;
            //    f.Parent = this;
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
            if (f.DialogResult != DialogResult.OK)
            {
                return;
            }

            if (f.Decision == FormWon.UserDecision.ReplayTheMap)
            {
                RestartGame();
                return;
            }
            Global.GotoNextMap();
            Global.SaveSettings();

            FormChooseMap fChooseMap = new FormChooseMap();
            this.Level = fChooseMap.GetMapByIndex(Global.CurrentSettings.CurrentMap);
            RestartGame();
        }

        private Dictionary<String, Sokoelement> dicElemntType = new Dictionary<String, Sokoelement>()
        {
            {"$", Sokoelement.box  },
            {"*",Sokoelement.box_docx },
            {".",Sokoelement.dock },
            {" ",Sokoelement.floor },
            {"#",Sokoelement.wall },
            {"@",Sokoelement.worker },
            {"+",Sokoelement.worker_dock },

        };
    

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RestartGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sokoMap.Undo();
            this.pictureBox1.Invalidate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormChooseMap f = new FormChooseMap();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            if(f.DialogResult != DialogResult.OK)
            {
                return;
            }
            Global.CurrentSettings.CurrentMap = f.MapIndexSelected;
            Global.SaveSettings();

            this.Level = f.SelectedMap;
            this.RestartGame();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormChooseSkin f = new FormChooseSkin();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            if (f.DialogResult != DialogResult.OK)
            {
                return;
            }
            Global.CurrentSettings.Skin = f.ChoosenSkin;
            Global.SaveSettings();
            this.pictureBox1.Invalidate();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout f = new FormAbout();
           // f.Parent = this;
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();

        }
    }
}
