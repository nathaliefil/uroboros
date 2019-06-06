using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax
{
    class Subcommand
    {
        private SubcommandType type;
        private List<String> words;
        private int integerVariable;

        public Subcommand(SubcommandType type, List<String> words)
        {
            this.type = type;
            this.words = words;
            this.integerVariable = 0;
            Build();
        }

        public SubcommandType GetSType()
        {
            return type;
        }

        public int GetIntegerVariable()
        {
            return integerVariable;
        }

        private void Build()
        {
            switch (type)
            {
                case SubcommandType.Where:
                    {
                        BuildWhere();
                        break;
                    }
                case SubcommandType.WhereNot:
                    {
                        BuildWhereNot();
                        break;
                    }
                case SubcommandType.First:
                    {
                        BuildFirst();
                        break;
                    }
                case SubcommandType.Last:
                    {
                        BuildLast();
                        break;
                    }
                case SubcommandType.FirstPart:
                    {
                        BuildFirstPart();
                        break;
                    }
                case SubcommandType.LastPart:
                    {
                        BuildLastPart();
                        break;
                    }
                case SubcommandType.Ignore:
                    {
                        BuildIgnore();
                        break;
                    }
                case SubcommandType.Max:
                    {
                        BuildMax();
                        break;
                    }
                case SubcommandType.Stop:
                    {
                        BuildStop();
                        break;
                    }
                case SubcommandType.StopNot:
                    {
                        BuildStopNot();
                        break;
                    }
                case SubcommandType.Order:
                    {
                        BuildOrder();
                        break;
                    }
                case SubcommandType.Each:
                    {
                        BuildEach();
                        break;
                    }
            }
        }

        private void BuildWhere()
        {
        }
        private void BuildWhereNot()
        {
        }
        private void BuildFirst()
        {
            integerVariable = Convert.ToInt32(words[1]);
        }
        private void BuildFirstPart()
        {
        }
        private void BuildLast()
        {
            BuildFirst();
        }
        private void BuildLastPart()
        {
            BuildFirstPart();
        }
        private void BuildIgnore()
        {
            BuildFirst();
        }
        private void BuildMax()
        {
        }
        private void BuildStop()
        {
        }
        private void BuildStopNot()
        {
            words.RemoveAt(1);
            BuildStop();
        }
        private void BuildOrder()
        {
        }
        private void BuildEach()
        {
            BuildFirst();
        }
    }
}
