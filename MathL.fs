module DMLib.MathL

open System

type PointF = { X: float; Y: float }

let ForcePositive x = max 0.0 x
let ForcePercent x = Math.Clamp(0.0, 1, x)
let PercentToFloat x = x / 100.0

[<CompiledName("LinCurve")>]
let linCurve p1 p2 =
    let x1 = p1.X
    let y1 = p1.Y
    let m = (p2.Y - y1) / (p2.X - x1)

    fun x -> m * (x - x1) + y1
