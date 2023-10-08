namespace DMLib

[<AutoOpen>]
module TupleCommon =

    let setSnd b a = a, b
    let setFst b a = b, a
    /// Duplicates a single element into a tuple of two.
    let dupFst a = setSnd a a

[<RequireQualifiedAccess>]
module Tuple =
    let mapFst mapper (first, second) = mapper first, second
    let mapSnd mapper (first, second) = first, mapper second
    /// Duplicates an element, then maps the first element of the new tuple.
    let dupMapFst f = dupFst >> mapFst f
    /// Duplicates an element, then maps the second element of the new tuple.
    let dupMapSnd f = dupFst >> mapSnd f
    let swap (x, y) = (y, x)
