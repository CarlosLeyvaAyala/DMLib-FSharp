#load "..\Result.fs"
#load "..\String.fs"
#load "..\Json.fs"
#load "RecordId.fs"
#load "HexNumber.fs"

open DMLib.Types
open DMLib

fsi.AddPrinter(fun (r: HexNumber) -> r.ToString())

let ha = HexNumber "fe120"
let hb = HexNumber "aaaaaaaccccc02"
let hc = HexNumber "0x00000FE120"
let ha' = HexNumber 1040672
let hb' = HexNumber 48038396061076482L
let hn = HexNumber -1
hn.ToInt()
hn.ToInt64()
ha.ToInt()
hb.ToInt64() |> Json.serialize true
ha = hc
ha = hb
hc
ha' = ha
hb = hb'
hb'
ha = HexNumber 1040672
ha' = hc
max ha hb
HexNumber ""
HexNumber "x3"
HexNumber "0X3"
HexNumber 0u


fsi.AddPrinter(fun (r: RecordId) -> r.ToString())

let a = RecordId 10
let b = RecordId 2
let c = RecordId 10
RecordId -1
max a b
a = c
c = b
List.max [ a; b; c; c.Increment() ]
