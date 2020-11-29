using System.Collections;
using System.Collections.Generic;

namespace GateNetworks
{
    // TODO
    class FunctionVectorComparer: IEqualityComparer<FunctionVector>
    {
        public bool Equals(FunctionVector x, FunctionVector y)
        {
            throw new System.NotImplementedException();
        }

        public int GetHashCode(FunctionVector obj)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Vector of gate transform function 
    /// </summary>
    public class FunctionVector
    {
        private Charge[] operands;

        private int hash;

        public int Count => operands.Length;
        // TODO: Check validity and assign error message possibly
        // TODO: Function logic, vector comparison

        public void Split(int length, out FunctionVector key, out FunctionVector value)
        {
            if(length>Count) throw new MyException();
            key = new FunctionVector { operands = new Charge[length] };
            value = new FunctionVector { operands = new Charge[Count - length] };
            for (int i = 0; i < Count; i++)
            {
                if (i < length)
                {
                    key.operands[i] = operands[i];
                }
                else
                {
                    value.operands[i - length] = operands[i];
                }
            }
        }
    }
}