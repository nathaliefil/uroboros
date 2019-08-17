﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.gui
{
    class HighlightKeywords
    {
        public static string[] CARDINAL = new string[]{"add", "ask", "clear bin", "clear clipboard", "clear log", "copy", "create", 
            "cut", "delete", "directory", "drop", "file", "log off", "log on", "move", "open", "order", "paste", 
            "print", "reaccess", "recreate", "remodify", "remove", "rename", "reverse", "run", "select", "sleep", "swap", 
            "uroboros stop"};

        public static string[] USUAL = new string[]{"after", "and", "asc", "before", "by", "desc", 
            "each", "else", "empty list", "first", "for", "force", "from", "if", "in", "inside", "is", "last", 
            "like", "not", "or", "order by", "skip", "to", "unique", "where", "while", "with", "without", "xor"};

        public static string[] INNER_VARIABLES = new string[]{"creation", "directories", 
            "empty", "exist", "extension", "everything", "false", "files", "fullname", "index", 
            "location", "modification", "name", "size", "this", "true", "creation.year", "creation.month", 
            "creation.month", "creation.weekday", "creation.day", "creation.hour", "creation.minute", 
            "creation.second", "modification.year", "modifiction.month", "modification.weekday", 
            "modification.day", "modification.hour", "modification.minute", "modification.second"};
    }
}
