Functions
=====================

In Uroboros name of function is insensitive to case size. Some function may also have many alternative names: for example function Average can be called as "average", "avg" or "mean". 

---

# **Logic Functions**

### Contain
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| contain, contains  | (List, Text) | (Logic)

This function returns true if text (arg2) appears at least one time in a list (arg1). Otherwise it returns false.



### Empty
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| empty  | (List) | (Logic)

This function returns true if list (arg1) do not have any element inside. Otherwise it returns false.



### Exist
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| exist, exists  | (Text) | (Logic)

This function returns true if a file or a catalog of name (arg1) exists in current location. Otherwise it returns false.



### ExistInside
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| existinside, existsinside  | (Text, Text) | (Logic)

This function returns true if a file or a catalog of name (arg1) exists in directory of name (arg2) in current location. Otherwise it returns false. If directory (arg2) do not exist, also false is returned.



---

# **Numeric Functions**
