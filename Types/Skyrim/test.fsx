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

let rnd = Random()
let randomInt maxValue _ = rnd.Next maxValue

///////////////////////////////////////////////////////////
[ "Ingun"
  "Dravynea"
  "DBTortureVictim3"
  "Knjakr"
  "Sylgja"
  "Nocturnal"
  "Nocturnal"
  "Nocturnal"
  "Nocturnal"
  "KharagGroShurkul"
  "Voada"
  "Fianna"
  "CurweDead"
  "Borri"
  "Embry"
  "Grisvar"
  "PavoAttius"
  "Olda"
  "MG04Augur"
  "dunAlftandYagGraGortwog"
  "Kust"
  "DLC2Storn"
  "MQ101TorturerAssistant" ]
|> List.map EDID
|> List.distinct
|> List.sort

EDID ""
EDID "   "
EDID "|"

///////////////////////////////////////////////////////////
[ "Skyrim.esm|0X00000000653"
  "Skyrim.esm|0xF3"
  "Skyrim.esm|0000F3"
  "Dawnguard.esm|fff34" ]
|> List.map (fun s -> UniqueId s)
|> List.sort
|> List.distinct

UniqueId "Skyrim.esm|0X00000000653"

///////////////////////////////////////////////////////////
let weights = [ 1..20 ] |> List.map (randomInt 101)

weights
|> List.map Weight
|> List.distinct
|> List.sortDescending

Weight -1
Weight 101

///////////////////////////////////////////////////////////
let aa = EspFileName "TroublesOfHeroine.esl"
let bb = EspFileName "aaaa.esp"
let cc = EspFileName "aaaa.esp"
max aa bb
aa.Value
bb.Value
cc.Value
aa
aa = bb
bb = cc
EspFileName "   .esm"
