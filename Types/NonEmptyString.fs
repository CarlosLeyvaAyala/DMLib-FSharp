namespace DMLib.Types

open System

type NonEmptyString =
    | NonEmpty of string
    member s.Value() = let (NonEmpty x) = s in x
    static member Create(s) = NonEmpty s

[<AutoOpen>]
module NonEmptyStringConstructor =
    let inline NonEmptyString (s: string) =
        if String.IsNullOrEmpty s then
            invalidArg (nameof s) $"This string can not be empty."

        if String.IsNullOrWhiteSpace s then
            invalidArg (nameof s) $"This string can not be white-spaces only."

        NonEmptyString.Create(s)
