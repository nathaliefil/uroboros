Functions
=====================

In Uroboros name of function is insensitive to case size. Some function may also have alternative names: for example function Average can be called as "average", "avg" or "mean". 

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
| empty  | (Text) | (Logic)

This function returns true if file or directory with specific name (arg1) is empty. Otherwise it returns false.


### EmptyList
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| emptylist, listisempty  | (List) | (Logic)

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


### IsCorrect
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| iscorrect  | (Text) | (Logic)

This function returns true if name (arg0) is proper for file or directory - do not contain not allowed characters: '\\', ':', '&', '*', '"', '<', '>'.


### IsDirectory
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| iscorrect  | (Text) | (Logic)

This function returns true if name (arg0) could be name for directory - do not contain any dots.

### IsFile
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| iscorrect  | (Text) | (Logic)

This function returns true if name (arg0) could be name for file - contains at least one dot.


---

# **Numeric Functions**

### Average
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| average, avg, mean  | (Number, Number, ... ) | (Number)

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


### DaysBetween
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| daysbetween  | (Time, Time) | (Number)

This function returns number of days (as integer) between two dates: (arg1) and (arg2).


### E
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| e  | ( ) | (Number)

This function returns Euler's number - the base of the natural logarithm.


### Floor
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| floor  | (Number) | (Number)

This function returns the greatest integer less than or equal to (arg1).


### Gb
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| gb  | (Number) | (Number)

This function returns number of bytes in (arg1) of gigabytes.


### GoldenRatio
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| goldenratio  | ( ) | (Number)

This function returns golden ratio/golden mean - sum of 1 and square root of 5 divided by 2.


### HoursBetween
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| hoursbetween  | (Time, Time) | (Number)

This function returns number of hours (as integer) between two dates: (arg1) and (arg2).


### IndexOf
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| indexof  | (Text, Text) | (Number)

This function returns the zero-based index of the first occurrence of a text (arg2) within the base text (arg1). If text (arg2) cannot be found inside base text (arg1), -1 is returned.


### Kb
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| kb  | (Number) | (Number)

This function returns number of bytes in (arg1) of kilobytes.

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
| max  | (Number, Number, ... ) | (Number)

This function returns the biggest of its arguments. Can have any number of numeric arguments, but at least one.


### Mb
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| mb  | (Number) | (Number)

This function returns number of bytes in (arg1) of megabytes.


### Min
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| min  | (Number, Number, ... ) | (Number)

This function returns the smallest of its arguments. Can have any number of numeric arguments, but at least one.


### MinutesBetween
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| minutesbetween  | (Time, Time) | (Number)

This function returns number of minutes (as integer) between two dates: (arg1) and (arg2).


### MonthsBetween
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| monthsbetween  | (Time, Time) | (Number)

This function returns number of months (as integer) between two dates: (arg1) and (arg2).


### Number
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| number  | (Text) | (Number)

This function parses text (arg1) into a number. If parsing cannot be executed successfully, returns 0.


### Pb
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| pb  | (Number) | (Number)

This function returns number of bytes in (arg1) of petabytes.


### Pi
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| pi  | ( ) | (Number)

This function returns number pi - the ratio of a circle's circumference to its diameter.


### Power
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| pow, power  | (Number, Number) | (Number)

This function returns number (arg1) to the power of (arg2).


### Product
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| product  | (Number, Number, ... ) | (Number)

This function returns product of all of its arguments. Can have any number of numeric arguments, but at least one.


### Round
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| round  | (Number) | (Number)

This function returns number (arg1) rounded to the nearest integer.


### SecondsBetween
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| secondsbetween  | (Time, Time) | (Number)

This function returns number of seconds (as integer) between two dates: (arg1) and (arg2).


### Size
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| size | (Text) | (Number)

This function returns size of file or directory (in bytes) with specific name (arg1) in current location.


### Sqrt
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| sqrt  | (Number) | (Number)

This function returns square root of a number (arg1).


### Sum
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| sum  | (Number, Number, ... ) | (Number)

This function returns sum of all of its arguments. Can have any number of numeric arguments, but at least one.


### Tb
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| tb  | (Number) | (Number)

This function returns number of bytes in (arg1) of terabytes.


### Year
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| year  | (Text) | (Number)

