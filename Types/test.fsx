#load "RecordId.fs"

open System
open System.IO
open DMLib.Types

fsi.AddPrinter(fun (r: RecordId) -> r.ToString())

let a = RecordId 10
let b = RecordId 2
let c = RecordId 10
RecordId -1
max a b
a = c
c = b
List.max [ a; b; c; c.Increment() ]
