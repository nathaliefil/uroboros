Inner Variables
=====================

Uroboros contains several built-in variables. It is not possible for user to create new usual variable with the same name.

# Three Most Important Ones

Of all inner variables, three of them are the most important. They are: location, this, index.

### Location
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| location | [text] | **true**  |

Location is text variable and its value is path to current working directory. Initially it is set to value taken from upper text box. Value of location can be changed during running code. This variable is pivotal for all commands which modify files and directories, because all their names are relative to actual working directory. Location is changed - they cannot be found.


### This
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| this | [text] | **true**  |

Initial value of this variable is empty text (""). However, during iterating over list of text, value of this is the name of actual taken element. It is crutial for subcommand Where and other structures like List Loop, Small Arrow Function. Values of file property variables always refer to file/directory with name taken from variable this and in location taken from variable location. If user sets own value for variable this, it is possible for him to get file/directory property like creation date, size, extension.


### Index
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| index | [number] | **true**  |

Index is numeric variable with initial value 0. During iterating over list of texts, in subcommand where or in Numeric Loop, value of index is increased by 1 after every iteration. Value of index is thus number of current iteration element in loop (starting from zero). For example, if some list has 5 elements, value of variable index will be in sequence: 0, 1, 2, 3, 4. When iterating is over, value of index goes back to pre-iteration value (usually: 0).



# File Property Variables



### Creation
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| copy | [time] | **false**  |

This variable is creation date of file or directory.


### Directories
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| directories | [list] | **false**  |

This variable is list of all directories in current working location.


### Empty
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| empty | [logic] | **false**  |

This variable returns true if file or directory with name from variable "this" in location taken from variable "location" is empty.


### Everything
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| everything | [list] | **false**  |

This variable is list of all files and directories in location taken from variable "location".



//more to do
Access
Exist
Extension
Files
Fullname
Modification
Name
Now
Size
index
location
this
