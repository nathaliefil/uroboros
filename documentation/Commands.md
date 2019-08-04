Commands
=====================

In Uroboros commands are structures consisting of keywords and data structures between them. Keywords are insensitive to case size. Semicolon is used to separate many commands. If command is the last command in code or in block of commands, it is not necessary to put semicolon after it. 


---

# **Core commands**

Core commands are used for manipulating files and directories. Core commands may start with two additional keywords: **force to**. These keywords force command to be executed in all cases: for example if command is going to create new directory, forced will do that even if this directory already exists - the old one will be erased in this case.
First argument of every core command is [list]. This argument can be ommited leaving free space - in this case the value of variable "this" is used instead of declared list.


### Copy
| Name  | Structure |
| ------------- | ------------- |
| copy | **copy** [list] |

This command adds every file and directory from list (arg1) to clipboard. If clipboard already contains data, it is expanded by new elements.


### Copy To
| Name  | Structure |
| ------------- | ------------- |
| copy to | **copy** [list] **to** [text] |

This command copies every file and directory from list (arg1) into directory (arg2). This is done without use of the clipboard.


### Create Directory
| Name  | Structure |
| ------------- | ------------- |
| create directory | **create directory** [list] |

This command creates new directory for each element of list (arg1).


### Create Directory From
| Name  | Structure |
| ------------- | ------------- |
| create directory from | **create directory** [list] **from** [text] |

This command creates new directory for each element of list (arg1) and its content are files and directories taken from directory (arg2).


### Create File
| Name  | Structure |
| ------------- | ------------- |
| create directory | **create file** [list] |

This command creates new file for each element of list (arg1).


### Create File From
| Name  | Structure |
| ------------- | ------------- |
| create file from | **create file** [list] **from** [text] |

This command creates new file for each element of list (arg1) and it is copy of file (arg2).


### Cut
| Name  | Structure |
| ------------- | ------------- |
| cut | **cut** [list] |

This command adds every file and directory from list (arg1) to cutting clipboard. If clipboard already contains data, it is expanded by new elements. If elements in clipboard are copied, their state is chnged to cut.


### Delete
| Name  | Structure |
| ------------- | ------------- |
| delete | **delete** [list] |

This command moves every file and directory from list (arg1) to rubbish bin.


### Drop
| Name  | Structure |
| ------------- | ------------- |
| drop | **drop** [list] |

This command deletes "without return" every file and directory from list (arg1). Sometimes it is not possible to rescue deleted files and directories.


### Move To
| Name  | Structure |
| ------------- | ------------- |
| move to | **move** [list] **to** [text] |
|         | **cut** [list] **to** [text] |

This command moves every file and directory from list (arg1) into directory (arg2). This is done without use of the clipboard.


### Open
| Name  | Structure |
| ------------- | ------------- |
| open | **open** [list] |

This command opens every file and directory from list (arg1) in default opening application.


### Reaccess To
| Name  | Structure |
| ------------- | ------------- |
| reaccess to | **reaccess** [list] **to** [time] |

This command changes time of last access of every file and directory from list (arg1) to new time (arg2).


### Recreate To
| Name  | Structure |
| ------------- | ------------- |
| recreate to | **recreate** [list] **to** [time] |

This command changes creation time of every file and directory from list (arg1) to new time (arg2).


### Remodify To
| Name  | Structure |
| ------------- | ------------- |
| remodify to | **remodify** [list] **to** [time] |

This command changes modification time of every file and directory from list (arg1) to new time (arg2).


### Rename To
| Name  | Structure |
| ------------- | ------------- |
| rename to | **rename** [list] **to** [text] |

This command renames every file and directory from list (arg1). New name is (arg2).


---

# **List commands**

List commands are used to modify existing list variables.

### Add to
| Name  | Structure |
| ------------- | ------------- |
| add to | **add** [list] **to** [list variable] |
|        | **add** [list] **to** [text variable] |

This command adds new elements (arg1) at the end of existing list variable (arg2). First argument of command can be ommited leaving free space - in this case the value of variable "this" is used instead of declared list. Second argument can be [list variable] or [text variable]. If it is [text variable], this command changes type of variable from [text] to [list] and after that cannot be treated as [text].


### Order by
| Name  | Structure |
| ------------- | ------------- |
| order by | **order** [list variable] **by** [order by variables] 

This command orders list (arg1) by variables (arg2). For more information visit "OrderBy.md".


### Remove from
| Name  | Structure |
| ------------- | ------------- |
| remove from | **remove** [list] **from** [list variable] |

This command removes elements (arg1) from list variable (arg2). First argument of command can be ommited leaving free space - in this case the value of variable "this" is used instead of declared list.


### Reverse
| Name  | Structure |
| ------------- | ------------- |
| reverse | **reverse** [list variable]

This command reverses order of elements in list variable (arg1).


# **Arithmetic commands**

Arithmetic commands are used to modify values of numeric variables.

| Structure | Behavior |
| ------------- | ------------- | 
| [numeric variable] **+=** [number] | Increment variable (arg1) by (arg2). |
| [numeric variable] **-=** [number] | Decrement variable (arg1) by (arg2). |
| [numeric variable] * **=** [number] | Multiply variable (arg1) by (arg2). |
| [numeric variable] **/=** [number] | Divide variable (arg1) by (arg2). |
| [numeric variable] **%=** [number] | Set value to variable: (arg1) modulo (arg2). |
| [numeric variable] **++** | Increment variable (arg1) by 1. |
| [numeric variable] **--** | Decrement variable (arg1) by 1. |


# **Variable declaration commands**

