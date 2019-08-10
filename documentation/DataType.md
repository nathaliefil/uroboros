Data Type
=====================

In Uroboros every data structure is one of five types: Logic, Number, Time, Text, List.
Data types in some situations can be treated as different ones. 
Logic type can be treated as: Number, Text, List.  
Number type can be treated as: Text, List.  
Time type can be treated as: Text, List.  
Text type can be treated as List.  
List type can be treated only as itself.  


---

# **Logic**

Logic type can have only two values: "true" and "false". When treated as number, "true" is returned as 1 and "false" is returned as 0. The same goes when treating it as text and list (returned is one-element list with text "0" or "1"). 


### Logic Constants

The easiest way to initiate value of logic type is to use two keywords: "true" and "false". They are insensitive to case size.

### Comparisons By Keywords

Two data structures of the same data type can be compared by the use of "is". If two values are the same, "true" is returned.

| Structure | Returns |
| --------- | ------- |
| [logic] **is** [logic] | (Logic) |
| [number] **is** [number] | (Logic) |
| [time] **is** [time] | (Logic) |
| [text] **is** [text] | (Logic) |
| [list] **is** [list] | (Logic) |

An analogous structure can be created using an additional key: "not". This is negation of equality - structure returns true if two values ((arg1) and (arg2)) are different.

| Structure | Returns |
| --------- | ------- |
| [logic] **is** **not** [logic] | (Logic) |
| [number] **is** **not** [number] | (Logic) |
| [time] **is** **not** [time] | (Logic) |
| [text] **is** **not** [text] | (Logic) |
| [list] **is** **not** [list] | (Logic) |

### Comparisons By Signs

Alternative way for checking equality is by the use of signs "=" or "==" (there is not difference between them).

| Structure | Returns |
| --------- | ------- |
| [logic] **=** [logic] | (Logic) |
| [number] **=** [number] | (Logic) |
| [time] **=** [time] | (Logic) |
| [text] **=** [text] | (Logic) |
| [list] **=** [list] | (Logic) |
| --------- | ------- |
| [logic] **==** [logic] | (Logic) |
| [number] **==** [number] | (Logic) |
| [time] **==** [time] | (Logic) |
| [text] **==** [text] | (Logic) |
| [list] **==** [list] | (Logic) |


Inequality can be checked by the use of "!=".

| Structure | Returns |
| --------- | ------- |
| [logic] **!=** [logic] | (Logic) |
| [number] **!=** [number] | (Logic) |
| [time] **!=** [time] | (Logic) |
| [text] **!=** [text] | (Logic) |
| [list] **!=** [list] | (Logic) |


Checking a smaller / larger value is done by using ">", "<", ">=", "<=". For logic and numeric type, result is intuitive comparison of numbers. For time type, it is checking which of two values is first chronologically. For text type, alphabetical order is used to dermine which text is "smaller". For list, compared is list length.


| Structure | Returns |
| --------- | ------- |
| [logic] **>** [logic] | (Logic) |
| [number] **>** [number] | (Logic) |
| [time] **>** [time] | (Logic) |
| [text] **>** [text] | (Logic) |
| [list] **>** [list] | (Logic) |
| Analogically for: ">", "<", ">=", "<=" |

