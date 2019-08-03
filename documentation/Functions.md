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

### Average
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| average, avg, mean  | (Number, Number, ...) | (Number)

This function returns arithmetic mean of all of its arguments. Can have any number of numeric arguments, but at least one.


### Ceil
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| ceil, ceiling  | (Number) | (Number)

This function returns the least integer greater than or equal to (arg1).


### Count
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| count  | (List) | (Number)

This function returns number of elements in a list (arg1).


### E
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| e  | () | (Number)

This function returns Euler's number - the base of the natural logarithm.


### Floor
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| floor  | (Number) | (Number)

This function returns the greatest integer less than or equal to (arg1).


### GoldenRatio
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| goldenratio  | () | (Number)

This function returns golden ratio/golden mean - sum of 1 and square root of 5 divided by 2.


### IndexOf
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| indexof  | (Text, Text) | (Number)

This function returns the zero-based index of the first occurrence of a text (arg2) within the base text (arg1). If text (arg2) cannot be found inside base text (arg1), -1 is returned.


### Length
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| length  | (Text) | (Number)

This function returns length of text (arg1) - number of characters. If text (arg1) is empty, 0 is returned.


### LengthOfLongest
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| lengthoflongest  | (List) | (Number)

This function returns length of the longest text in a list (arg1).


### LengthOfShortest
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| lengthofshortest  | (List) | (Number)

This function returns length of the shortest text in a list (arg1).


### Ln
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| ln, log  | (Number) | (Number)

This function returns natural logarithm of a number (arg1).


### Log10
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| log10  | (Number) | (Number)

This function returns decimal logarithm of a number (arg1).


### Max
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| max  | (Number, Number, ...) | (Number)

This function returns the biggest of its arguments. Can have any number of numeric arguments, but at least one.


### Min
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| min  | (Number, Number, ...) | (Number)

This function returns the smallest of its arguments. Can have any number of numeric arguments, but at least one.


### Length
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| length  | (Text) | (Number)

This function parses text (arg0) into a number. If parsing cannot be executed successfully, returns 0.


### Pi
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| pi  | () | (Number)

This function number pi - the ratio of a circle's circumference to its diameter.


### Power
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| pow, power  | (Number, Number) | (Number)

This function returns number (arg0) to the power of (arg1).


### Product
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| product  | (Number, Number, ...) | (Number)

This function returns product of all of its arguments. Can have any number of numeric arguments, but at least one.


### Round
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| round  | (Number) | (Number)

This function returns number (arg1) rounded to the nearest integer.


### Sqrt
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| sqrt  | (Number) | (Number)

This function returns square root of a number (arg1).


### Sum
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| sum  | (Number, Number, ...) | (Number)

This function returns sum of all of its arguments. Can have any number of numeric arguments, but at least one.


### Year
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| year  | (Text) | (Number)

This function finds string of 4 consecutive numbers inside text (arg1). If found, returns it as a number. Otherwise returns 0.


### YearDay
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| yearday  | (Time) | (Number)

This function returns, which day of the year is date (arg1).

---

# **Time Functions**

### Christmas
