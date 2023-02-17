#load "..\..\MathL.fs"
#load "..\..\Combinators.fs"
#load "..\..\Result.fs"
#load "..\..\String.fs"
#load "..\..\IO.Path.fs"
#load "..\NonEmptyString.fs"
#load "..\MemoryAddress.fs"
#load "EDID.fs"
#load "Weight.fs"
#load "EspFileName.fs"
#load "UniqueId.fs"

open System
open DMLib.String
open DMLib.Types.Skyrim
open System.IO
open DMLib.IO.Path
open System.Text.RegularExpressions

///////////////////////////////////////////////////////////
EDID "eoueo"
EDID "oe uaou aoue"

///////////////////////////////////////////////////////////
[ "Skyrim.esm|0X00000000653"
  "Skyrim.esm|F3"
  "Dawnguard.esm|fff34" ]
|> List.map (fun s -> UniqueId s)
|> List.sort

UniqueId "Skyrim.esm|0X00000000653"

///////////////////////////////////////////////////////////
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

///////////////////////////////////////////////////////////
let aa = EspFileName "TroublesOfHeroine.esl"
let bb = EspFileName "aaaa.esp"
max aa bb
aa.Value()
aa
EspFileName ""
