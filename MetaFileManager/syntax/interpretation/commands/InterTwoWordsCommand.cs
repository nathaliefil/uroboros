using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.commands.other;

namespace Uroboros.syntax.interpretation.commands
{
    class InterTwoWordsCommand
    {
        public static ICommand Build(string word1, string word2)
        {
            if (word1.Equals("clear"))
            {
                if (word2.Equals("bin"))
                    return new TwoWordCommand(TwoWordCommandType.ClearBin);
                if (word2.Equals("clipboard"))
                    return new TwoWordCommand(TwoWordCommandType.ClearClipboard);
                if (word2.Equals("log"))
                    return new TwoWordCommand(TwoWordCommandType.ClearLog);
            }

            if (word1.Equals("log"))
            {
                if (word2.Equals("on"))
                    return new TwoWordCommand(TwoWordCommandType.LogOn);
                if (word2.Equals("off"))
                    return new TwoWordCommand(TwoWordCommandType.LogOff);
            }

            if (word1.Equals("uroboros") && (word2.Equals("stop")))
                    return new TwoWordCommand(TwoWordCommandType.UroborosStop);


            throw new SyntaxErrorException("ERROR! Unknown two-word command: " + word1 + " " + word2+".");
        }
    }
}
