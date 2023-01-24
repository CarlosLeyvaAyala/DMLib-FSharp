﻿/// <summary>Functions that work with collections.</summary>
module DMLib.Collections

open System.Collections.ObjectModel

/// <summary>Checks if some index is within the valid range of a list count.</summary>
/// <param name="itemCount">List item count.</param>
/// <param name="expectedIndex">Index to check if is within a valid.</param>
/// <returns>A <c>bool</c> telling if the index is within range.</returns>
let IsInvalidIndex itemCount expectedIndex =
    expectedIndex >= itemCount || expectedIndex < 0

/// Converts as C# collection to an F# list.
let CollectionToList<'T> (c: Collection<'T>) = [ for i in 0 .. c.Count - 1 -> c[i] ]

/// Converts as C# collection to an F# array.
let CollectionToArray<'T> (c: Collection<'T>) = [| for i in 0 .. c.Count - 1 -> c[i] |]

/// Converts an array to a C# list.
let ArrayToCList (a: 'a []) =
    let l = System.Collections.Generic.List<'a>()

    for i in 0 .. a.Length - 1 do
        l.Add(a[i])

    l

/// Converts an array to a C# list.
let ListToCList (a: List<'a>) =
    let l = System.Collections.Generic.List<'a>()

    for i in 0 .. a.Length - 1 do
        l.Add(a[i])

    l

let ArrayToObservableCollection (a: 'a []) =
    let l = ObservableCollection<'a>()

    for i in 0 .. a.Length - 1 do
        l.Add(a[i])

    l
