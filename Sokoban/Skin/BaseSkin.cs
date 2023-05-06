using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.SokobanMap;
using System.Drawing;
namespace Sokoban
{
    public abstract class BaseSkin
    {

        bool hasSetImage = false;
        bool hasSetImageSize = false;
        public int ImageSize { get; private set; } = 16;
        protected void SetImageSize (int imageSize)
        {
            this.ImageSize = imageSize;
            hasSetImageSize = true;
        }
        protected void SetImageFileName(
             String box,
             String box_docx,
             String dock,
             String floor,
             String wall,
             String workerLeft,
             String workerRight,
             String workerUp,
             String workerdown)
        {

            dicImage = new Dictionary<Sokoelement, Image>()
            {
                 {Sokoelement.box,  Image.FromFile(Util.FileUtil.ImageFolderPath +  box) },
            {Sokoelement.box_docx,Image.FromFile(Util.FileUtil.ImageFolderPath +  box_docx) },
            {Sokoelement.dock,Image.FromFile(Util.FileUtil.ImageFolderPath +  dock) },
            {Sokoelement.floor,Image.FromFile(Util.FileUtil.ImageFolderPath +  floor) },
            {Sokoelement.wall,Image.FromFile(Util.FileUtil.ImageFolderPath +  wall)},

            //{Sokoelement.worker,Image.FromFile(Util.FileUtil.ImageFolderPath +  box) },
            //{Sokoelement.worker_dock,Image.FromFile(Util.FileUtil.ImageFolderPath +  box) },
            };
            dicImageDirection = new Dictionary<Direction, Image>()
            {
                 {SokobanMap.Direction.Up ,Image.FromFile(Util.FileUtil.ImageFolderPath +  workerUp)},
                 {SokobanMap.Direction.Down  ,Image.FromFile(Util.FileUtil.ImageFolderPath +  workerdown) },
                {SokobanMap.Direction.Left  ,Image.FromFile(Util.FileUtil.ImageFolderPath +  workerLeft) },
                {SokobanMap.Direction.Right  ,Image.FromFile(Util.FileUtil.ImageFolderPath +  workerRight) },
            };

            hasSetImage = true;
        }
        /*
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
        */

        public System.Drawing.Image GetElementImage(Sokoelement element)
        {
            if (!hasSetImage || !hasSetImageSize )
            {
                throw new Exception("Please call SetImage() and SetImageSize() first");
            }
            /*
            if (!dicImage.ContainsKey(element))
            {
                String fileName = Util.FileUtil.ImageFolderPath + dicImageFileName[element];

                System.Drawing.Image img = Image.FromFile(fileName);
                dicImage.Add(element, img);
            }
            */
            return dicImage[element];
        }
        public System.Drawing.Image GetWorkderDirectionImage(SokobanMap.Direction direction)
        {
            if (!hasSetImage || !hasSetImageSize)
            {
                throw new Exception("Please call SetImage() and SetImageSize() first");
            }
            /*
            if (!dicImageDirection.ContainsKey(direction))
            {
                String fileName = Util.FileUtil.ImageFolderPath + dicImageDirectionFileName[direction];

                System.Drawing.Image img = Image.FromFile(fileName);
                dicImageDirection.Add(direction, img);
            }
            */
            return dicImageDirection[direction];
        }


        // private Dictionary<Sokoelement, Image> _dicImage = null;
        public Dictionary<Sokoelement, Image> dicImage = new Dictionary<Sokoelement, Image>();

        private Dictionary<SokobanMap.Direction, Image> dicImageDirection = new Dictionary<Direction, Image>();
        /*
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
        */
    }
}
