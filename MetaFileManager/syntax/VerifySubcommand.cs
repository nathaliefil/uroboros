using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax
{
    partial class Syntax
    {
        private static bool VerifySubcommand(SubcommandType subtype, List<String> words)
        {
            switch (subtype)
            {
                case SubcommandType.Where:
                {
                    return VerifyWhere(words);
                }
                case SubcommandType.WhereNot:
                {
                    return VerifyWhereNot(words);
                }
                case SubcommandType.First:
                {
                    return VerifyFirst(words);
                }
                case SubcommandType.Last:
                {
                    return VerifyLast(words);
                }
                case SubcommandType.FirstPart:
                {
                    return VerifyFirstPart(words);
                }
                case SubcommandType.LastPart:
                {
                    return VerifyLastPart(words);
                }
                case SubcommandType.Ignore:
                {
                    return VerifyIgnore(words);
                }
                case SubcommandType.Max:
                {
                    return VerifyMax(words);
                }
                case SubcommandType.Stop:
                {
                    return VerifyStop(words);
                }
                case SubcommandType.StopNot:
                {
                    return VerifyStopNot(words);
                }
                case SubcommandType.Order:
                {
                    return VerifyOrder(words);
                }
                case SubcommandType.Each:
                {
                    return VerifyEach(words);
                }
            }
            return false;
        }
        private static bool VerifyWhere(List<String> words)
        {
            return true;
        }
        private static bool VerifyWhereNot(List<String> words)
        {
            words.RemoveAt(1);
            return VerifyWhere(words);
        }
        private static bool VerifyFirst(List<String> words)
        {
            if (words.Count > 2)
            {
                return false;
            }
            if (!words[1].All(char.IsDigit))
            {
                return false;
            }
            return true;
        }
        private static bool VerifyFirstPart(List<String> words)
        {
            return true;
        }
        private static bool VerifyLast(List<String> words)
        {
            return VerifyFirst(words);
        }
        private static bool VerifyLastPart(List<String> words)
        {
            return VerifyFirstPart(words);
        }
        private static bool VerifyIgnore(List<String> words)
        {
            return VerifyFirst(words);
        }
        private static bool VerifyMax(List<String> words)
        {
            return true;
        }
        private static bool VerifyStop(List<String> words)
        {
            return true;
        }
        private static bool VerifyStopNot(List<String> words)
        {
            words.RemoveAt(1);
            return VerifyStop(words);
        }
        private static bool VerifyOrder(List<String> words)
        {
            return true;
        }
        private static bool VerifyEach(List<String> words)
        {
            return VerifyFirst(words);
        }
    }
}
