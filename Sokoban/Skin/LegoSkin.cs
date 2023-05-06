using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Skin
{
    class LegoSkin : BaseSkin
    {
        public LegoSkin()
        {

            String folderName = "Lego";
            String box = $@"{folderName}\Lego_object.bmp";
            String box_docx = $@"{folderName}\Lego_object_store.bmp";
            String dock = $@"{folderName}\Lego_store.bmp";
            String floor = $@"{folderName}\Lego_ground.bmp";
            String wall = $@"{folderName}\Lego_wall.bmp";
            String worker_left = $@"{folderName}\Lego_mover.bmp";
            String worker_right = $@"{folderName}\Lego_mover.bmp";
            String worker_up = $@"{folderName}\Lego_mover.bmp";
            String worker_down = $@"{folderName}\Lego_mover.bmp";

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
