using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Skin
{
    class ChocobanSkin : BaseSkin
    {
        public ChocobanSkin()
        {

            String folderName = "Chocoban";
            String box = $@"{folderName}\object.bmp";
            String box_docx = $@"{folderName}\object_store.bmp";
            String dock = $@"{folderName}\store.bmp";
            String floor = $@"{folderName}\ground.bmp";
            String wall = $@"{folderName}\wall.bmp";
            String worker_left = $@"{folderName}\mover.bmp";
            String worker_right = $@"{folderName}\mover.bmp";
            String worker_up = $@"{folderName}\mover.bmp";
            String worker_down = $@"{folderName}\mover.bmp";

            this.SetImageFileName(box,
                box_docx,
                dock,
                floor,
                wall,
                worker_left,
                worker_right,
                worker_up,
                worker_down
                );

            this.SetImageSize(25);
        }
    }
   
}
