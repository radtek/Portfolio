using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace System.Net.Json
{
    [Serializable]
    public class GeneratorException : Exception
    {
        // Fields
        private CompilerResults _results;

        // Methods
        public GeneratorException()
        {
        }
        public GeneratorException(string message)
            : base(message)
        {
        }

        protected GeneratorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GeneratorException(string message, CompilerResults results)
            : base(message)
        {
            this._results = results;
        }

        public GeneratorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // Properties
        public CompilerResults CompilerResults
        {
            get
            {
                return this._results;
            }
        }
        //public CompilerResults get_CompilerResults()
        //{
        //    return this._results;
        //}


 

    }


}
