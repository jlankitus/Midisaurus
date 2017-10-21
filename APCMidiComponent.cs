using System;
using System.Collections.Generic;
using Midi;
using System.Threading;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace APC_Midi
{
    public class APCMidiComponent : GH_Component
    {
        IGH_DataAccess DA;

        public APCMidiComponent()
          : base("APC_Midi", "APC Midi",
              "Gets APC Midi notes",
              "Handel Plugins", "OSC/Midi")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("slider", "slider", "...", GH_ParamAccess.item);
            pManager.AddGenericParameter("slider2", "slider", "...", GH_ParamAccess.item);
            pManager.AddGenericParameter("slider3", "slider", "...", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Knob", "Knob", "...", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Value", "Value", "...", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            this.DA = DA;
            
            // Select first midi keyboard
            InputDevice inputDevice = InputDevice.InstalledDevices[0];
            if(!inputDevice.IsOpen)
            inputDevice.Open();
            inputDevice.StartReceiving(null);
            go(inputDevice);
        }

        public void go(InputDevice inputDevice)
        {
            this.inputDevice = inputDevice;
            pitchesPressed = new Dictionary<Pitch, bool>();
            inputDevice.NoteOn += new InputDevice.NoteOnHandler(this.NoteOn);
            inputDevice.NoteOff += new InputDevice.NoteOffHandler(this.NoteOff);
            inputDevice.ControlChange += new InputDevice.ControlChangeHandler(this.ControlChange);
            // PrintStatus();
        }

        public void NoteOn(NoteOnMessage msg)
        {
            lock (this)
            {
                pitchesPressed[msg.Pitch] = true;
            }
        }

        public void NoteOff(NoteOffMessage msg)
        {
            lock (this)
            {
                pitchesPressed.Remove(msg.Pitch);
            }
        }

        public void ControlChange(ControlChangeMessage msg)
        {
            string control = msg.Control.ToString();
            string value = msg.Value.ToString();

            GH_Document ghDocument = this.OnPingDocument();
            IList<IGH_DocumentObject> objects = ghDocument.Objects;

            List<string> sliderNickNames = new List<string>();
            List<string> sliderGuids = new List<string>();

            var input = Params.Input[0].Sources[0]; //get the first thing connected to the first input of this component
            var slider1 = input as Grasshopper.Kernel.Special.GH_NumberSlider; //try to cast that thing as a slider

            var input2 = Params.Input[1].Sources[0]; //get the first thing connected to the first input of this component
            var slider2 = input2 as Grasshopper.Kernel.Special.GH_NumberSlider; //try to cast that thing as a slider

            var input3 = Params.Input[2].Sources[0]; //get the first thing connected to the first input of this component
            var slider3 = input3 as Grasshopper.Kernel.Special.GH_NumberSlider; //try to cast that thing as a slider

            if (slider1 != null && control == "48") //if the component was successfully cast as a slider
            {
                /*
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    slider.SetSliderValue((decimal)msg.Value);
                    slider.ExpireSolution(true);
                });*/
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() => { slider1.SetSliderValue((decimal)msg.Value); }));
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() => { slider1.ExpireSolution(true); }));
            }
            if (slider2 != null && control == "49") //if the component was successfully cast as a slider
            {
                /*
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    slider.SetSliderValue((decimal)msg.Value);
                    slider.ExpireSolution(true);
                });*/
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() => { slider2.SetSliderValue((decimal)msg.Value); }));
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() => { slider2.ExpireSolution(true); }));
            }
            if (slider3 != null && control == "50") //if the component was successfully cast as a slider
            {
                /*
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    slider.SetSliderValue((decimal)msg.Value);
                    slider.ExpireSolution(true);
                });*/
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() => { slider3.SetSliderValue((decimal)msg.Value); }));
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() => { slider3.ExpireSolution(true); }));
            }

            DA.SetData(0, control);
            DA.SetData(1, value);
            // RunScript();
            // Console.WriteLine(msg.Control.ToString());
            // Console.WriteLine(msg.Value.ToString());
        }

        /*
        public override void AddedToDocument(GH_Document document)
        {
            // base.AddedToDocument(document);    
              
        }

        protected override void ExpireDownStreamObjects()
        {
            base.ExpireDownStreamObjects();
        }*/


        private InputDevice inputDevice;
        private Dictionary<Pitch, bool> pitchesPressed;
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
            get { return new Guid("{95eaaac9-c6d9-4973-8d59-151b9eeabfba}"); }
        }
    }
}
