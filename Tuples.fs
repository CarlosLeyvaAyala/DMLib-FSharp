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
