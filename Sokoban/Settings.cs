using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    [Serializable]
    public class Settings
    {
        public string Skin { get; set; } = "Boxxle";
        public int CurrentMap { get; set; } = 1;
       

    }
}
