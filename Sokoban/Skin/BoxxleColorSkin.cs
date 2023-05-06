using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Skin
{
    class BoxxleColorSkin : BaseSkin
    {
        public BoxxleColorSkin()
        {

            String folderName = @"boxxle_color\";

            String box = $@"{folderName}object.bmp";
            String box_docx = $@"{folderName}object_store.bmp";
            String dock = $@"{folderName}store.bmp";
            String floor = $@"{folderName}floor.bmp";
            String wall = $@"{folderName}wall.bmp";
            String worker_left = $@"{folderName}mover_left.bmp";
            String worker_right = $@"{folderName}mover_right.bmp";
            String worker_up = $@"{folderName}mover_up.bmp";
            String worker_down = $@"{folderName}mover_down.bmp";

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

            this.SetImageSize(16);
        }
    }
}

   