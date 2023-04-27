using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class SokobanMap
    {
        String Level =
@"#######
#.@ # #
#$* $ #
#   $ #
# ..  #
#  *  #
#######";
        String[,] Level2d = null;
        int Row { get; set; } = 0;
        int Col { get; set; } = 0;
        public enum Sokoelement
        {
            box,
            box_docx,
            dock,
            floor,
            wall,
            worker,
            worker_doced
        }

        private Dictionary<String, Sokoelement> dicElemntType = new Dictionary<String, Sokoelement>()
        {
            {"$", Sokoelement.box  },
            {"*",Sokoelement.box_docx },
            {".",Sokoelement.dock },
            {" ",Sokoelement.floor },
            {"#",Sokoelement.wall },
            {"@",Sokoelement.worker },
            {"+",Sokoelement.worker_doced },

        };
        public Position WorkerPosition = null;
        public List<Position> listWallPosition = new List<Position>();
        public List<Position> listDockPosition = new List<Position>();
        public List<Position> listBoxPosition = new List<Position>();
        Dictionary<Direction, Position> dicDirectionPoint = new Dictionary<Direction, Position>()
        {
            {Direction.Up,new Position (-1,0) },
            {Direction.Down ,new Position (1,0)},
            {Direction.Left ,new Position (0,-1) },
            {Direction.Right ,new Position (0,1) },

        };
        private enum Direction
        {
            Up,
            Down,
            Left ,
            Right 
        };
        public void MovePlayer(Direction direction)
        {
            Position NextToPosition = dicDirectionPoint[direction];
            String element = Level2d[NextToPosition.Row, NextToPosition.Col];
            bool IsItHitthewall = false;
            
            if (element ==dicElemntType [Level2d[NextToPosition.Row, NextToPosition.Col]]))
            {
                return;
            }
        }
        public void ParseLevel(String level)
        {
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
            Row = Level2d.GetLength(0);
            Col = Level2d.GetLength(1);

            for(i=0;i<Row; i++)
            {
                for(j=0;j<Col; j++)
                {
                    switch (dicElemntType[ Level2d[i, j]])
                    {
                        case Sokoelement.box:
                            listBoxPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.box_docx:
                            listBoxPosition.Add(new Position(i, j));
                            listDockPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.dock:
                            listDockPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.floor:
                            break;
                        case Sokoelement.wall:

                            listWallPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.worker:
                            if (WorkerPosition != null)
                            {
                                throw new Exception("The level contain more than one worker. It is invalid");
                            }
                            WorkerPosition = new Position(i, j);
                            break;
                        case Sokoelement.worker_doced:
                            if (WorkerPosition != null)
                            {
                                throw new Exception("The level contain more than one worker. It is invalid");
                            }
                            WorkerPosition = new Position(i, j);
                            listBoxPosition.Add(new Position(i, j));
                            break;
                    }
                }
            }
            if(listDockPosition.Count != listBoxPosition.Count)
            {
                throw new Exception("No of list DockPostion does not match with list BoxPosition");
            }
            if(listDockPosition.Count == 0)
            {
                throw new Exception("No of list of DockPosition is 0 which is not correct. It must be more than 0");
            }

        }
        public  SokobanMap(String level)
        {
            this.Level = level;
            ParseLevel(this.Level);
        }
    }
}
