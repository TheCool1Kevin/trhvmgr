using System;

namespace trhvmgr.Interactive
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandInfo : Attribute
    {
        public CommandInfo(int nparams)
        {
            this.MinParams = nparams;
            this.MaxParams = nparams;
        }

        public CommandInfo(int min, int max)
        {
            this.MinParams = min;
            this.MaxParams = max;
        }

        public int MinParams { get; set; }
        public int MaxParams { get; set; }
        public string ParameterSyntax { get; set; }
        public string HelpText { get; set; }
    }
}
