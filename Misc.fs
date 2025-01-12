namespace DMLib

open System.Diagnostics
open DMLib.String
open System.ComponentModel
open System
open System.Threading.Tasks

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
    let (|GreaterThan|_|) x y = if x < y then Some() else None
    let (|GreaterOrEqualThan|_|) x y = if x <= y then Some() else None
    let (|LesserThan|_|) x y = if x > y then Some() else None
    let (|LesserOrEqualThan|_|) x y = if x >= y then Some() else None

[<AutoOpen>]
module Misc =
    /// Converts a normal function to an async one
    let makeAsync (f: unit -> unit) =
        fun () ->
            async {
                let! _ = Task.Run(f) |> Async.AwaitTask
                return ()
            }
            |> Async.StartImmediate

    /// Caches results from some function.
    let memoize (f: 'a -> 'b) =
        let mutable cache: Map<'a, 'b> = Map.empty

        fun (x: 'a) ->
            match cache |> Map.tryFind x with
            | None ->
                let y = f x
                cache <- cache.Add(x, y)
                y
            | Some v -> v



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
