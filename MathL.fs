module DMLib.MathL

open System

type PointF =
    { X: float
      Y: float }

    member t.asTuple = t.X, t.Y
    static member ofTuple(x, y) = { X = x; Y = y }
    static member toTuple(p: PointF) = p.asTuple


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

/// Checks if the range is actually valid
[<CompiledName("IsInRange'")>]
let inline isInRange'<'a when 'a: comparison> (lo: 'a) hi x : bool =
    let l = min lo hi
    let h = max lo hi
    isInRange l h x

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

[<CompiledName("LinCurveT")>]
let linCurveT p1 p2 =
    (PointF.ofTuple p1, PointF.ofTuple p2) ||> linCurve

[<CompiledName("ExpCurve")>]
let expCurve shape p1 p2 x =
    let e = Math.Exp
    let b = shape
    let ebx1 = e (b * p1.X)

    match (e (b * p2.X)) - ebx1 with
    | m when m = 0 -> linCurve p1 p2 x // Shape is actually a line, not an exponential curve.
    | d ->
        let a = (p2.Y - p1.Y) / d
        let c = p1.Y - a * ebx1
        a * (e (b * x)) + c

// https://www.developpez.net/forums/d331608-3/general-developpement/algorithme-mathematiques/contribuez/image-interpolation-spline-cubique/#post3513925
let private secondDerivative (p: PointF array) =
    let n = p.Length
    let matrix = Array2D.create n 3 0.0
    let result = Array.create n 0.0

    // build the tridiagonal system
    // (assume 0 boundary conditions: y2[0]=y2[-1]=0)
    matrix[0, 1] <- 1

    for i = 1 to n - 2 do
        matrix[i, 0] <- (p[i].X - p[i - 1].X) / 6.0
        matrix[i, 1] <- (p[i + 1].X - p[i - 1].X) / 3.0
        matrix[i, 2] <- (p[i + 1].X - p[i].X) / 6.0

        result[i] <-
            (p[i + 1].Y - p[i].Y) / (p[i + 1].X - p[i].X)
            - (p[i].Y - p[i - 1].Y) / (p[i].X - p[i - 1].X)

    matrix[n - 1, 1] <- 1

    // solving pass1 (up->down)
    for i = 1 to n - 1 do
        let k = matrix[i, 0] / matrix[i - 1, 1]
        matrix[i, 1] <- matrix[i, 1] - (k * matrix[i - 1, 2])
        matrix[i, 0] <- 0
        result[i] <- result[i] - (k * result[i - 1])

    // solving pass2 (down->up)
    for i = n - 2 downto 0 do
        let k = matrix[i, 2] / matrix[i + 1, 1]
        matrix[i, 1] <- matrix[i, 1] - (k * matrix[i + 1, 0])
        matrix[i, 2] <- 0
        result[i] <- result[i] - (k * result[i + 1])

    for i = 0 to n - 1 do
        result[i] <- result[i] / matrix[i, 1]

    result

/// Use it with care. Not really optimized and not as general as the "-curve" functions.
let getBezierSplinePointsTable stepSize p =
    let sd = secondDerivative p
    let n = p.Length
    let mutable output = List.empty<PointF>

    let inline outX x1 x2 =
        let l = [| x1..stepSize..x2 |]

        if l |> Array.last = x2 then l[.. l.Length - 2] else l

    let inline genOutput cur next i x =
        let t = (x - cur.X) / (next.X - cur.X)
        let a = 1.0 - t
        let b = t
        let h = next.X - cur.X

        let y =
            a * cur.Y
            + b * next.Y
            + (h * h / 6.0) * ((a * a * a - a) * sd[i] + (b * b * b - b) * sd[i + 1])

        output <- output @ [ { X = x; Y = y } ]

    let inline genOutputBetweenXs i =
        let cur = p[i]
        let next = p[i + 1]

        outX cur.X next.X |> Array.iter (genOutput cur next i)

    for i = 0 to n - 2 do
        genOutputBetweenXs i

    // Make sure last element is the last in the point list
    let lp = Array.last p

    if (List.last output).X <> lp.X then
        output <- output @ [ lp ]

    // Start as a flat line
    if output[0].X > 0 then
        output <- [ { X = 0; Y = output[0].Y } ] @ output

    // End as a flat line
    let last = List.last output

    if last.X < 1 then
        output <- output @ [ { X = 1; Y = last.Y } ]

    output
