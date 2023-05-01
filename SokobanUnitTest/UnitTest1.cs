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
            {"+",Sokoelement.worker_doced },

        };
        private Dictionary<Sokoelement, String> dicElemntTypeToString = new Dictionary<Sokoelement, String>()
        {
            {Sokoelement.box,"$"  },
            {Sokoelement.box_docx,"*" },
            {Sokoelement.dock,"." },
            {Sokoelement.floor," " },
            {Sokoelement.wall,"#" },
            {Sokoelement.worker,"@" },
            {Sokoelement.worker_doced,"+" },

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

            Assert.IsTrue(Map.Level2d[1, 1] ==dicElemntTypeToString[Sokoelement.worker_doced] );
            Assert.IsTrue(Map.WorkerPosition.Row == 1);
            Assert.IsTrue(Map.WorkerPosition.Col  == 1);

            Map.PlayerWalk(Direction.Left);
            Assert.IsTrue(Map.Level2d[1, 1] == dicElemntTypeToString[Sokoelement.worker_doced]);
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
            Assert.IsTrue(Map.Level2d[3, 3] == dicElemntTypeToString[Sokoelement.worker]);
            Map.PlayerWalk(Direction.Left );
            Assert.IsTrue(Map.Level2d[3, 2] == dicElemntTypeToString[Sokoelement.worker]);

        }

        [TestMethod]
        public void Push()
        {
            Sokoban.SokobanMap Map = new Sokoban.SokobanMap(Level);
            Map.PlayerWalk(Sokoban.SokobanMap.Direction.Left);

            Assert.IsTrue(Map.Level2d[1, 1] == dicElemntTypeToString[Sokoelement.worker_doced]);

        }
    }
}
