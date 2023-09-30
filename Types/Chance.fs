namespace DMLib.Types

open DMLib.MathL

type Chance =
    | Chance of float
    static member Min = 0.0
    static member Max = 100.0

    static member Create x =
        x |> forceRange Chance.Min Chance.Max |> Chance

    static member toInt(Chance x) = int x
    static member toFloat(Chance x) = x
    member t.value = Chance.toFloat t
    member t.asInt = Chance.toInt t
    override t.ToString() = sprintf "Chance %f" t.value

    static member Zero = Chance 0

[<AutoOpen>]
module ChanceTopLevelOperations =
    let inline Chance a = Chance.Create(int a)
