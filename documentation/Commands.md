Commands
=====================

In Uroboros commands are structures consisting of keywords and definitions of data structures between them. Keywords are insensitive to case size. Semicolon is used to separate many commands. If command is the last command in code or in block of commands, it is not necessary to put semicolon after it. 


---

# **Core commands**

Core commands are the commands used for manipulating files and directories. Core commands may start with two additional keywords: **force to**. These keywords force command to be executed in all cases: for example if command is going to create new directory, forced will do that even if this directory already exists - the old one will be erased.
First argument of every core command is [list]. This argument can be ommited leaving free space - in this case the value of variable "this" is used instead of declared list.


### Copy
| Name  | Structure |
| ------------- | ------------- |
| copy | **copy** [list] |

This commands adds every file and directory from list (arg1) to clipboard. If clipboard already contains data, it is expanded by new elements.


### Copy To
| Name  | Structure |
| ------------- | ------------- |
| copy to | **copy** [list]) **to** [text] |

This commands copies every file and directory from list (arg1) into directory (arg2). This is done without use of the clipboard.


### Create Directory
| Name  | Structure |
| ------------- | ------------- |
| create directory | **create directory** (list) |

This commands creates new directory for each element of list (arg1).


### Create Directory From
| Name  | Structure |
| ------------- | ------------- |
| create directory from | **create directory** (list) **from** (text) |

This commands creates new directory for each element of list (arg1) and its content are files and directories taken from directory (arg2).


### Create File
| Name  | Structure |
| ------------- | ------------- |
| create directory | **create file** (list) |

This commands creates new file for each element of list (arg1).


### Create File From
| Name  | Structure |
| ------------- | ------------- |
| create file from | **create file** (list) **from** (text) |

This commands creates new file for each element of list (arg1) and it is copy of file (arg2).


### Cut
| Name  | Structure |
| ------------- | ------------- |
| cut | **cut** (list) |

This commands adds every file and directory from list (arg1) to cutting clipboard. If clipboard already contains data, it is expanded by new elements.


### Delete
| Name  | Structure |
| ------------- | ------------- |
| delete | **delete** (list) |

This commands moves every file and directory from list (arg1) to rubbish bin.


### Drop
| Name  | Structure |
| ------------- | ------------- |
| drop | **drop** (list) |

This commands deletes "without return" every file and directory from list (arg1). Sometimes it is not possible to rescue deleted files and directories.


### Move To
| Name  | Structure |
| ------------- | ------------- |
| move to | **move** (list) **to** (text) |
|         | **cut** (list) **to** (text) |

This commands moves every file and directory from list (arg1) into directory (arg2). This is done without use of the clipboard.


### Open
| Name  | Structure |
| ------------- | ------------- |
| open | **open** (list) |

This commands opens every file and directory from list (arg1) in default opening application.


### Reaccess To
| Name  | Structure |
| ------------- | ------------- |
| reaccess to | **reaccess** (list) **to** (time) |

This commands rename every file and directory from list (arg1). New name is (arg2).


### Rename To
| Name  | Structure |
| ------------- | ------------- |
| rename to | **rename** (list) **to** (text) |

This commands rename every file and directory from list (arg1). New name is (arg2).


---

# **List commands**

List commands are used to modify existing list variable.
