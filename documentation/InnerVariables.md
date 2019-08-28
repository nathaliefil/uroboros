Inner Variables
=====================

Uroboros contains several built-in variables. It is not possible for user to create new usual variable with the same name.

# Two Most Important Ones

Of all inner variables, two of them are the most important. They are 'this' and 'index'.


### This
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| this | [text] | **true**  |

Initial value of this variable is empty text (""). However, during iterating over list of text, value of this is the name of actual taken element. It is crutial for subcommand Where and other structures like List Loop, Small Arrow Function. Values of file property variables always refer to file/directory with name taken from variable this and in location taken from variable wholelocation. If user sets his own value for variable 'this', it is possible for him to get property like creation date, size, extension from file/directory.


### Index
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| index | [number] | **true**  |

Index is numeric variable with initial value 0. During iterating over list of texts, in subcommand where or in Numeric Loop, value of index is increased by 1 after every iteration. Value of index is thus number of current iteration element in loop (starting from zero). For example, if some list has 5 elements, value of variable index will be in sequence: 0, 1, 2, 3, 4. When iterating is over, value of index goes back to pre-iteration value (usually: 0).


# File Path Variables

### Location
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| location | [text] | **true**  |

Location is text variable and its value is path to current working directory. Initially it is set to value taken from upper text box. Value of location can be changed during running code. After every change variable 'path' is cleaned. This variable is pivotal for all commands which modify files and directories, because all their names are relative to actual working directory. Location is changed - they cannot be found.


### Path
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| path | [text] | **false**  |

Path is text variable and its value is initially set to "\\". When structure Inside is executed, value of 'path' is expanded by new directories divided by backslashes. Value of "path" can be interpreted as the current relative path exapanded from 'location'.


### WholeLocation
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| wholelocation | [text] | **false**  |

Value of this variable is sum of variables "location" and "path". This is the real current location. Inside this location all core commands are performed.



# List variables


### Directories
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| directories | [list] | **false**  |

This variable is list of all directories in current working location.


### Everything
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| everything | [list] | **false**  |

This variable is list of all directories and files in current working location.


### Files
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| files | [list] | **false**  |

This variable is list of all files in current working location.


# File / Directory Property Variables

These variables return information about file/directory, which name is value from variable 'this' and is located in path from variable 'location'.


### Access
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| access | [time] | **false**  |

This variable is time of last access to file or directory.


### Creation
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| creation | [time] | **false**  |

This variable is creation time of file or directory.


### Empty
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| empty | [logic] | **false**  |

This variable returns true if file or directory is empty. If it does not exist, true is returned.


### Exist
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| exist | [logic] | **false**  |

This variable returns true if file or directory with name from variable 'this' exists.


### Extension
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| extension | [text] | **false**  |

This variable returns extension of file. If current element is directory, empty text is returned.


### Fullname
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| fullname | [text] | **false**  |

This variable returns name of file/directory including its extension.


### IsCorrect
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| iscorrect | [logic] | **false**  |

This function returns true if name of current element is proper for file or directory - do not contain not allowed characters: '\\', ':', '&', '*', '"', '<', '>'.


### IsDirectory
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| isdirectory | [logic] | **false**  |

This function returns true if current element is directory.


### IsFile
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| isfile | [logic] | **false**  |

This function returns true if current element is file.


### Modification
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| modification | [time] | **false**  |

This variable is modification time of file or directory.


### Name
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| name | [text] | **false**  |

This variable returns name of file/directory excluding its extension.


### Size
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| size | [number] | **false**  |

This variable returns size of file/directory in bytes.


# Other


### Now
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| now | [time] | **false**  |

This variable is current system time.


### Success
| Name  | Type | Modificable |
| ----- | -----| ----------- |
| success | [logic] | **false**  |

Value of this variable is set to "false" initially. After every execution of core command value is changed - it is "true" if command was executed without problems. Otherwise value is set to "false".

