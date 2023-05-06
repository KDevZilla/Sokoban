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

        public bool IsSolve { get; private set; } = false;
        
        public String[,] Level2d = null;
        Stack<String[,]> stackState { get; set; } = new Stack<String[,]>();
        Stack<Direction> stackWorkderDirection { get; set; } = new Stack<Direction>();
        Sokoelement[,] LevelElement = null;
        public int Row { get; private set; } = 0;
        public int Col { get; private set; } = 0;
        public enum Sokoelement
        {
            box,
            box_docx,
            dock,
            floor,
            wall,
            worker,
            worker_dock
        }
        public Direction WorkerCurrentDirection { get; private set; } = Direction.Left;
        private Dictionary<String, Sokoelement> dicStringToElemntType = new Dictionary<String, Sokoelement>()
        {
            {"$", Sokoelement.box  },
            {"*",Sokoelement.box_docx },
            {".",Sokoelement.dock },
            {" ",Sokoelement.floor },
            {"#",Sokoelement.wall },
            {"@",Sokoelement.worker },
            {"+",Sokoelement.worker_dock },

        };
        private Dictionary< Sokoelement, String> dicElemntTypeToString = new Dictionary<Sokoelement, String >()
        {
            {Sokoelement.box,"$"  },
            {Sokoelement.box_docx,"*" },
            {Sokoelement.dock,"." },
            {Sokoelement.floor," " },
            {Sokoelement.wall,"#" },
            {Sokoelement.worker,"@" },
            {Sokoelement.worker_dock,"+" },

        };
        public Position WorkerPosition = null;
        //public List<Position> listWallPosition = new List<Position>();
        //public List<Position> listDockPosition = new List<Position>();
        //public List<Position> listBoxPosition = new List<Position>();

        public bool IsContainBoxAtPosition(Position position)
        {
            return dicBoxPosition.ContainsKey(position.PositionString());
        }
        public bool IsContatinBoxAtPosition(int row, int col)
        {
            return IsContainBoxAtPosition(new Position(row, col));
        }
        public bool IsWorkerAtPosition(Position position)
        {
            return WorkerPosition.Row == position.Row &&
                WorkerPosition.Col == position.Col;
        }
        public bool IsWorkerAtPosition(int row, int col)
        {
            return IsWorkerAtPosition(new Position(row, col));
        }
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

        public bool IsIndexInRange(int rowIndex, int colIndex)
        {
            if(rowIndex < 0 || rowIndex >= this.Row || colIndex < 0 || colIndex >= this.Col)
            {
                return false;
            }
            return true;
        }

        public void UpdateMap()
        {
            int i;
            int j;
            
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                {
                    Level2d[i, j] = dicElemntTypeToString[Sokoelement.floor];
                }
            }
            
            
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

                    Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.worker_dock];
                }
                else
                {
                    Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.dock];
                }
            }

        }
        private void CheckIfIsSolve()
        {
            foreach (Position pos in dicBoxPosition.Values)
            {
                if(!dicDockPosition.ContainsKey(pos.PositionString()))
                {
                    this.IsSolve = false;
                    return;
                }
                //Level2d[pos.Row, pos.Col] = dicElemntTypeToString[Sokoelement.box];
            }
            this.IsSolve = true;
           
            
        }
        //Stack<Direction> stackDirection { get; set; } = new Stack<Direction>();
        
        public void PlayerWalk(Direction direction)
        {
            Position deltaPosition = dicDirectionPoint[direction];
            Position NextToPlayerPosition = this.WorkerPosition.Add(deltaPosition);

            if (!IsIndexInRange(NextToPlayerPosition.Row, NextToPlayerPosition.Col))
            {
                return;
            }

            Sokoelement element = GetElement(NextToPlayerPosition.Row, NextToPlayerPosition.Col);
            bool CanMove = false;

            Position BoxPositionToMove = null;
            Position NextToPosition = null;
            if (element == Sokoelement.floor ||
               element == Sokoelement.dock)
            {
                CanMove = true;
            }
            else if (element== Sokoelement.box ||
                element== Sokoelement.box_docx)
            {
                
                int i;
                 BoxPositionToMove = NextToPlayerPosition.Clone();
                Position NextToBoxPostion = BoxPositionToMove.Add(deltaPosition);
                 //NextToPosition = NextToPlayerPosition.Clone();
                if (!IsIndexInRange(NextToBoxPostion.Row, NextToBoxPostion.Col))
                {
                    CanMove = false;
                }
                else
                {
                    Sokoelement NextToBoxElement = GetElement(NextToBoxPostion.Row, NextToBoxPostion.Col);

                    if (NextToBoxElement == Sokoelement.wall ||
                        NextToBoxElement == Sokoelement.box ||
                        NextToBoxElement == Sokoelement.box_docx)
                    {
                        CanMove = false;

                    } else
                    {
                        CanMove = true;
                    }
                } 

               // return;
            }
            if (!CanMove)
            {
                return;
            }

                if (BoxPositionToMove != null)
                {
                    MoveBox(BoxPositionToMove, BoxPositionToMove.Add(deltaPosition));
                }

           
           stackWorkderDirection.Push(WorkerCurrentDirection);
            WorkerCurrentDirection = direction;
            WorkerPosition = NextToPlayerPosition.Clone();
            

            //stackDirection.Push(direction);
            stackState.Push((String[,])Level2d.Clone());

            UpdateMap();
            CheckIfIsSolve();
            
        }
        private Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return Direction.Up;
                    break;
                case Direction.Up:
                    return Direction.Down;
                    break;
                case Direction.Left:
                    return Direction.Right;
                    break;
                case Direction.Right:
                    return Direction.Left;
                    break;
                default:
                    throw new Exception($"{direction} is not valid");
            }

        }
        public void Undo()
        {
            // Direction directionFromStack = stackDirection.Pop();
            if(stackState.Count <= 0)
            {
                return;
            }
          //  stackState.Pop();
            Level2d = stackState.Pop();
            WorkerCurrentDirection = stackWorkderDirection.Pop();
            ParseLevelFromLevel2d();
            UpdateMap();
            CheckIfIsSolve();

        }
        private void MoveBox(Position from, Position to)
        {
            dicBoxPosition.Remove(from.PositionString());
            dicBoxPosition.Add(to.PositionString(), to.Clone());

        }
        private void ParseLevelFromLevel2d()
        {
            Row = Level2d.GetLength(0);
            Col = Level2d.GetLength(1);

            dicBoxPosition = new Dictionary<string, Position>();
            dicDockPosition = new Dictionary<string, Position>();
            dicWallPosition = new Dictionary<string, Position>();
            WorkerPosition = null;

            int i;
            int j;

            // LevelElement = new Sokoelement[Row, Col];
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                {
                    switch (dicStringToElemntType[Level2d[i, j]])
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
                        case Sokoelement.worker_dock:
                            if (WorkerPosition != null)
                            {
                                throw new Exception("The level contain more than one worker. It is invalid");
                            }
                            WorkerPosition = new Position(i, j);
                            AddDockPosition(i, j);
                            //listBoxPosition.Add(new Position(i, j));
                            break;
                    }
                }
            }
            if (dicBoxPosition.Count != dicDockPosition.Count)
            {
                throw new Exception("No of list DockPostion does not match with list BoxPosition");
            }

            if (dicBoxPosition.Count == 0)
            {
                throw new Exception("No of list of DockPosition is 0 which is not correct. It must be more than 0");
            }
        }
        public void ParseLevel(String level)
        {
            String[] LevelLines = Level.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int MaxColumn = LevelLines[0].Length;
            int i, j = 0;
            for (i = 0; i < LevelLines.Length; i++)
            {
                if(MaxColumn< LevelLines[i].Length )
                {
                    MaxColumn = LevelLines[i].Length;
                }
     
            }
            Level2d = new string[LevelLines.Length, MaxColumn];

            for (i = 0; i < LevelLines.Length; i++)
            {
                string line = LevelLines[i];
                //for (j = 0; j < line.Length; j++)
                for(j=0;j<MaxColumn;j++)
                {
                    if (j > line.Length - 1)
                    {
                        Level2d[i, j] = " ";
                    }
                    else
                    {
                        Level2d[i, j] = line.Substring(j, 1);
                    }
                }
            }
            ParseLevelFromLevel2d();

        }
        public  SokobanMap(String level)
        {
            this.Level = level;
            ParseLevel(this.Level);
        }
    }
}
