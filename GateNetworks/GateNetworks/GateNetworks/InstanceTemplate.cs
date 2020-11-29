namespace GateNetworks
{
    /// <summary>
    /// Template for network and gate objects
    /// </summary>
    abstract class InstanceTemplate
    {
        private string[] inputs;
        private string[] outputs;

        protected InstanceTemplate(string[] inputs, string[] outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;
        }

        public int InputCount => inputs.Length;
        public int OutputCount => outputs.Length;
        public string[] Inputs => inputs;
        public string[] Outputs => outputs;

        public bool HasInput(string name) => Utilities.Contains(inputs, name);

        public int InputIndex(string name) => Utilities.IndexOf(inputs, name);

        public bool HasOutput(string name) => Utilities.Contains(outputs, name);

        public int OutputIndex(string name) => Utilities.IndexOf(outputs, name);


    }
}