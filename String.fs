module DMLib.String

open System.Text.RegularExpressions
open System

let castToString (s: obj) = s :?> string

///<summary>Indicates whether the specified string is <see langword="null" /> or an empty string ("").</summary>
///<param name="value">The string to test.</param>
///<returns><see langword="true" /> if the <paramref name="value" /> parameter is <see langword="null" /> or an empty string (""); otherwise, <see langword="false" />.</returns>
let isNullOrEmpty = String.IsNullOrEmpty

let isNullOrWhiteSpace = String.IsNullOrWhiteSpace

/// Folder function that folds as is.
let dumbFold separator acc (s: string) = acc + s + separator
/// Folder function to create a string separated by new lines.
let foldNl acc s = acc + s + "\n"
/// Folder function to create a string separated by commas (readable to humans).
let foldPrettyComma acc s = acc + s + ", "

/// Folds a string in such a way that the last element does not end with the separator.
let smartFold separator acc s =
    if isNullOrEmpty acc then s else acc + separator + s

let smartNl = smartFold "\n"
let smartPrettyComma = smartFold ", "

/// Converts a string array to a string separated by newlines.
let toStrWithNl = Array.fold foldNl ""
let toLower (s: string) = s.ToLower()
let toUpper (s: string) = s.ToUpper()
let split (separator: string) (s: string) = s.Split(separator)
let startsWith (value: string) (s: string) = s.StartsWith(value)

let startsWithIC (value: string) (s: string) =
    s.StartsWith(value, StringComparison.CurrentCultureIgnoreCase)

let endsWith (value: string) (s: string) = s.EndsWith(value)

let endsWithIC (value: string) (s: string) =
    s.EndsWith(value, StringComparison.CurrentCultureIgnoreCase)

let inline contains (value: string) (s: string) = s.Contains(value)
let lastIndexOf (substr: string) (str: string) = str.LastIndexOf substr

/// Checks is a string s contains some value
let inline containsIC (value: string) (s: string) =
    s.Contains(value, StringComparison.CurrentCultureIgnoreCase)

/// Checks if a string is equals to other. Case insensitive.
let inline equalsIC (s2: string) (s1: string) =
    s1.Equals(s2, StringComparison.OrdinalIgnoreCase)

let replace (oldValue: string) newValue (s: string) = s.Replace(oldValue, newValue)

let replaceAt (str: string) index (replacement: string) =
    str
        .Remove(index, Math.Min(replacement.Length, str.Length - index))
        .Insert(index, replacement)

let trim (s: string) = s.Trim()
let trimStart (s: string) = s.TrimStart()
let trimLeft = trimStart
let trimEnd (s: string) = s.TrimEnd()
let trimRight = trimEnd
let removeLastChars n (s: string) = s[.. s.Length - (n + 1)]
let enclose left right (s: string) = left + s + right
let encloseSame surround = enclose surround surround
let encloseQuotes = encloseSame "\""

/// Converts a string separated by new lines to an array of trimmed strings
let stringToArray s = s |> split "\n" |> Array.map trim

/// Converts a string separated by new lines to a list of trimmed strings
let stringToList s = s |> stringToArray |> Array.toList

/// Case insensitive comparison.
let compareICase (s1: string) s2 = System.String.Compare(s1, s2, true)

let isUrl (str: string) =
    let r =
        @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$"

    Regex.IsMatch(str, r)

let isUrlAlt (str: string) =
    let r =
        @"^((ht|f)tp(s?)\:\/\/)?[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?=\,\'\/\\\+&%\$#_]*)?"

    Regex.IsMatch(str, r)

let (|IsUrl|_|) input =
    if isUrl input then Some input else None

let (|StartsWith|_|) endStr input =
    if startsWith endStr input then Some() else None

