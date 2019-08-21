Quick Guide
=====================

Welcome to Quick Guide, the easiest way to start with Uroboros.

---

# **Let's Go**

Run gui application. When it is opened, you have to select one directory from your drive. It is necessary, because Uroboros actions are 
based on manipulating files and directories from selected location. You can select location by the top right button.

![Start image not found](documentation/resources/start.png)

When location is selected, everything is ready.

---

# **First Code**

```
print "hello world";
```

Try to run this code. This is Print, the basic command. It can print anything in log - texts, lists, numbers, time.

```
"hello world";
```

Print is default command and you can omit first keyword, however it is recommended to keep it for greater readability. Two first codes
make the same result.

```
print 2+2*2;
```

Print can be used to print numbers and operations on them (+,-,*,/,%).

```
print "2+2*2 = " + 2+2*2;
```

Text and numbers can be merged by the use of pluses.

```
6 => print "hello world";
```
If you want to repeat one command several times, you can use Big Arrow or curly brackets. This structure is called Numeric Loop.

```
6 { 
   print "hello world"; 
} 
```
Curly brackets allow to repeat more than one command, for example:

```
6 { 
   print "hello"; 
   print " world"; 
} 
```

As you can see, two commands are executed after each other six times.

```
2 => 2 => print "hello";
```

Big arrows and curly brackets can be nested in various configurations. 

```
10 => print "Index = " + index;
```

Numeric Loops have one variable related with them - "index". Index is number of current turnover of loop, starting from 0. If loops
are nested, index refers to the last one.

```
print files;
```

Variable "files" returns list of all files from current location (location selected at the beginning of the program). If there are no
files, nothing is printed from command above. Location can be changed by user any time.

```
print "-------------------- directories:";
print directories;
print "-------------------- files:";
print files;
print "-------------------- everything:";
print everything;
```

The other two important variables are "directories" and "everything". Variable "directories" returns all directories and variable
"everything" returns all directories and all files (in this order).

```
files => print "file";
```

List Loop is a structure similar to Numeric Loop. It can be created by use of Big Arrow or curly brackets. It performs command for
every element in list. So for example, if list contains 20 elements, command will be executed 20 times.

```
files => print this;
```

List Loops have one variable related with them - "this". It is name of current taken element from list. List Loops, like Numeric Loops,
use variable "index".

```
files => print "File No. " + index + ": " + this;
```

As you can see, both "index" and "this" change for every element from list.

```
files { 
   print "File = " + this;
   print "  Extension = " + extension;
   print "  Size = " + size;
   print "  Creation Year = " + creation.year;
   print "  Creation Month = " + month(creation.month);
}
```

List Loop can be used to extract properties of files, as seen above. If you want to know more about properties of files, go to 
[inner variables](documentation/InnerVariables.md).

```
v = 2 * 3;
v *= 4;
s = "V equals " + v;
print v;
print s;
```

Uroboros allows to use variables and they are (like all data structures) of five types: Logic, Number, Time, Text, List. For more
information about data types go to [data types](documentation/DataTypes.md). Variables are dynamically typed.

```
files{
   print size + "  " + this;
   if size > 1MB{
      print "  It is so big!";
   }
}
```

Structure If executes commands in curly brackets only if condition is satisfied.

```
files{
   print size + "  " + this;
   if size > 1MB{
      print "  It is so big!";
   }
   else{
      print "  It is small.";
   }
}
```

Aditional structure Else executes commands only if condition in If is not satisfied.

```
variable = 1;

while variable < 10{
   print variable;
   variable++;
   sleep 0.1;
}
```

Structure While executes commands in loop as long as condition is satisfied.




