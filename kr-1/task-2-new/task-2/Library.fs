namespace task_2

module Romb =
    let printRomb n =
        let rec loop str i space =
            if i = (n * 2) then 
                str
            else if i = 1 then 
                loop (String.replicate space " " + String.replicate (2*i - 1) "*") (i + 1) (space - 1)
                else
                    if i < n then 
                        loop (str + "\n" + String.replicate space " " + String.replicate (2*i - 1) "*") (i + 1) (space - 1)
                    else 
                        loop (str + "\n" + String.replicate space " " + String.replicate ((2*n - i)*2 - 1) "*") (i + 1) (space + 1)
        if (n <= 0) then ""
        else if (n = 1) then "*"
        else
            loop "" 1 (n - 1)