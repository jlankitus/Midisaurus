using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace APC_Midi
{
    public class APC_MidiInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "APCMidi";
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
                return new Guid("bbde151e-a6a0-44d1-a7d9-efe11e488dfc");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Handel Architects";
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
