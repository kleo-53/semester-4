namespace Lazy

type LazyFactory =
    /// Creates lazy realization in one thread
    static member SimpleLazy supplier =
        new SimpleLazy<'a>(supplier) :> ILazy<'a>

    /// Creates multi thread lazy realization 
    static member ThreadsLazy supplier =
        new ThreadsLazy<'a>(supplier) :> ILazy<'a>

    /// Creates lock free lazy realization
    static member LockFreeLazy supplier =
        new LockFreeLazy<'a>(supplier) :> ILazy<'a>

