namespace DMLib.Types

open DMLib.MathL

[<Sealed>]
type CanvasPoint(x: float, y: float) =
    static let validate v = v |> forceRange 0.0 1.0
    let mutable x = validate x
    let mutable y = validate y

    member t.X
        with get () = x
        and set v = x <- validate v

    member t.Y
        with get () = y
        and set v = y <- validate v

    member t.InvertY() = CanvasPoint(x, 1.0 - y)
    static member InvertY(v: CanvasPoint) = v.InvertY()
    static member InvertY(a: CanvasPoint array) = a |> Array.map CanvasPoint.InvertY

    override t.ToString() = sprintf "X %f, Y %f" t.X t.Y
    override t.GetHashCode() = t.ToString() |> hash

    override t.Equals(a) =
        match a with
        | :? CanvasPoint as x -> x.X = t.X && x.Y = t.Y
        | _ -> false

#if INTERACTIVE
fsi.AddPrinter(fun (r: CanvasPoint) -> r.ToString())
#endif
