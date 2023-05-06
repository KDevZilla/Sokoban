using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Skin
{
    public class YoshiSkin:BaseSkin
    {
        public YoshiSkin()
        {
            String folderName = $@"yoshi32\";

            String box = $@"{folderName}yoshi-32-box.png";
            String box_docx = $@"{folderName}yoshi-32-box-docked.png";
            String dock = $@"{folderName}yoshi-32-dock.png";
            String floor = $@"{folderName}yoshi-32-floor.png";
            String wall = $@"{folderName}yoshi-32-wall.png";
            String worker_left = $@"{folderName}yoshi-32-worker.png";
            String worker_right = $@"{folderName}yoshi-32-worker.png";
            String worker_up = $@"{folderName}yoshi-32-worker.png";
            String worker_down = $@"{folderName}yoshi-32-worker.png";

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

            this.SetImageSize(32);
        }
    }
}