This function finds string of 4 consecutive numbers inside text (arg1). If found, returns it as a number. Otherwise returns 0.


### YearsBetween
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| yearsbetween  | (Time, Time) | (Number)

This function returns number of years (as integer) between two dates: (arg1) and (arg2).


### YearDay
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| dayofyear, yearday  | (Time) | (Number)

This function returns, which day of the year is date (arg1).

---

# **Time Functions**

### Access
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| access | (Text) | (Time)

This function returns time of last access to file or directory with specific name (arg1) in current location.


### Christmas
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| christmas  | (Number) | (Time)

This function returns date of Christmas of the indicated year (arg1). Clock is set to 00:00:00.


### Creation
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| creation | (Text) | (Time)

This function returns creation time of file or directory with specific name (arg1) in current location.


### Date
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| date  | (Number, Number, Number) | (Time)

This function returns date created from three variables: day (arg1), month (arg2), year (arg3). Clock is set to 00:00:00.


### Easter
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| easter  | (Number) | (Time)

This function returns date of Easter of the indicated year (arg1). Clock is set to 00:00:00.


### Modification
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| modification | (Text) | (Time)

This function returns time of last modification of to file or directory with specific name (arg1) in current location.


### NewYear
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| newyear  | (Number) | (Time)

This function returns time of New Year (arg1).

---

# **Text Functions**

### After
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| after  | (Text, Text) | (Text)

This function returns part of base text (arg1), which appears after first occurance of phrase (arg2) inside base text (arg1). If text (arg2) cannot be found inside base text (arg1), empty text "" is returned.


### Before
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| before  | (Text, Text) | (Text)

This function returns part of base text (arg1), which appears before first occurance of phrase (arg2) inside base text (arg1). If text (arg2) cannot be found inside base text (arg1), empty text "" is returned.


### Binary
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| binary  | (Number) | (Text)

This function converts base 10 number (arg1) into binary and returns it as text.


### Commonbeginning
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| commonbeginning  | (List) | (Text)

This function returns common beginning of all elements in list (arg1). If all texts in list (arg1) start with a different phrase, empty text "" is returned.


### Commonbeginning
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| commonending  | (List) | (Text)

This function returns common ending of all elements in list (arg1). If all texts in list (arg1) end with a different phrase, empty text "" is returned.


### Digits
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| digits  | (Text) | (Text)

This function takes all digits from text (arg1) and returns it as a new text.


### Extension
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| extension | (Text) | (Text)

This function returns extension of file with specific name (arg1) in current location. If argument is name of directory, returned is empty text ("");


### Filled
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| fill, filled  | (Text, Number) | (Text)

This function takes text (arg1) and adds "0" signs in the beginning in order to make its length at least number (arg2). If length of text (arg1) is already bigger or equal to number (arg2), text (arg1) is returned.


### Fullname
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| fullname | (Text) | (Text)

This function returns whole name (with extension) of file or directory with specific name (arg1) in current location.


### Hex
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| hex  | (Number) | (Text)

This function converts base 10 number (arg1) into base 16 and returns it as text.


### Letters
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| letters  | (Text) | (Text)

This function takes all letters from text (arg1) and returns it as a new text.


### Lower
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| lower  | (Text) | (Text)

This function returns text (arg1) with all letters transformed into lowercase.


### Month
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| month  | (Number) | (Text)

This function returns english name of nth (arg1) month.


### Name
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| name | (Text) | (Text)

This function returns name (without extension) of file or directory with specific name (arg1) in current location.


### Repeat
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| repeat, repeated  | (Text, Number) | (Text)

This function takes text (arg1) and repeats it (arg2) times.


### Substring
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| substring | (Text, Number) | (Text)

This function takes text (arg1) and returns it excluding first (arg2) characters.


### Substring
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| substring | (Text, Number, Number) | (Text)

This function takes text (arg1), removes its first (arg2) characters and returns next (arg3) characters.


### Trim
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| trim  | (Text) | (Text)

This function returns text (arg1) with removed all white characters (spaces, tabs, ...) from the beginning and end of text.


### Upper
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| upper  | (Text) | (Text)

This function returns text (arg1) with all letters transformed into uppercase.


### WeekDay
| Names  | Arguments | Returns |
| ------------- | ------------- | ------------- |
| weekday  | (Number) | (Text)

This function returns english name of nth (arg1) day of week.
