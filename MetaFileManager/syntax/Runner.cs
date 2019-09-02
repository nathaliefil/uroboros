using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.structures.abstracts;
using Uroboros.syntax.commands.structures;
using Uroboros.syntax.structures;
using Uroboros.syntax.runtime;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation;
using Uroboros.syntax.log;

namespace Uroboros.syntax
{
    class Runner
    {
        public static void Run(string code, string location)
        {
            Logger.GetInstance().LogOn();
            InterVariables.GetInstance().Clear();
            RuntimeVariables.GetInstance().InitializeInnerVariables();
            RuntimeVariables.GetInstance().Actualize("location", location);

            try
            {
                List<Token> tokens = Reader.CreateTokenlist(code);
                List<ICommand> commands = CommandListFactory.Build(tokens);
                RunCommands(commands);
            }
            catch (Uroboros.syntax.SyntaxErrorException te)
            {
                Logger.GetInstance().LogSyntaxError(te.GetMessage());
            }
        }


        public static void RunCommands(List <ICommand> commands)
        {
            List<Structure> structures = new List<Structure>();
            bool jumpIntoElse = false;

            try
            {
                int pointer = 0;

                while (pointer < commands.Count())
                {
                    ICommand takenCommand = commands[pointer];

                    if (takenCommand is BracketOn)
                    {
                        RuntimeVariables.GetInstance().BracketsUp();

                        if (takenCommand is EmptyOpenning)
                            structures.Add(new EmptyBlock());
                        else if (takenCommand is IfOpenning)
                        {
                            if (jumpIntoElse)
                                jumpIntoElse = false;

                            if ((takenCommand as IfOpenning).ToBool())
                                structures.Add(new If());
                            else
                            {
                                pointer = JumpOverBlockOfCode(commands, pointer);
                                jumpIntoElse = true;
                            }
                        }
                        else if (takenCommand is ElseOpenning)
                        {
                            if (jumpIntoElse)
                            {
                                structures.Add(new Else());
                                jumpIntoElse = false;
                            }
                            else
                                pointer = JumpOverBlockOfCode(commands, pointer);
                        }

                        else if (takenCommand is WhileOpenning)
                        {
                            if ((takenCommand as WhileOpenning).ToBool())
                                structures.Add(new While((takenCommand as WhileOpenning).GetCondition(), (takenCommand as BracketOn).GetCommandNumber()));
                            else
                                pointer = JumpOverBlockOfCode(commands, pointer);

                            if (jumpIntoElse)
                                jumpIntoElse = false;
                        }
                        else if (takenCommand is InsideOpenning)
                        {
                            List<string> list = (takenCommand as InsideOpenning).ToList();
                            if (list.Count == 0)
                                pointer = JumpOverBlockOfCode(commands, pointer);
                            else
                            {
                                string value = list[0];
                                list.RemoveAt(0);
                                structures.Add(new Inside(list, (takenCommand as BracketOn).GetCommandNumber()));

                                RuntimeVariables.GetInstance().Actualize("this", value);
                                RuntimeVariables.GetInstance().ExpandLocation(value);
                                RuntimeVariables.GetInstance().Actualize("index", 0);
                            }

                            if (jumpIntoElse)
                                jumpIntoElse = false;
                        }
                        else if (takenCommand is ListLoopOpenning)
                        {
                            List<string> list = (takenCommand as ListLoopOpenning).ToList();
                            if (list.Count == 0)
                                pointer = JumpOverBlockOfCode(commands, pointer);
                            else
                            {
                                string value = list[0];
                                list.RemoveAt(0);
                                structures.Add(new ListLoop(list, (takenCommand as BracketOn).GetCommandNumber()));

                                RuntimeVariables.GetInstance().Actualize("this", value);
                                RuntimeVariables.GetInstance().Actualize("index", 0);
                            }

                            if (jumpIntoElse)
                                jumpIntoElse = false;
                        }
                        else if (takenCommand is NumericLoopOpenning)
                        {
                            int repeats = (int)(takenCommand as NumericLoopOpenning).ToNumber();
                            if (repeats <= 0)
                                pointer = JumpOverBlockOfCode(commands, pointer);
                            else
                            {
                                repeats--;
                                structures.Add(new NumericLoop(repeats, (takenCommand as BracketOn).GetCommandNumber()));

                                RuntimeVariables.GetInstance().Actualize("index", 0);
                            }

                            if (jumpIntoElse)
                                jumpIntoElse = false;
                        }
                    }
                    else if (takenCommand is BracketOff)
                    {
                        RuntimeVariables.GetInstance().BracketsDown();

                        if (structures.Count == 0)
                            throw new RuntimeException("ERROR! Brackets are wrong.");

                        Structure lastStructure = structures.Last();

                        if (lastStructure is ILoopingStructure)
                        {
                            bool iterateOneMoreTime = lastStructure.HasNext();

                            if (iterateOneMoreTime)
                            {
                                pointer = lastStructure.GetCommandNumber();
                                RuntimeVariables.GetInstance().BracketsUp();
                            }
                            else
                                structures.RemoveAt(structures.Count - 1);
                        }
                        else
                            structures.RemoveAt(structures.Count - 1);
                    }
                    else
                        commands[pointer].Run();

                    pointer++;
                }
            }
            catch (Uroboros.syntax.RuntimeException re)
            {
                Logger.GetInstance().LogRuntimeError(re.GetMessage());
            }
        }

        private static int JumpOverBlockOfCode(List<ICommand> commands, int oldPosition)
        {
            RuntimeVariables.GetInstance().BracketsDown();

            int level = 0;
            int newPosition = oldPosition + 1;

            while (newPosition < commands.Count())
            {
                ICommand takenCommand = commands[newPosition];

                if (takenCommand is BracketOn)
                    level++;

                if (takenCommand is BracketOff)
                {
                    level--;
                    if (level == -1)
                        return newPosition;
                }
                newPosition++;
            }
            return newPosition;
        }
    }
}
