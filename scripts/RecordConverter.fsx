#r "nuget: TextCopy"
//
#load "..\Result.fs"
#load "..\Tuples.fs"
#load "..\Combinators.fs"
#load "..\Array.fs"
#load "..\String.fs"

open DMLib
open DMLib.String
open DMLib.Combinators
open System.Text.RegularExpressions
open System

let menu =
    [ "Create ofRaw (anonymous record)"; "Create to Raw (anonymous record)" ]
    |> Tuple.dupMapFst (fun l -> [ 1 .. l.Length ] |> List.map _.ToString("x"))
    ||> List.zip
    |> List.map (fun x -> x ||> sprintf "%s) %s")
    |> String.concat "\n"

let getDeclaration t =
    let s = t |> trim

    let typ = Regex("type (?<name>\w+) =").Match(s).Groups["name"].Value

    typ,
    s
    |> split "\n"
    |> Array.map trim
    |> Array.filter (fun s -> s |> dont (startsWith "//"))
    |> Array.skipWhile (fun s -> s <> "{")
    |> Array.filter (fun s -> s <> "{" && s <> "}")
    |> String.concat "; "

let getAnonRec t =
    getDeclaration t |> Tuple.mapSnd (enclose "{| " " |}")

let ofRawAnon t =
    t |> getAnonRec |> (fun (typ, s) -> sprintf "ofRaw (r: %s): %s = " s typ)

let toRawAnon t =
    let (ty, v) = getAnonRec t

    Regex(@"(?<name>\w+): \w+").Replace(v, "${name} = r.${name}")
    |> fun s -> sprintf "toRaw(r: %s) = %s" ty s

printfn "What do you want to do?"
printfn "%s" menu
let id = Console.ReadLine()

TextCopy.ClipboardService.GetText()
|> match id |> toLower with
   | "1" -> ofRawAnon
   | "2" -> toRawAnon
   | x -> failwith $"Not implemented ({x})"
|> tee TextCopy.ClipboardService.SetText
