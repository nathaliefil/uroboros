Order By
=====================

In Uroboros ordering elements of list can be done in two ways: as subcommand of List Expression or as separate Command. Its behaviour is
very similar to Order By from language SQL. User prints one after another variables used for sorting. Elements are sorted ascending as
default, but user can reverse order by typing keyword 'desc' after variable.



### Definition

| Structure | Returns |
| --------- | ------- |
| [order by structure] | [order by variables] |
| [order by structure] [order by structure] | [order by variables] |
| [order by structure] [order by structure] [order by structure] | [order by variables] |
| analogically for more structures | |


| Structure | Returns |
| --------- | ------- |
| [variable for order] | [order by structure] |
| [variable for order] **asc** | [order by structure] |
| [variable for order] **desc** | [order by structure] |

### Allowed Variables For Order

Currently there are 39 variables which can be used to order elements. They are all [inner variables](InnerVariables.md). In future
development Uroboros will allow any data structre to be used for ordering - even if it is not related to property of file or directory

| Variable For Order |
| --------- |
| access |
| creation |
| empty |
| exist |
| extension |
| fullname |
| modification |
| name |
| size |
| iscorrect |
| isdirectory |
| isfile |
| access.year |
| access.month |
| access.wekday |
| access.day |
| access.hour |
| access.minute |
| access.second |
| access.date |
| access.clock |
| creation.year |
| creation.month |
| creation.wekday |
| creation.day |
| creation.hour |
| creation.minute |
| creation.second |
| creation.date |
| creation.clock |
| modification.year |
| modification.month |
| modification.wekday |
| modification.day |
| modification.hour |
| modification.minute |
| modification.second |
| modification.date |
| modification.clock |
