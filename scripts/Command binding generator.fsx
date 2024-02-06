// Usage:
// * Copy whole F# static command definition.
// * Set static class namespace.
// * Run script.
//
// This script will detect al commands and will generate XAML bindings for them.

#r "nuget: TextCopy"

// DMLib includes must be deleted once nuget works again
#load "..\Combinators.fs"
#load "..\MathL.fs"
#load "..\Result.fs"
#load "..\Array.fs"
#load "..\String.fs"

open System.Text.RegularExpressions
open DMLib.String

let getCommands () =
    let commands =
        TextCopy.Clipboard().GetText()
        |> Regex("static member val (\w+) =").Matches

    [ for cmd in commands do
          cmd.Groups[1].Value ]

let classFromClipboard defaultName =
    "c:"
    + match TextCopy.Clipboard().GetText() with
      | Regex "type\s+(\w+)" [ className ] -> className
      | _ -> defaultName
    + "."

let genBindings staticClass =
    let toBindings (cmdName, cmd) =
        let outStr =
            """
            <CommandBinding
            CanExecute="OnCan%s"
            Command="%s"
            Executed="On%s" />
            """

        sprintf (Printf.StringFormat<string -> string -> string -> string>(outStr)) cmdName cmd cmdName

    let toDecls s =
        let d =
            """
              private void OnCan%s(object sender, CanExecuteRoutedEventArgs e) {
                e.CanExecute = ;
              }
              private void On%s(object sender, ExecutedRoutedEventArgs e) {

              }
            """

        sprintf (Printf.StringFormat<string -> string -> string>(d)) s s

    let toMenus cmd =
        $"""
<MenuItem Command="{staticClass}{cmd}"
    Header="{cmd}"
    ToolTip="{{Binding RelativeSource={{RelativeSource Self}}, Path=Command.Text}}" />
"""

    let toCommands cmd = $"Command=\"{staticClass}{cmd}\""

    let fold acc s = acc + s
    let c = getCommands ()

    let b =
        c
        |> List.map (fun s -> s, staticClass + s)
        |> List.map toBindings
        |> List.fold fold ""

    let d = c |> List.map toDecls |> List.fold fold ""
    let m = c |> List.map toMenus |> List.fold fold ""

    let cmd =
        c
        |> List.map toCommands
        |> List.fold smartNl ""
        |> encloseSame "\n"

    {| bindings = b
       declarations = d
       menus = m
       commandList = cmd |}

let copy = TextCopy.ClipboardService.SetText

let r = "AppCmds" |> classFromClipboard |> genBindings

r.bindings |> copy
r.declarations |> copy
r.menus |> copy

r.bindings
r.declarations
r.menus
r.commandList
