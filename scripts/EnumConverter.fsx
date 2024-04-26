#r "nuget: TextCopy"
#load "..\Result.fs"
#load "..\Combinators.fs"
#load "..\Array.fs"
#load "..\String.fs"

open DMLib.String
open DMLib.Combinators
open System.Text.RegularExpressions

let notComments s =
    not (Regex(@"\s*\/\/").Match(s).Success)

TextCopy.ClipboardService.GetText()

let toEnum t =
    t
    |> trim
    |> stringToArray
    |> Array.filter notComments
    |> Array.scan (fun (_, value) s -> s, value + 1) ("", -2)
    |> Array.skip 1
    |> Array.map (fun (s, value) ->
        match s.TrimEnd() with
        | StartsWith' "type" s -> s
        | s -> sprintf "%s = %d" s value)
    |> Array.fold smartNl ""

TextCopy.ClipboardService.SetText


let text =
    """
type PlayableRaces =
    /// Roman
    | Cidamin
    /// Orc
    | Moroch
    /// Sudanese
    | Vischurr
    /// Naga
    | Naga
    /// Angel
    | Bellamin
    /// Dark elf
    | Moredur
    /// High elf
    | Moritan
    /// Wood elf
    | Morund
    /// Slavs
    | Rugorya
    // Jundar
    | Thuliri
    /// Demon
    | Fajrumin
    """

let title = Regex("type (?<name>\w+) =")
let enumName = title.Match(text).Groups["name"].Value

Regex(@"(?m)(?<decl>^\s+\|\s*)(?<name>\w+)(\s*=\s*.*)")
    .Replace(text, sprintf "${decl}%s.${name} -> ${name}" enumName)
|> fun s -> title.Replace(s, "static member ofInt =")
|> TextCopy.ClipboardService.SetText

let toMatch text =
    text
    |> trim
    |> regexReplace @"(?m)(\|\s*)(?<name>\w+)" "$1${name} -> "
    |> stringToArray
    |> Array.filter notComments
    |> Array.skipWhile (startsWith "|" |> Not)
    |> String.concat "\n"

text |> toMatch |> TextCopy.ClipboardService.SetText
