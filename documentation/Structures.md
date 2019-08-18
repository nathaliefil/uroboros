Structures
=====================

Code written in Uroboros consists of many structures. All words and characters in code that are not comments are analyzed and 
interpreted.


### Comments

| Name  | Structure |
| ------------- | ------------- |
| single line comment | // comment content |
| multi line comment | /* comment content */|

Single line comment starts with two slashes //. Any code after it is not executed until new line.   
Multi line comment starts with slash and asterisk and ends with asterisk and slash. It can stop many lines of text from executing.

---

### Commands

| Structure | Returns |
| --------- | ------- |
| [command] | (Structure) |
| [command] ; [command] | (Structure) |
| [command] ; [command] ; [command] | (Structure) |
| analogically for more commands | |

The simplest structure is list of many commands. Semicolon is used to separate two commands. For only one commands there is 
no need for typing semicolon after it (but can be).

### If

| Structure | Returns |
| --------- | ------- |
| **if** [logic] { [structure] } | (Structure) |

Structure If executes structure (arg2) inside curly brackets only if condition (arg1) is satisfied.


### If Else

| Structure | Returns |
| --------- | ------- |
| **if** [logic] { [structure] } **else** { [structure] } | (Structure) |

Structure If Else executes structure (arg2) inside first block of curly brackets only if condition (arg1) is satisfied. 
If condition (arg1) is not satisfied, structure after keyword 'else' (arg3) is executed.

### While

| Structure | Returns |
| --------- | ------- |
| **while** [logic] { [structure] } | (Structure) |

Structure While executes structure (arg2) inside curly brackets in a loop as long as condition (arg1) is satisfied. 
This can lead to some dangers like [infinite loop](https://en.wikipedia.org/wiki/Infinite_loop). If condition (arg1) is not
satisfied in first touch, structure (arg2) is not executed at all.


### Numeric Loop

| Structure | Returns |
| --------- | ------- |
| [number] => [structure] | (Structure) |
| [number] { [structure] } | (Structure) |

Numeric Loop repeats execution of structure (arg2) for n-times, where n is value of rounded number (arg1). If number (arg1) is zero
or negative, structure (arg2) is not executed at all. Numeric Loop can be written in two ways - with curly brackets or with arrow sign.
Numeric loop with arrow sign behaves the same as with brackets, but cannot be used for executing many commands separated by semicolons.
In this case only first command will be executed in loop, so it is necessary to use brackets. When Numeric Loop starts executing, value
of [inner variable index](InnerVariables.md) is set to zero. After every iteration of loop index is increased by 1. If Numeric Loop is
finished, value of index returns to the value before start of execution.


### List Loop

| Structure | Returns |
| --------- | ------- |
| [list] => [structure] | (Structure) |
| [list] { [structure] } | (Structure) |

List Loop repeats execution of structure (arg2) for n-times, where n is length of list (arg1). If list (arg1) is empty,
structure (arg2) is not executed at all. List Loop can be written in two ways - with curly brackets or with arrow sigs.
List loop with arrow sign behaves the same as with brackets, but cannot be used for executing many commands separated by semicolons. 
In this case only first command will be executed in loop, so it is necessary to use brackets. When List Loop starts executing, 
value of [inner variable index](InnerVariables.md) is set to zero and value of [inner variable this](InnerVariables.md) 
is set to first element of list. After every iteration of loop index is increased by 1 and this is set to next list element. 
If List Loop is finished, value of index and this returns to the value before start of execution.


### Inside

| Structure | Returns |
| --------- | ------- |
| **inside** [list] { [structure] } | (Structure) |

To do

