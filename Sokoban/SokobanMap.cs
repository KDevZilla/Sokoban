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
        public String[,] Level2d = null;
        Sokoelement[,] LevelElement = null;
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

        private Dictionary<String, Sokoelement> dicStringToElemntType = new Dictionary<String, Sokoelement>()
        {
            {"$", Sokoelement.box  },
            {"*",Sokoelement.box_docx },
            {".",Sokoelement.dock },
            {" ",Sokoelement.floor },
            {"#",Sokoelement.wall },
            {"@",Sokoelement.worker },
            {"+",Sokoelement.worker_doced },

        };
        private Dictionary< Sokoelement, String> dicElemntTypeToString = new Dictionary<Sokoelement, String >()
        {
            {Sokoelement.box,"$"  },
            {Sokoelement.box_docx,"*" },
            {Sokoelement.dock,"." },
            {Sokoelement.floor," " },
            {Sokoelement.wall,"#" },
            {Sokoelement.worker,"@" },
            {Sokoelement.worker_doced,"+" },

        };
        public Position WorkerPosition = null;
        //public List<Position> listWallPosition = new List<Position>();
        //public List<Position> listDockPosition = new List<Position>();
        //public List<Position> listBoxPosition = new List<Position>();

        public Dictionary<String, Position> dicWallPosition = new Dictionary<string, Position>();
        public Dictionary<String, Position> dicDockPosition = new Dictionary<string, Position>();
        public Dictionary<String, Position> dicBoxPosition = new Dictionary<string, Position>();
        Dictionary<Direction, Position> dicDirectionPoint = new Dictionary<Direction, Position>()
        {
            {Direction.Up,new Position (-1,0) },
            {Direction.Down ,new Position (1,0)},
            {Direction.Left ,new Position (0,-1) },
            {Direction.Right ,new Position (0,1) },

        };
        public enum Direction
        {
            Up,
            Down,
            Left ,
            Right 
        };
        public Sokoelement GetElement(int Row, int Col)
        {
            String elementString = Level2d[Row, Col];
            return dicStringToElemntType[elementString];

        }
        public void AddDockPosition(int row, int col)
        {
            AddDockPosition(new Position(row, col));
        }
        public void AddDockPosition(Position pos)
        {
            dicDockPosition.Add(pos.PositionString(), pos);
        }
        public void AddWallPosition(int row, int col)
        {
            AddWallPosition(new Position(row, col));
        }
        public void AddWallPosition(Position pos)
        {
            dicWallPosition.Add(pos.PositionString(), pos);
        }
        public void AddBoxPosition(int row, int col)
        {
            AddBoxPosition(new Position(row, col));
        }
        public void AddBoxPosition(Position pos)
        {
            dicBoxPosition.Add(pos.PositionString(), pos);
        }

        public bool IsInRange(int Row, int Col)
        {
            if(Row < 0 || Row > this.Row || Col < 0 || Col > this.Col)
            {
                return false;
            }
            return true;
        }

        public void UpdateMap()
        {
            int i;
            int j;
            /*
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Row; j++)
                {

                }
            }
            */

            Level2d[WorkerPosition.Row, WorkerPosition.Col] = dicElemntTypeToString[Sokoelement.worker];
            foreach (Position pos in dicBoxPosition.Values)
            {
                Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.box];
            }
            foreach (Position pos in dicWallPosition.Values)
            {
                Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.wall];
            }
            foreach (Position pos in dicDockPosition.Values)
            {
                if (Level2d[pos.Row, pos.Col] == dicElemntTypeToString[Sokoelement.box])
                {
                    Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.box_docx];
                } else if(Level2d [pos.Row ,pos.Col]== dicElemntTypeToString[Sokoelement.worker]){

                    Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.worker_doced];
                }
                else
                {
                    Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.dock];
                }
            }

        }
        public void PlayerWalk(Direction direction)
        {
            Position deltaPosition = dicDirectionPoint[direction];
            Position NextToPlayerPosition = this.WorkerPosition.Add(deltaPosition);
            //Position NextToPosition = this.WorkerPosition.Add(deltaPosition);

            //this.WorkerPosition
          //  String elementString = Level2d[NextToPosition.Row, NextToPosition.Col];
           // bool IsItHitthewall = false;
            Sokoelement element = GetElement(NextToPlayerPosition.Row, NextToPlayerPosition.Col);
            bool CanMove = false;
            List<Position> listPositionBoxMove = new List<Position>();
            Position NextToPosition = null;
            if (element== Sokoelement.box ||
                element== Sokoelement.box_docx)
            {
                int i;
                 NextToPosition = NextToPlayerPosition.Clone();
                while (true)
                {
                    NextToPosition  = NextToPosition.Add(deltaPosition);
                    if(!IsInRange (NextToPosition.Row ,NextToPosition.Col))
                    {
                        break;
                    }

                    Sokoelement NextElement = GetElement(NextToPosition.Row, NextToPosition.Col);
                    if(NextElement == Sokoelement.wall)
                    {
                        CanMove = false;
                       // IsItHitthewall = true;
                        break;
                    } else if(NextElement == Sokoelement.box ||
                        NextElement == Sokoelement.box_docx)
                    {
                        listPositionBoxMove.Add(NextToPosition);
                    } else if(NextElement == Sokoelement.floor ||
                        NextElement== Sokoelement.dock)
                    {
                        CanMove = true;
                        break;
                    }

                }
                return;
            }
            if (CanMove)
            {
                for(int i = listPositionBoxMove.Count; i >= 0; i--)
                {
                    NextToPosition = listPositionBoxMove[i].Add(deltaPosition);
                    dicBoxPosition[listPositionBoxMove[i].PositionString()] = NextToPosition.Clone();
                }
            }
            WorkerPosition = NextToPlayerPosition.Clone();

            UpdateMap();

            
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
           // LevelElement = new Sokoelement[Row, Col];
            for (i=0;i<Row; i++)
            {
                for(j=0;j<Col; j++)
                {
                    switch (dicStringToElemntType[ Level2d[i, j]])
                    {
                        case Sokoelement.box:
                            AddBoxPosition(i, j);
                            //listBoxPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.box_docx:
                            AddBoxPosition(i, j);
                            AddDockPosition(i, j);
                            //listBoxPosition.Add(new Position(i, j));
                            //listDockPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.dock:
                            AddDockPosition(i, j);
                            //listDockPosition.Add(new Position(i, j));
                            break;
                        case Sokoelement.floor:
                            break;
                        case Sokoelement.wall:
                            AddWallPosition(i, j);
                            //listWallPosition.Add(new Position(i, j));
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
                            AddBoxPosition(i, j);
                            //listBoxPosition.Add(new Position(i, j));
                            break;
                    }
                }
            }
            if(dicBoxPosition.Count != dicDockPosition.Count )            
            {
                throw new Exception("No of list DockPostion does not match with list BoxPosition");
            }

            if(dicBoxPosition.Count == 0)
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
