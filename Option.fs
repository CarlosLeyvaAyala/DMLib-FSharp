[<RequireQualifiedAccess>]
module DMLib.Option

let ofBool x value = if value then Some x else None

let ofTryByref =
    function
    | true, x -> Some x
    | _ -> None
