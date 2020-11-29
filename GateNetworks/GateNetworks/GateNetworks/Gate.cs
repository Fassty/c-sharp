using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace GateNetworks
{
    class GateBuilder
    {
        private string name;
        private string[] inputs;
        private string[] outputs;
        private Dictionary<FunctionVector, FunctionVector> function;

        public GateBuilder()
        {
            inputs = null;
            outputs = null;
            function = new Dictionary<FunctionVector, FunctionVector>(new FunctionVectorComparer());
        }

        public string[] SetInputs
        {
            set
            {
                if (inputs != null) throw new MyException();

                if (!Utilities.HasDuplicates(value)) throw new DuplicityException();

                if(!value.ValidValues()) throw new InvalidValueException();

                outputs = value;
            }
        }

        public string[] SetOutputs
        {
            set
            {
                if(outputs != null) throw new MyException();

                if(!Utilities.HasDuplicates(value)) throw new DuplicityException();

                if(!value.ValidValues()) throw  new InvalidValueException();

                outputs = value;
            }
        }

        public void AddFunctionVector(FunctionVector vector)
        {
            if (vector.Count != inputs.Length + outputs.Length)
            {
                throw new InvalidVectorSizeException();
            }

            FunctionVector key, value;
            vector.Split(inputs.Length, out key, out value);
            if (key.Count != inputs.Length)
            {
                throw new MyException();
            }

            if (value.Count != outputs.Length)
            {
                throw new MyException();
            }

            if (function.ContainsKey(key))
            {
                throw new DuplicityException();
            }

            function.Add(key, value);
        }


    }

    /// <summary>
    /// We don't necessarily need to instantiate all defined Gates, so remembering this will help save memory
    /// </summary>
    class GateTemplate
    {

    }

    /// <summary>
    /// Gate definition
    /// </summary>
    class Gate : InstanceTemplate
    {
        private string name;
        private Dictionary<FunctionVector, FunctionVector> function;

        public Gate(string name, Dictionary<FunctionVector,FunctionVector> function, string[] inputs, string[] outputs) : base(inputs, outputs)
        {
            this.name = name;
            this.function = function;
        }
    }
}