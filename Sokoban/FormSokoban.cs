using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
         * 
         */
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
            this.KeyPreview = true;
            pictureBox1.Paint += PictureBox1_Paint;
            this.KeyDown -= Form1_KeyDown;
            this.KeyDown += Form1_KeyDown;
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
            // throw new NotImplementedException();
            int i;
            int j;

            //  String[] LevelLines = Level.Split(Environment.NewLine.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
            //  Level2d = new string[LevelLines.Length, LevelLines[0].Length];

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
                        img = GetImageWorkerDirection(sokoMap.WorkerCurrentDirection);
                    }
                    else
                    {

                        img = GetImage(dicElemntType[sokoMap.Level2d[i, j]]);
                    }
                    int offset = 2;
                    Rectangle rec = new Rectangle(j * ElementWidth - offset, i * ElementWidth - offset, ElementWidth + offset * 2, ElementWidth + offset * 2);
                    e.Graphics.DrawImage(img, rec);
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
                MessageBox.Show("Finish");
            }

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
        private Image GetImage(Sokoelement imgtype)
        {
            if (!dicImage.ContainsKey(imgtype))
            {
                String fileName = Util.FileUtil.ImageFolderPath + dicImageFileName[imgtype];

                System.Drawing.Image img = Image.FromFile(fileName);
                dicImage.Add(imgtype, img);
            }
            return dicImage[imgtype];
        }
        private Image GetImageWorkerDirection(SokobanMap.Direction direction)
        {
            if (!dicImageDirection.ContainsKey(direction))
            {
                String fileName = Util.FileUtil.ImageFolderPath + dicImageDirectionFileName[direction];

                System.Drawing.Image img = Image.FromFile(fileName);
                dicImageDirection.Add(direction, img);
            }
            return dicImageDirection[direction];
        }
        private Dictionary<Sokoelement, Image> _dicImage = null;
        public Dictionary<Sokoelement, Image> dicImage
        {
            get
            {
                if (_dicImage == null)
                {
                    _dicImage = new Dictionary<Sokoelement, Image>();
                }
                return _dicImage;
            }
        }
        private Dictionary<SokobanMap.Direction, Image> _dicImageDirection = null;
        public Dictionary<SokobanMap.Direction, Image> dicImageDirection
        {
            get
            {
                if (_dicImageDirection == null)
                {
                    _dicImageDirection = new Dictionary<SokobanMap.Direction, Image>();
                }
                return _dicImageDirection;
            }
        }
        /*
        private Dictionary<Sokoelement, String> dicImageFileName = new Dictionary<Sokoelement, string>()
        {
            {Sokoelement.box,@"yoshi-32-box.png" },
            {Sokoelement.box_docx,@"yoshi-32-box-docked.png" },
            {Sokoelement.dock,@"yoshi-32-dock.png" },
            {Sokoelement.floor,@"yoshi-32-floor.png" },
            {Sokoelement.wall,@"yoshi-32-wall.png" },
            {Sokoelement.worker,@"yoshi-32-worker.png" },
            {Sokoelement.worker_dock,@"yoshi-32-worker-docked.png" },

        };
        */
        private Dictionary<Sokoelement, String> dicImageFileName = new Dictionary<Sokoelement, string>()
        {
            {Sokoelement.box,@"boxxle\object.bmp" },
            {Sokoelement.box_docx,@"boxxle\object_store.bmp" },
            {Sokoelement.dock,@"boxxle\store.bmp" },
            {Sokoelement.floor,@"boxxle\floor.bmp" },
            {Sokoelement.wall,@"boxxle\wall.bmp" },
            {Sokoelement.worker,@"boxxle\mover_down.bmp" },
            {Sokoelement.worker_dock,@"boxxle\mover_down.bmp" },

        };

        private Dictionary<SokobanMap.Direction, String> dicImageDirectionFileName = new Dictionary<SokobanMap.Direction, string>()
        {
            {SokobanMap.Direction.Up ,@"boxxle\mover_up.bmp" },
            {SokobanMap.Direction.Down  ,@"boxxle\mover_down.bmp" },
            {SokobanMap.Direction.Left  ,@"boxxle\mover_left.bmp" },
            {SokobanMap.Direction.Right  ,@"boxxle\mover_right.bmp" },


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

            this.Level = f.SelectedMap;
            this.RestartGame();

        }
    }
}
