module DMLib.Objects

open Microsoft.FSharp.Reflection
open System

/// Converts a record to an array so its fields can be iterated, pretty much like Javascript.
let recordToArray record =
    FSharpType.GetRecordFields(record.GetType())
    |> Array.map (fun v -> v.Name, v.GetValue(record))

/// Gets a property from an object so it can be executed with p.GetMethod.Invoke(v, [||])
let getPropertyByName name o =
    o.GetType().GetProperties()
    |> Array.filter (fun p -> p.Name = name)
    |> Array.first

/// Gets all possible cases from a discriminated union.
let getUnionCases<'T> () =
    FSharpType.GetUnionCases(typeof<'T>)
    |> Array.map (fun case -> FSharpValue.MakeUnion(case, [||]) :?> 'T)

/// This function can check if lists or other usually not nullable types are actually null.
let inline isNull (x: ^T when ^T: not struct) = obj.ReferenceEquals(x, null)
/// This function can check if lists or other usually not nullable types are actually null.
let inline isNotNull (x: ^T when ^T: not struct) = not (obj.ReferenceEquals(x, null))

let (|IsNull|_|) object = if isNull object then Some() else None

let (|IsNotNull|_|) object =
    if isNull object then
        None
    else
        Some object

let defaultIfNull v o = if isNull o then v else o

let (|NullableNull|NullableV|) (nullable: Nullable<'a>) =
    if nullable.HasValue then
        NullableV nullable.Value
    else
        NullableNull

let nullToResult (o: 'a) = if isNull o then Error() else Ok o
