using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Grasshopper.Kernel;
using Rhino.Geometry;



namespace CA_Gumtree
{

  

   public class CellEnvironment
    {
        public int columns;
        public int rows;
       public Cell[,] board;

        public List<Cell> cellList;

        //constructor
        public CellEnvironment(int _columns, int _rows) {

            columns = _columns;
            rows = _rows;

            //an empty double array of cells, size colums * rows. gonna use a list
            board = new Cell[columns,rows];

            //a new list to hold cells.
            cellList = new List<Cell>();
    }


        public void run()
        {



            if (cellList.Count() > 0)
            {

                

                //Rhino.RhinoApp.WriteLine(pop.ElementAt(2).position.ToString());

                foreach (var a in cellList)
                {
                    //kill cells that have death boolean = true & reset the boolean
                    if (a.shouldIDie) { a.age = 0;
                       a.shouldIDie = false;
                    }
                
                    //call the run function of the cell
                    a.run(this);
                }

            }
        }



    }
}
