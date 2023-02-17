#load "..\Result.fs"
#load "..\String.fs"
#load "..\Json.fs"
#load "RecordId.fs"
#load "MemoryAddress.fs"

open DMLib.Types
open DMLib

//fsi.AddPrinter(fun (r: MemoryAddress) -> r.ToString())

let ha = MemoryAddress "fe120"
let hb = MemoryAddress "aaaaaaaccccc02"
let hc = MemoryAddress "0x00000FE120"
let ha' = MemoryAddress 1040672u
let hb' = MemoryAddress 48038396061076482UL
ha.ToInt()
hb.ToInt64() |> Json.serialize true
ha = hc
ha = hb
hc
ha' = ha
hb = hb'
hb'
ha = MemoryAddress 1040672u
ha' = hc
max ha hb
MemoryAddress ""
MemoryAddress "x3"
MemoryAddress "0X3"
MemoryAddress 0u


fsi.AddPrinter(fun (r: RecordId) -> r.ToString())

let a = RecordId 10
let b = RecordId 2
let c = RecordId 10
RecordId -1
max a b
a = c
c = b
List.max [ a; b; c; c.Increment() ]

18_446_744_073_709_551_615UL
|> Json.serialize true
