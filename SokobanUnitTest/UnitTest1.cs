using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sokoban;
using static Sokoban.SokobanMap;

namespace SokobanUnitTest
{
    [TestClass]
    public class UnitTest1
    {

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
        private Dictionary<Sokoelement, String> dicElemntTypeToString = new Dictionary<Sokoelement, String>()
        {
            {Sokoelement.box,"$"  },
            {Sokoelement.box_docx,"*" },
            {Sokoelement.dock,"." },
            {Sokoelement.floor," " },
            {Sokoelement.wall,"#" },
            {Sokoelement.worker,"@" },
            {Sokoelement.worker_dock,"+" },

        };


        String Level =
@"#######
#.@ # #
#$* $ #
#   $ #
# ..  #
#  *  #
#######";

        [TestMethod]
        public void Walk()
        {
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(Level);
            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Left);

            Assert.IsTrue(Map.Level2d[1, 1] ==dicElemntTypeToString[Sokoelement.worker_dock] );
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col  == 1);

            Map.PlayerWalk(Direction.Left);
            Assert.IsTrue(Map.Level2d[1, 1] == dicElemntTypeToString[Sokoelement.worker_dock]);
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col == 1);

            Map.PlayerWalk(Direction.Right);
            Assert.IsTrue(Map.Level2d[1, 2] == dicElemntTypeToString[Sokoelement.worker]);
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col == 2);

            Map.PlayerWalk(Direction.Right);
            Assert.IsTrue(Map.Level2d[1, 3] == dicElemntTypeToString[Sokoelement.worker]);
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col == 3);


            Map.PlayerWalk(Direction.Down);
            Assert.IsTrue(Map.Level2d[2, 3] == dicElemntTypeToString[Sokoelement.worker]);

            Map.PlayerWalk(Direction.Down);
            Assert.IsTrue(Map.Level2d[3, 3] == dicElemntTypeToString[Sokoelement.worker]);
             

            Map.PlayerWalk(Direction.Right);
            Assert.IsTrue(Map.Level2d[3, 4] == dicElemntTypeToString[Sokoelement.worker]);
            //Assert.IsTrue (Map.dicBoxPosition.ContainsKey (new ))
            Assert.IsTrue(Map.IsContatinBoxAtPosition(3, 5));
            Assert.IsTrue(Map.dicBoxPosition.Count == 5);
            Map.PlayerWalk(Direction.Left );
            Assert.IsTrue(Map.Level2d[3, 3] == dicElemntTypeToString[Sokoelement.worker]);

        }
        [TestMethod]
        public void InitialMap()
        {
            String ILevel =
@"#######
#.@ # #
#$* $ #
#   $ #
# ..  #
#  *  #
#######";
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(ILevel);

            Assert.IsTrue(Map.dicBoxPosition.Count == 5);
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(2, 1).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(2, 2).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(2, 4).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(3, 4).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(5, 3).PositionString()));

          
            Assert.IsTrue(!Map.IsSolve);

            String nonSquareLevel =
@"#######
#.@ # #
#$* $       #
#   $ #
# ..  #
#  *  #
#######";

            Map = new Sokoban.SokobanMap(nonSquareLevel);
            Assert.IsTrue(Map.Col == "#$* $       #".Length);
            Assert.IsTrue(Map.Row == 7);

            Assert.IsTrue(Map.dicBoxPosition.Count == 5);
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(2, 1).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(2, 2).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(2, 4).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(3, 4).PositionString()));
            Assert.IsTrue(Map.dicBoxPosition.ContainsKey(new Position(5, 3).PositionString()));

        }
        [TestMethod]
        public void Push()
        {
            String pLevel =
@"#######
#.@ # #
#$* $ #
#   $ #
# ..  #
#  *  #
#######";
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(Level);

            Assert.IsTrue(Map.dicBoxPosition.Count == 5);
            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Left);

            Assert.IsTrue(Map.Level2d[1, 1] == dicElemntTypeToString[Sokoelement.worker_dock]);

            Map.PlayerWalk(Direction.Down);
            Assert.IsTrue(Map.Level2d[2, 1] == dicElemntTypeToString[Sokoelement.worker]);
            Assert.IsTrue(Map.Level2d[3, 1] == dicElemntTypeToString[Sokoelement.box]);

            Assert.IsTrue(Map.dicBoxPosition.Count == 5);
            Assert.IsTrue(Map.WorkerPosition.Row == 2);
            Assert.IsTrue(Map.WorkerPosition.Col == 1);



        }

        [TestMethod]
        public void PushBoxNextToBox()
        {
            String pLevel =
@"#######
#@. # #
#$* $ #
#$  $ #
#$....#
#  *  #
#######";
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(pLevel);

            Assert.IsTrue(Map.dicBoxPosition.Count == 7);
            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Down);

            Assert.IsTrue(Map.Level2d[1, 1] == dicElemntTypeToString[Sokoelement.worker]);
            Assert.IsTrue(Map.dicBoxPosition.Count == 7);
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col == 1);

            Assert.IsTrue(Map.dicBoxPosition.Count == 7);
            Assert.IsTrue(Map.IsContatinBoxAtPosition(2, 1));
            Assert.IsTrue(Map.IsContatinBoxAtPosition(2, 1));
            Assert.IsTrue(Map.IsContatinBoxAtPosition(2, 1));





        }

        [TestMethod]
        public void PushBoxNextToWall()
        {
            String pLevel =
@"#######
#@. # #
#$* $ #
#$  $ #
##.. .#
#  *  #
#######";
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(pLevel);

            Assert.IsTrue(Map.dicBoxPosition.Count == 6);
            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Down);

            Assert.IsTrue(Map.Level2d[1, 1] == dicElemntTypeToString[Sokoelement.worker]);
            Assert.IsTrue(Map.dicBoxPosition.Count == 6);
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col == 1);

            Assert.IsTrue(Map.dicBoxPosition.Count == 6);
            Assert.IsTrue(Map.IsContatinBoxAtPosition(2, 1));
            Assert.IsTrue(Map.IsContatinBoxAtPosition(3, 1));






        }

        [TestMethod]
        public void SolveStatus()
        {
            String pLevel =
@"#######
#@  # #
#$*   #
#.    #
##    #
#  *  #
#######";
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(pLevel);
            Assert.IsTrue(!Map.IsSolve);

           // Assert.IsTrue(Map.dicBoxPosition.Count == 6);
            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Down);

            Assert.IsTrue(Map.Level2d[2, 1] == dicElemntTypeToString[Sokoelement.worker]);
            Assert.IsTrue(Map.Level2d[3, 1] == dicElemntTypeToString[Sokoelement.box_docx]);
            Assert.IsTrue(Map.IsSolve);

           // Assert.IsTrue(Map.dicBoxPosition.Count == 6);
          





        }

        [TestMethod]
        public void Undo()
        {
            String pLevel =
@"####
#.@#
#$ #
####";
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(pLevel);

            String[,] level2dOriginal =(String[,])Map.Level2d.Clone();

            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Down);
            Map.Undo();
            int i;
            int j;
            for(i=0;i<level2dOriginal.GetUpperBound(0); i++)
            {
                for(j=0;j<level2dOriginal.GetUpperBound(1); j++)
                {
                    if(level2dOriginal[i,j] != Map.Level2d[i, j])
                    {
                        Assert.Fail($"{i} , {j} has problem");
                    }
                }
            }
           






        }
    }
}
