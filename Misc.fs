namespace DMLib

open System.Diagnostics
open DMLib.String
open Microsoft.FSharp.Reflection
open System.ComponentModel
open System

module Link =
    let openInBrowser url =
        if isUrl url then
            let mutable p = ProcessStartInfo(url)
            p.UseShellExecute <- true
            Process.Start(p) |> ignore

[<AutoOpen>]
module Patterns =
    /// Checks if some value is the same as other.
    let (|Equals|_|) x y = if x = y then Some() else None

module Objects =
    /// Converts a record to an array so its fields can be iterated, pretty much like Javascript.
    let recordToArray record =
        FSharpType.GetRecordFields(record.GetType())
        |> Array.map (fun v -> v.Name, v.GetValue(record))

    let inline isNull (x: ^T when ^T: not struct) = obj.ReferenceEquals(x, null)
    let inline isNotNull (x: ^T when ^T: not struct) = not (obj.ReferenceEquals(x, null))

    let (|IsNull|_|) object = if isNull object then Some() else None

    let (|IsNotNull|_|) object =
        if isNull object then
            None
        else
            Some object

[<Obsolete("Use DMLib_WPF")>]
type WPFBindable() =
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    /////////////////////////////////////////////////////////////////////////////////////////////
    // WPF binding
    // https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/members/events
    [<CLIEvent>]
    member _.PropertyChanged = propertyChanged.Publish

    interface INotifyPropertyChanged with
        member _.add_PropertyChanged(handler) =
            propertyChanged.Publish.AddHandler(handler)

        member _.remove_PropertyChanged(handler) =
            propertyChanged.Publish.RemoveHandler(handler)

    member t.OnPropertyChanged(e: PropertyChangedEventArgs) = propertyChanged.Trigger(t, e)

    member t.OnPropertyChanged(property: string) =
        t.OnPropertyChanged(PropertyChangedEventArgs(property))

    /// Sends a message telling all properties were updated.
    member t.OnPropertyChanged() = t.OnPropertyChanged("")
