module DMLib.MathL

open System

[<CompiledName("Max")>]
let inline max<'a when 'a: comparison> a b : 'a = if a > b then a else b

[<CompiledName("Min")>]
let inline min<'a when 'a: comparison> a b : 'a = if a < b then a else b

[<CompiledName("ForceMax")>]
let forceMax = min

[<CompiledName("ForceMin")>]
let forceMin = max

[<CompiledName("ForceRange")>]
let forceRange lo hi x = x |> forceMax hi |> forceMin lo

[<CompiledName("IsInRange")>]
let inline isInRange<'a when 'a: comparison> (lo: 'a) hi x : bool = x >= lo && x <= hi

type PointF = { X: float; Y: float }

[<CompiledName("ForcePositive")>]
let forcePositive x = max 0.0 x

[<CompiledName("ForcePercent")>]
let forcePercent x = Math.Clamp(0.0, 1, x)

[<CompiledName("PercentToFloat")>]
let percentToFloat x = x / 100.0

[<CompiledName("FloatToPercent")>]
let floatToPercent x = x * 100.0

[<CompiledName("LinCurve")>]
let linCurve p1 p2 =
    let x1 = p1.X
    let y1 = p1.Y
    let m = (p2.Y - y1) / (p2.X - x1)

    fun x -> m * (x - x1) + y1

[<CompiledName("ExpCurve")>]
let expCurve shape p1 p2 x =
    let e = Math.Exp
    let b = shape
    let ebx1 = e (b * p1.X)

    match (e (b * p2.X)) - ebx1 with
    | x when x = 0 -> linCurve p1 p2 x // Shape is actually a line, not an exponential curve.
    | d ->
        let a = (p2.Y - p1.Y) / d
        let c = p1.Y - a * ebx1
        a * (e (b * x)) + c