let (|StartsWith'|_|) endStr input =
    if startsWith endStr input then Some input else None

let (|StartsWithIC|_|) endStr input =
    if startsWithIC endStr input then Some() else None

let (|EndsWith|_|) endStr input =
    if endsWith endStr input then Some() else None

let (|EndsWith'|_|) endStr input =
    if endsWith endStr input then Some input else None

let (|IsEmptyStr|_|) input =
    if isNullOrEmpty input then Some() else None

let (|IsNotEmptyStr|_|) input =
    if isNullOrEmpty input then None else Some()

let (|IsWhiteSpaceStr|_|) input =
    if isNullOrWhiteSpace input then Some() else None

let regexReplace pattern (replacement: string) input =
    Regex(pattern).Replace(input, replacement)

/// Gets the index of a substring. Case sensitive.
let (|IsAtIndex|_|) (subStr: string) (str: string) =
    match str.IndexOf subStr with
    | -1 -> None
    | i -> Some i

/// Gets the index of a substring. Case insensitive.
let (|IsAtIndexIC|_|) (subStr: string) (str: string) =
    match str.IndexOf(subStr, StringComparison.CurrentCultureIgnoreCase) with
    | -1 -> None
    | i -> Some i

/// Checks if a string is contained in other. Case sensitive.
let (|IsContainedIn|_|) (container: string) (str: string) =
    if container.Contains str then Some() else None

/// Checks if a string is contained in other. Case insensitive.
let (|IsContainedInIC|_|) (container: string) (str: string) =
    if container.Contains(str, StringComparison.CurrentCultureIgnoreCase) then
        Some()
    else
        None

/// Checks if a string is not contained in other. Case sensitive.
let (|IsNotContainedIn|_|) (container: string) (str: string) =
    if container.Contains str then None else Some()

/// Checks if a string is not contained in other. Case insensitive.
let (|IsNotContainedInIC|_|) (container: string) (str: string) =
    if container.Contains(str, StringComparison.CurrentCultureIgnoreCase) then
        None
    else
        Some()

/// Checks if a string contains other. Case sensitive.
let (|Contains|_|) (substr: string) (str: string) =
    if str.Contains(substr) then Some() else None

/// Checks if a string contains other. Case insensitive.
let (|ContainsIC|_|) (substr: string) (str: string) =
    if str.Contains(substr, StringComparison.InvariantCultureIgnoreCase) then
        Some()
    else
        None

/// Checks if a string does not contain other. Case sensitive.
let (|NotContains|_|) (substr: string) (str: string) =
    if str.Contains(substr) then None else Some()

/// Checks if a string does not contain other. Case insensitive.
let (|NotContainsIC|_|) (substr: string) (str: string) =
    if str.Contains(substr, StringComparison.InvariantCultureIgnoreCase) then
        None
    else
        Some()

/// Checks if some string conforms to a Regex pattern.
let (|Regex|_|) pattern input =
    let m = System.Text.RegularExpressions.Regex.Match(input, pattern)

    if m.Success then
        Some(List.tail [ for g in m.Groups -> g.Value ])
    else
        None

let (|Split|) (on: string) (s: string) =
    s.Split(on, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
    |> Array.toList

let (|IsInt32|_|) (s: string) =
    match Int32.TryParse s with
    | true, x -> Some x
    | _ -> None

let (|IsUInt64|_|) (s: string) =
    match UInt64.TryParse s with
    | true, x -> Some x
    | _ -> None

let (|IsUInt32|_|) (s: string) =
    match UInt32.TryParse s with
    | true, x -> Some x
    | _ -> None

let (|IsDouble|_|) (s: string) =
    match Double.TryParse s with
    | true, x -> Some x
    | _ -> None

let separateCapitals s =
    s |> regexReplace @"((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))" " $1" |> trimStart

let capitalizeFirst (s: string) = Char.ToUpper(s[0]).ToString() + s[1..]

/// Finds the starting string that both inputs share, if any.
let findCommonRadix s1 s2 =
    match
        s1
        |> Seq.zip s2
        |> Seq.takeWhile (fun (a, b) -> a = b)
        |> Seq.map (fun (a, _) -> a)
        |> Seq.fold (fun acc s -> $"{acc}{s}") ""
    with
    | IsEmptyStr -> None
    | v -> Some v

/// Given two software versions, return the highest of them.
let getHighestVersion versionToCheck existingVersion =
    let toNumbers s =
        s
        |> split "."
        |> Array.map (function
            | IsUInt32 x -> x
            | _ -> 0u)


    let n1 = existingVersion |> toNumbers
    let n2 = versionToCheck |> toNumbers
    let l = Math.Max(n1.Length, n2.Length)

    let padZeros (a: uint32 array) =
        let blank = Array.create (l - a.Length) 0u
        Array.append a blank

    let compare = (padZeros n1, padZeros n2) ||> Array.zip

    let differentIdx =
        compare
        |> Array.mapi (fun i v -> i, v)
        |> Array.takeWhile (snd >> fun (a, b) -> a = b)
        |> Array.first
        |> Option.map (fst >> fun i -> i + 1)
        |> Option.defaultValue 0

    match compare[differentIdx] with
    | o, n when o > n -> existingVersion
    | o, n when o < n -> versionToCheck
    // Both are the same, but return the old to reflect there was no change
    | _ -> existingVersion

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
        if not (condition x) then transform x else x

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
