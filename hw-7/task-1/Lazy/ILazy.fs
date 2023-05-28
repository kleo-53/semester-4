namespace Lazy

type ILazy<'a> =
    // Calculates the result once and then returns it without recalculating
    abstract member Get: unit -> 'a