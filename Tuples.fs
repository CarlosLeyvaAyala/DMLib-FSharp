namespace DMLib

[<AutoOpen>]
module TupleCommon =

    let setSnd b a = a, b
    let setFst b a = b, a
    /// Duplicates a single element into a tuple of two.
    let dupFst a = setSnd a a
    let fst3 (v, _, _) = v
    let snd3 (_, v, _) = v
    let thrd3 (_, _, v) = v

[<RequireQualifiedAccess>]
module Tuple =
    let mapFst mapper (first, second) = mapper first, second
    let mapSnd mapper (first, second) = first, mapper second
    let mapFst3 mapper (first, second, third) = mapper first, second, third
    let mapSnd3 mapper (first, second, third) = first, mapper second, third
    let mapThrd3 mapper (first, second, third) = first, second, mapper third
    /// Duplicates an element, then maps the first element of the new tuple.
    let dupMapFst f = dupFst >> mapFst f
    /// Duplicates an element, then maps the second element of the new tuple.
    let dupMapSnd f = dupFst >> mapSnd f
    let swap (x, y) = (y, x)
    let mapBoth mapper1 mapper2 (v1, v2) = (mapper1 v1, mapper2 v2)
