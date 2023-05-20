module MapForTree

// Tree structure
type Tree<'a> =
    | Tree of 'a * Tree<'a> * Tree<'a>
    | Tip of 'a

// Map function for tree
let rec mapForTree func tree =
    match tree with
    | Tree(value, leftTree, rightTree) -> Tree(func value, mapForTree func leftTree, mapForTree func rightTree)
    | Tip value -> Tip(func value)