using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.variables.from_file;
namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByComparator
    {
        public static bool Equals(string s1, string s2, OrderByStruct order)
        {

            switch (order.GetVariable())
            {
                case OrderByVariable.Extension:
                    return FileInnerVariable.GetExtension(s1).Equals(FileInnerVariable.GetExtension(s2));

                case OrderByVariable.Fullname:
                    return FileInnerVariable.GetFullname(s1).Equals(FileInnerVariable.GetFullname(s2));

                case OrderByVariable.Name:
                    return FileInnerVariable.GetName(s1).Equals(FileInnerVariable.GetName(s2));

                case OrderByVariable.Size:
                    return FileInnerVariable.GetSize(s1).Equals(FileInnerVariable.GetSize(s2));

                case OrderByVariable.IsCorrect:
                    return FileValidator.IsNameCorrect(s1).Equals(FileValidator.IsNameCorrect(s2));

                case OrderByVariable.IsDirectory:
                    return FileValidator.IsDirectory(s1).Equals(FileValidator.IsDirectory(s2));

                case OrderByVariable.IsFile:
                    return FileValidator.IsDirectory(s1).Equals(FileValidator.IsDirectory(s2));

                case OrderByVariable.Access:
                    {
                        if (order is OrderByStructTime)
                            return DateExtractor.GetVariable(FileInnerVariable.GetAccess(s1), (order as OrderByStructTime).GetTimeVariable()) ==
                                DateExtractor.GetVariable(FileInnerVariable.GetAccess(s2), (order as OrderByStructTime).GetTimeVariable());
                        else if (order is OrderByStructDate)
                            return DateExtractor.DateToInt(FileInnerVariable.GetAccess(s1)).Equals(
                                DateExtractor.DateToInt(FileInnerVariable.GetAccess(s2)));
                        else if (order is OrderByStructClock)
                            return DateExtractor.ClockToInt(FileInnerVariable.GetAccess(s1)).Equals(
                                DateExtractor.ClockToInt(FileInnerVariable.GetAccess(s2)));
                        else
                            return FileInnerVariable.GetAccess(s1).Equals(FileInnerVariable.GetAccess(s2));
                    }

                case OrderByVariable.Creation:
                    {
                        if (order is OrderByStructTime)
                            return DateExtractor.GetVariable(FileInnerVariable.GetCreation(s1), (order as OrderByStructTime).GetTimeVariable()) ==
                                DateExtractor.GetVariable(FileInnerVariable.GetCreation(s2), (order as OrderByStructTime).GetTimeVariable());
                        else if (order is OrderByStructDate)
                            return DateExtractor.DateToInt(FileInnerVariable.GetCreation(s1)).Equals(
                                DateExtractor.DateToInt(FileInnerVariable.GetCreation(s2)));
                        else if (order is OrderByStructClock)
                            return DateExtractor.ClockToInt(FileInnerVariable.GetCreation(s1)).Equals(
                                DateExtractor.ClockToInt(FileInnerVariable.GetCreation(s2)));
                        else
                            return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetCreation(s2));
                    }

                case OrderByVariable.Modification:
                    {
                        if (order is OrderByStructTime)
                            return DateExtractor.GetVariable(FileInnerVariable.GetModification(s1), (order as OrderByStructTime).GetTimeVariable()) ==
                                DateExtractor.GetVariable(FileInnerVariable.GetModification(s2), (order as OrderByStructTime).GetTimeVariable());
                        else if (order is OrderByStructDate)
                            return DateExtractor.DateToInt(FileInnerVariable.GetModification(s1)).Equals(
                                DateExtractor.DateToInt(FileInnerVariable.GetModification(s2)));
                        else if (order is OrderByStructClock)
                            return DateExtractor.ClockToInt(FileInnerVariable.GetModification(s1)).Equals(
                                DateExtractor.ClockToInt(FileInnerVariable.GetModification(s2)));
                        else
                            return FileInnerVariable.GetModification(s1).Equals(FileInnerVariable.GetModification(s2));
                    }
            }
            return false;
        }
    }
}
