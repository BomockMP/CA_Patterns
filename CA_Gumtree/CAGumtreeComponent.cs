using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace CA_Gumtree
{
    public class CAGumtreeComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        /// 


        public CAGumtreeComponent()
          : base("CA_Gumtree", "CA_Gumtree",
              "CA based on the bark of gumtrees",
              "Cellular Automata", "")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("columns", "columns", "number of columns" , GH_ParamAccess.item);
            pManager.AddIntegerParameter("rows", "rows", "number of rows", GH_ParamAccess.item);
            pManager.AddNumberParameter("age", "age", "age", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Neighbour Pattern type", "type", "define the pattern type for the cell for finding its neighbours", GH_ParamAccess.list);
            //restart
            pManager.AddBooleanParameter("restart simulation", "restart", "toogle to restart simulation", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
            pManager.AddPointParameter("cell position", "pos", "cell", GH_ParamAccess.list);
            pManager.AddNumberParameter("age as double", "age", "age", GH_ParamAccess.list);
            /*  public List<Point2d> renderPtList = new List<Point2d>();
    public List<double> ageList = new List<double>(); */
        }


        //--------------------------------------------------------------------------------------------------------------------
        // -------SETUP----------
        // -------------------------------------------------------------------------------------------------------------------- 

        //Global Variables

        public Boolean initialise = true;
        public int columns = 0;
        public int rows = 0;
        //public int cellAge = 0;
        public List<double> randomAgeList = new List<double>();

        public List<int> patternTypeList = new List<int>(); //list of ints to define pattern type

        public CellEnvironment cellEnvironment;

        //render lists
        public List<Point2d> renderPtList = new List<Point2d>();
        public List<double> ageList = new List<double>();


        //additional functions
        public void render()
        {
            renderPtList = new List<Point2d>();
            ageList = new List<double>();

            foreach (var v in cellEnvironment.cellList)
            {
                Point2d p = new Point2d(v.xPos, v.YPos);
                renderPtList.Add(p);
                ageList.Add(v.age);
                
            }
        }




        ///RUN
        /// 
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            randomAgeList = new List<double>();
            patternTypeList = new List<int>();

            Boolean restart = false; //restart toggle

            if (!DA.GetData(0, ref columns)) return;
            if (!DA.GetData(1, ref rows)) return;
            if (!DA.GetDataList(2, randomAgeList)) return;
            if (!DA.GetDataList(3, patternTypeList)) return;

            if (!DA.GetData(4, ref restart)) { return; } //collect restart data



            //code to run once
            if (initialise) {
                
                //clear current environment (testing)
               
                //set up environment and board
                cellEnvironment = new CellEnvironment(columns, rows);
                //add cells to array
                //for each int of rows, and each int of columns, create a new cell at that location, and then add it to the array of cells
                for (int i = 1; i < columns; i++) {
                    for (int j = 1; j < rows; j++)
                    {

                        if (i * j < randomAgeList.Count && i * j < patternTypeList.Count)
                        {
                            double d = randomAgeList.ElementAt(i * j);
                            int type = patternTypeList.ElementAt(i * j); //must make sure there are enough in the list
                            //double cellsRandomage = randomAgeList.ElementAt(i * j);
                            Cell a = new Cell(i, j, d, type);
                        cellEnvironment.cellList.Add(a);  
                    }
                }
                }


                Rhino.RhinoApp.WriteLine("Initialised");
                initialise = false;
            }

            //restart command
            if (restart)
            {
                Rhino.RhinoApp.WriteLine("Restart");
                initialise = true;
                restart = false;
            }

            //COMMANDS TO LOOP


            cellEnvironment.run();

            //DRAW COMMAND
            render();

            //assign outputs
            DA.SetDataList(0, renderPtList);
            DA.SetDataList(1, ageList);



        }
        



   
        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            //added fresh GUID
            get { return new Guid("b90c2cb1-5f25-4908-b13b-410c302f5305"); }
        }
    }
}
