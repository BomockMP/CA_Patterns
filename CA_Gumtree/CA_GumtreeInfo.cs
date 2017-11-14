using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace CA_Gumtree
{
    public class CA_GumtreeInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "CAGumtree";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("843cd5c8-4b13-4d8e-9b4e-43c2de8f4b4a");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
