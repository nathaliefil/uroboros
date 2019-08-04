Commands
=====================

In Uroboros commands are structures consisting of keywords and definitions of data structures between them. Keywords are insensitive to case size. Semicolon is used to separate many commands. If command is the last command in code or in block of commands, it is not necessary to put semicolon after it. 


---

# **Core commands**

Core commands are the commands used for manipulating files and directories. Core commands may start with two additional keywords: **force to**. These keywords force command to be executed in all cases: for example if command is going to create new directory, forced will do that even if this directory already exists - the old one will be erased.


### Copy
| Name  | Structure |
| ------------- | ------------- |
| copy | **copy** (list)

This commands adds every file and directory from list (arg1) to clipboard. If clipboard already contains data, it is expanded by new elements.


### Copy To
| Name  | Structure |
| ------------- | ------------- |
| copy to | **copy** (list) **to** (text)

This commands copies every file and directory from list (arg1) into directory (arg2). It is done outside of the clipboard.
