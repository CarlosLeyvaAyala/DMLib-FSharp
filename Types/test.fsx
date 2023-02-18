#load "..\Result.fs"
#load "..\String.fs"
#load "..\Json.fs"
#load "NonEmptyString.fs"
#load "RecordId.fs"
#load "MemoryAddress.fs"

open DMLib.Types
open DMLib
open System

let rnd = Random()

/////////////////////////////////////////////////////////////
let ts = NonEmptyString " eueu  "
let nesA = NonEmptyString "AAAAA"
let nesB = NonEmptyString "BBBBBBBBB"
let nesC = NonEmptyString "BBBBBBBBB"
nesC = nesB
nesA = nesB
max nesB nesA
ts.Value
max nesA nesB
min ts nesB
min ts nesA
NonEmptyString "         "
NonEmptyString ""
/////////////////////////////////////////////////////////////
//fsi.AddPrinter(fun (r: MemoryAddress) -> r.ToString())

let ha = MemoryAddress "fe120"
ha.ToString()
let hb = MemoryAddress "aaaaaaaccccc02"
let hc = MemoryAddress "0x00000FE120"
let ha' = new MemoryAddress 1040672u
let hb' = new MemoryAddress 48038396061076482UL
ha.ToInt()
hb.ToInt64() |> Json.serialize true
ha = hc
ha = hb
hc
ha' = ha
hb = hb'
hb'
ha = new MemoryAddress 1040672u
ha' = hc
max ha hb
MemoryAddress ""
MemoryAddress "x3"
MemoryAddress "0X3"
//MemoryAddress 0u

/////////////////////////////////////////////////////////////
//fsi.AddPrinter(fun (r: RecordId) -> r.ToString())

let rId =
    [ for i in [ 1..20 ] do
          rnd.Next(101) |> uint64 ]

rId
|> List.map RecordId
|> List.distinct
|> List.sort
|> List.max
|> fun r -> r.Next()
