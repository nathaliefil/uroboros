using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.variables.from_location;
using Uroboros.syntax.variables.from_location.date;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByComparator
    {
        public static bool Equals(string s1, string s2, OrderByVariable variable)
        {

            switch (variable)
            {
                case OrderByVariable.Creation:
                    return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetCreation(s2));

                case OrderByVariable.Extension:
                    return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetExtension(s2));

                case OrderByVariable.Fullname:
                    return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetFullname(s2));

                case OrderByVariable.Modification:
                    return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetModification(s2));

                case OrderByVariable.Name:
                    return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetName(s2));

                case OrderByVariable.Size:
                    return FileInnerVariable.GetCreation(s1).Equals(FileInnerVariable.GetSize(s2));




                case OrderByVariable.CreationYear:
                    return DateExtractor.GetVariable(DateVariableType.Year, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.Year, FileInnerVariable.GetCreation(s2));

                case OrderByVariable.CreationMonth:
                    return DateExtractor.GetVariable(DateVariableType.Month, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.Month, FileInnerVariable.GetCreation(s2));

                case OrderByVariable.CreationWeekDay:
                    return DateExtractor.GetVariable(DateVariableType.WeekDay, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.WeekDay, FileInnerVariable.GetCreation(s2));

                case OrderByVariable.CreationDay:
                    return DateExtractor.GetVariable(DateVariableType.Day, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.Day, FileInnerVariable.GetCreation(s2));

                case OrderByVariable.CreationHour:
                    return DateExtractor.GetVariable(DateVariableType.Hour, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.Hour, FileInnerVariable.GetCreation(s2));

                case OrderByVariable.CreationMinute:
                    return DateExtractor.GetVariable(DateVariableType.Minute, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.Minute, FileInnerVariable.GetCreation(s2));

                case OrderByVariable.CreationSecond:
                    return DateExtractor.GetVariable(DateVariableType.Second, FileInnerVariable.GetCreation(s1))
                        == DateExtractor.GetVariable(DateVariableType.Second, FileInnerVariable.GetCreation(s2));




                case OrderByVariable.ModificationYear:
                    return DateExtractor.GetVariable(DateVariableType.Year, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.Year, FileInnerVariable.GetModification(s2));

                case OrderByVariable.ModificationMonth:
                    return DateExtractor.GetVariable(DateVariableType.Month, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.Month, FileInnerVariable.GetModification(s2));

                case OrderByVariable.ModificationWeekDay:
                    return DateExtractor.GetVariable(DateVariableType.WeekDay, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.WeekDay, FileInnerVariable.GetModification(s2));

                case OrderByVariable.ModificationDay:
                    return DateExtractor.GetVariable(DateVariableType.Day, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.Day, FileInnerVariable.GetModification(s2));

                case OrderByVariable.ModificationHour:
                    return DateExtractor.GetVariable(DateVariableType.Hour, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.Hour, FileInnerVariable.GetModification(s2));

                case OrderByVariable.ModificationMinute:
                    return DateExtractor.GetVariable(DateVariableType.Minute, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.Minute, FileInnerVariable.GetModification(s2));

                case OrderByVariable.ModificationSecond:
                    return DateExtractor.GetVariable(DateVariableType.Second, FileInnerVariable.GetModification(s1))
                        == DateExtractor.GetVariable(DateVariableType.Second, FileInnerVariable.GetModification(s2));

            }

            return false;
        }
    }
}
