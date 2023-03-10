module DMLib.String

open System.Text.RegularExpressions
open System

///<summary>Indicates whether the specified string is <see langword="null" /> or an empty string ("").</summary>
///<param name="value">The string to test.</param>
///<returns><see langword="true" /> if the <paramref name="value" /> parameter is <see langword="null" /> or an empty string (""); otherwise, <see langword="false" />.</returns>
let isNullOrEmpty = String.IsNullOrEmpty

let isNullOrWhiteSpace = String.IsNullOrWhiteSpace

/// Folder function to create a string separated by new lines.
let foldNl acc s = acc + s + "\n"
/// Folder function to create a string separated by commas (readable to humans).
let foldPrettyComma acc s = acc + s + ", "

/// Folds a string in such a way that the last element does not end with the separator.
let smartFold separator acc s =
    if isNullOrEmpty acc then
        s
    else
        acc + separator + s

let smartNl = smartFold "\n"
let smartPrettyComma = smartFold ", "

/// Converts a string array to a string separated by newlines.
let toStrWithNl = Array.fold foldNl ""

let toLower (s: string) = s.ToLower()
let toUpper (s: string) = s.ToUpper()
let split (separator: string) (s: string) = s.Split(separator)
let startsWith (value: string) (s: string) = s.StartsWith(value)
let endsWith (value: string) (s: string) = s.EndsWith(value)
let contains (value: string) (s: string) = s.Contains(value)
let trim (s: string) = s.Trim()
let trimStart (s: string) = s.TrimStart()
let trimLeft = trimStart
let trimEnd (s: string) = s.TrimEnd()
let trimRight = trimEnd
let removeLastChars n (s: string) = s[.. s.Length - (n + 1)]
let enclose left right (s: string) = left + s + right
let encloseSame surround = enclose surround surround
let encloseQuotes = encloseSame "\""

/// Case insensitive comparison.
let compareICase (s1: string) s2 = System.String.Compare(s1, s2, true)

let isUrl (str: string) =
    let r =
        @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$"

    Regex.IsMatch(str, r)

let (|IsUrl|_|) input =
    if isUrl input then Some input else None

let (|EndsWith|_|) endStr input =
    if endsWith endStr input then
        Some input
    else
        None

let (|IsEmptyStr|_|) input =
    if isNullOrEmpty input then
        Some()
    else
        None

let (|IsWhiteSpaceStr|_|) input =
    if isNullOrWhiteSpace input then
        Some()
    else
        None

let regexReplace pattern (replacement: string) input =
    Regex(pattern).Replace(input, replacement)

let separateCapitals s =
    s
    |> regexReplace @"((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))" " $1"
    |> trimStart

type NonEmptyString = private NonEmptyString of string

module NonEmptyString =
    let create str =
        if str = "" then
            Error "This string can not be empty."
        else
            NonEmptyString str |> Ok

    let value (NonEmptyString str) = str

    let apply f (NonEmptyString e) = f e

    let map f e = apply f e |> create


type HexNumber = private HexNumber of string

module HexNumber =
    let private isHex s =
        match Regex("[^a-fA-F0-9]").Match(s).Success with
        | true -> Error $"{encloseQuotes s} is not a hex number."
        | false -> Ok s

    let private isEmpty s =
        match trim s with
        | "" -> Error "An hex number can not be empty."
        | _ -> Ok s

    let create str =
        result {
            let! nonEmpty = isEmpty str
            let! hex = isHex nonEmpty
            return HexNumber hex
        }

    let value (HexNumber hex) = hex
    let apply f (HexNumber hex) = f hex
    let map f hex = apply f hex |> create


type QuotedStr = private QuotedStr of string

module QuotedStr =
    let private transformIfNot transform condition x =
        if not (condition x) then
            transform x
        else
            x

    let private q = "\""
    let private ensureFirstQuote = transformIfNot (fun s -> q + s) (startsWith q)
    let private ensureTrailQuote = transformIfNot (fun s -> s + q) (endsWith q)

    let create s =
        QuotedStr(s |> ensureFirstQuote |> ensureTrailQuote)

    let value (QuotedStr s) = s

    let unquote (QuotedStr s) = s[.. s.Length - 2][1..]

    let apply f (QuotedStr s) = f s

    let map f s = apply f s |> create

    let oneOff s = s |> create |> value
