module DMLib.Combinators

let i x = x

let tap f x =
    f
    x

let tee f x =
    f x
    x

let fork f g x = (f x), (g x)

let swap f x y = f y x

let join2 aTuple =
    match fst aTuple with
    | Error e -> Error e
    | Ok v1 ->
        match snd aTuple with
        | Error e -> Error e
        | Ok v2 -> Ok(v1, v2)

let isNot f x = not (f x)
