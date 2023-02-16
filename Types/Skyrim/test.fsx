#load "..\..\MathL.fs"
#load "..\..\Combinators.fs"
#load "Weight.fs"

open DMLib.Types.Skyrim
open System.IO

fsi.AddPrinter(fun (r: Weight) -> r.ToString())

let a = Weight 10
let b = Weight 100
let c = Weight 10
c.ToInt()
max a b
a = c
c = b
List.max [ a; b; c ]
Weight -1
Weight 0
Weight 101
