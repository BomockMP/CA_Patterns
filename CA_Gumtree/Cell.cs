using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace CA_Gumtree
{

    public class Cell

    //Cell needs a position (bottom left corner x,y z = 0)
    //Cell needs a colour (do later, associated with the age)
    //Cell needs an age



    {

        //a position defined by an x and y location, the bottom left of the square
        public Vector2d position;
        public int xPos;
        public int YPos;
        public double age;
        public List<Cell> neighbours = new List<Cell>();
        public Boolean initiateCell = true;
        public Boolean shouldIDie = false;

        public Cell(int x, int y, double _age)
        {

            //position.X = x;
            //position.Y = y;

            xPos = x;
            YPos = y;
            age = _age;



        }

        public void run(CellEnvironment cellEnvironment)
        {
            //i think the order of functions will be important. Im Thinking, check age first, then averageAge. then call the kill function as the first in envirionment.run so it is called first.
            //find neighbours once
            if (initiateCell) {  
            getNeighbours(cellEnvironment);
                initiateCell = false;
            }

            //check to see if age of it and neighbours is >100, to iniiate kill boolean
            checkAge();


            //calculate age as average of other ages of cells around. this could be made more complex.
            averageAgeCalculate();
        }


        //get the neighbouring cells and add to a list.
        public void getNeighbours(CellEnvironment cellEnvironment)
        {

            foreach (var v in cellEnvironment.cellList) {

                if (v.xPos > 1 && v.YPos > 1 && v.xPos < cellEnvironment.columns && v.YPos < cellEnvironment.rows) { 

                if (v.xPos == xPos-1 && v.YPos == YPos-1 ) { neighbours.Add(v); }
                if (v.xPos == xPos - 1 && v.YPos == YPos ) { neighbours.Add(v); }
                if (v.xPos == xPos - 1 && v.YPos == YPos + 1) { neighbours.Add(v); }

                if (v.xPos == xPos && v.YPos == YPos - 1) { neighbours.Add(v); }
                if (v.xPos == xPos && v.YPos == YPos + 1) { neighbours.Add(v); }

                if (v.xPos == xPos + 1 && v.YPos == YPos - 1) { neighbours.Add(v); }
                if (v.xPos == xPos + 1 && v.YPos == YPos ) { neighbours.Add(v); }
                if (v.xPos == xPos + 1 && v.YPos == YPos + 1) { neighbours.Add(v); }

                }
            }

            //get the index of this cell;
            //get its neighbours;
            //add its neighbouring cells to a list for easy access;
            //neighbours.Add();
        }

        public void averageAgeCalculate()
        {

            age++;

            double averageAge = 0;
            double neighbourAges = 0;

            foreach (var v in neighbours)
            {


                neighbourAges = neighbourAges + v.age;
                
            }

            //add our own age
            averageAge = averageAge + age + neighbourAges;

            //find the average
            //averageAge = averageAge / neighbours.Count;
            averageAge = (averageAge / (neighbours.Count+1)); //plus one because includes self!!
            //set our age to the average
            age = averageAge;

            

        }

        //get the age of a neighbouring cell. might wanna do this via index instead of imputting cell.
        //  public double getAge(Cell cell)
        //   {
        //       return age;
        //    }

        public void checkAge() {

            //if this cell is old enough
            if (age > 100)
            {

                //neighbour count for storing how many neighbours are over 100
                int neighbourOverAgeCount = 0;

                //check the age of surrounding cells
                foreach (var v in neighbours)
                {
                    if (v.age > 100)
                    {
                        neighbourOverAgeCount++;
                    }
                }

                //if the neighbourOverAgeCount is > than ??? then death boolean  becomes true. 
                if (neighbourOverAgeCount > 2) {
                    shouldIDie = true;}

            }
        }


        //function for setting the age of the agent from outside 
        public void setAge(double newAge)
        {
            this.age = newAge;
        }

        public void drawCell()
        {

        }


    }
}