module EvenNumbers.Tests

open FsCheck
open EvenFunctions

let EqualFunctionsTest (testList:list<int>) = (evenMap testList = evenFilter testList && evenFilter testList = evenFold testList)

Check.Quick EqualFunctionsTest

let revIsOrig (xs:list<int>) = List.rev xs = xs
Check.QuickThrowOnFailure revIsOrig