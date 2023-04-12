namespace DMLib

open System.Diagnostics
open DMLib.String
open Microsoft.FSharp.Reflection
open System.ComponentModel

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

    let (|IsNull|_|) object = if isNull object then Some() else None

///<summary>A class that has already enabled all plumbing to just tell WPF a property has changed.</summary>
///<remarks>Usage: inherit from this class. When a property has changed,
///call <c>OnPropertyChanged</c>.</remarks>
///<example id="wpfbindable"><code lang="fsharp">
/// type NavItem(uId: string, d: Raw) =
///     inherit WPFBindable()
///
///     member t.Img
///         with get () = img
///         and set (v) =
///             img <- v
///             t.OnPropertyChanged("Img")
///</code>
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
