Quick Guide
=====================

Welcome to Quick Guide, the easiest way to start with Uroboros.

---

# **Let's Go**

Run gui application. When it is opened, you have to select one directory from your drive. It is necessary, because Uroboros actions are 
based on manipulating files and directory from selected location. You can select location by the top right button.

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
6 { print "hello world"; } 
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
"everything" returns all direcories and all files (in this order).


