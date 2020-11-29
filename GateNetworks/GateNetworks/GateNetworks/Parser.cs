using System;
using System.CodeDom;
using System.IO;

namespace GateNetworks
{
    class Parser
    {
        private StreamReader reader;
        private long currentLine = 0;

        private string line = null;

        public Parser(StreamReader reader)
        {
            this.reader = reader;
        }
        
        public long CurrentLine => currentLine;

        /// <summary>
        /// Methods for throwing exception with currentLine parameter
        /// </summary>
        #region ErrorDefinitions
        public void SyntaxError() { throw new SyntaxError(currentLine); }
        public void MissingKeyword() { throw new MissingKeywordError(currentLine); }
        public void BindingRuleError() { throw new BindingRuleError(currentLine); }
        public void DuplicateError() { throw new DuplicateError(currentLine); }
        

        #endregion

        // Load one line for processing
        public string LoadLine()
        {
            // If we already have loaded a line, this works as a getter
            if (line != null) return line;

            // Skip empty lines and read line
            do
            {
                line = reader.ReadLine();
                currentLine++;
            } while (line != null && (line.Trim().Length == 0 || line[0] == ';'));

            // Couldn't read a valid line so we reached EOF
            if (line == null)
            {
                throw new EOF();
            }

            return line;
        }

        // Reset the line to read another one
        public void NextLine()
        {
            line = null;
        }

        // I need to detect invalid chars like \t, since all elements must be separated just by one space 
        public string[] SplitLine()
        {
            return LoadLine().Split(' ');
        }

        // Splits the line into identifier and its arguments
        public void SplitIdentifier(out string identifier, out string[] args)
        {
            string[] split = SplitLine();
            identifier = split[0];
            args = split.Slice(1, split.Length);
        }

        // Returns just the identifier name, used for loading network name
        public void GetNetworkIdentifier(out string identifier)
        {
            string[] ignoreArgs;
            SplitIdentifier(out identifier,out ignoreArgs);
        }

        // Checks valid format of zero argument identifiers: network and end
        public void CheckControlWord(string word)
        {
            string[] args;
            string expected;
            SplitIdentifier(out expected,out args);
            if (word != expected)
            {
                SyntaxError();
            }

            if (args.Length > 0)
            {
                SyntaxError();
            }
        }

        public void CheckGateDefinition(string word, out string[] args)
        {
            string expected;
            SplitIdentifier(out expected,out args);
            if (word != expected)
            {
                SyntaxError();
            }

            if (args.Length != 1)
            {
                SyntaxError();
            }
        }

    }
}