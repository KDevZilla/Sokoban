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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Point CurrentPositionPlayer = new Point(2, 1);
        private enum Direction
        {
            Up=Keys.Up,
            Down=Keys.Down,
            Left=Keys.Left ,
            Right=Keys.Right
        };
        Dictionary<Direction, Point> dicDirectionPoint = new Dictionary<Direction, Point>()
        {
            {Direction.Up,new Point (0,-1) },
            {Direction.Down ,new Point (0,1) },
            {Direction.Left ,new Point (-1,0) },
            {Direction.Right ,new Point (1,0) },

        };
        private void RefreshImage()
        {
            this.pictureBox1.Invalidate();
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
            MovePlayer((Direction)e.KeyCode);


        }
        private void MovePlayer(Direction direction)
        {
            Level2d[CurrentPositionPlayer.Y, CurrentPositionPlayer.X] = " ";
            Point pointDelta = dicDirectionPoint[direction];
            CurrentPositionPlayer.X += pointDelta.X;
            CurrentPositionPlayer.Y += pointDelta.Y;
            Level2d[CurrentPositionPlayer.Y, CurrentPositionPlayer.X] = "@";
            RefreshImage();
        }
        private void SetPlayeronLevel(Point PlayerPosition)
        {
           
        }
        String Level =
@"#######
#.@ # #
#$* $ #
#   $ #
# ..  #
#  *  #
#######";
        String[,] Level2d = null;
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public enum ImageType
        {
            box,
            box_docx,
            dock,
            floor,
            wall,
            worker,
            worker_doced
        }
        private Dictionary<ImageType, Image> _dicImage = null;
        public Dictionary<ImageType, Image> dicImage
        {
            get
            {
                if(_dicImage == null)
                {
                    _dicImage = new Dictionary<ImageType, Image>();
                }
                return _dicImage;
            }
        }
        private Dictionary<ImageType, String> dicImageFileName = new Dictionary<ImageType, string>()
        {
            {ImageType.box,@"yoshi-32-box.png" },
            {ImageType.box_docx,@"yoshi-32-box-docked.png" },
            {ImageType.dock,@"yoshi-32-dock.png" },
            {ImageType.floor,@"yoshi-32-floor.png" },
            {ImageType.wall,@"yoshi-32-wall.png" },
            {ImageType.worker,@"yoshi-32-worker.png" },
            {ImageType.worker_doced,@"yoshi-32-worker-docked.png" },

        };

        private Dictionary<String, ImageType> dicElemntImageType = new Dictionary<String, ImageType>()
        {
            {"$", ImageType.box  },
            {"*",ImageType.box_docx },
            {".",ImageType.dock },
            {" ",ImageType.floor },
            {"#",ImageType.wall },
            {"@",ImageType.worker },
            {"+",ImageType.worker_doced },

        };
        private Image GetImage(ImageType imgtype)
        {
            if (!dicImage.ContainsKey(imgtype))
            {
                String fileName = Util.FileUtil.ImageFolderPath + dicImageFileName[imgtype];

                System.Drawing.Image img = Image.FromFile(fileName);
                dicImage.Add(imgtype, img);
            }
            return dicImage[imgtype];
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            pictureBox1.Paint += PictureBox1_Paint;
            this.KeyDown -= Form1_KeyDown;
            this.KeyDown += Form1_KeyDown;

            String[] LevelLines = Level.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Level2d = new string[LevelLines.Length, LevelLines[0].Length];
            int i, j = 0;
            for (i = 0; i < LevelLines.Length; i++)
            {
                string line = LevelLines[i].Trim();
                for (j = 0; j < line.Length; j++)
                {
                    Level2d[i, j] = line.Substring(j, 1);
                }
            }
        }



        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // throw new NotImplementedException();
            int i;
            int j;
            int ElementWidth = 80;
          //  String[] LevelLines = Level.Split(Environment.NewLine.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
          //  Level2d = new string[LevelLines.Length, LevelLines[0].Length];
           
            for (i = 0; i < Level2d.GetLength (0) ; i++)
            {
                //string line = LevelLines[i].Trim();
                for (j = 0; j < Level2d.GetLength (1); j++)
                {
                    //Level2d[i, j] = line.Substring (j,1);

                    Image img = GetImage(dicElemntImageType[Level2d[i,j]]);
                    Rectangle rec = new Rectangle(j * ElementWidth -1, i * ElementWidth -1, ElementWidth+2, ElementWidth+2);
                    e.Graphics.DrawImage(img, rec);
                }
            }


        }
    }
}
