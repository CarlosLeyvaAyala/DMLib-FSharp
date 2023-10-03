namespace DMLib

[<RequireQualifiedAccess>]
module Map =
    ///<summary>Converts a map to another map given a function <c>f</c>.</summary>
    ///<param name="f">The change function.</param>
    ///<param name="map">The input map.</param>
    ///<returns>The resulting map.</returns>
    ///<example id="change-1"><code lang="fsharp">
    /// let input: Map&gt;int, int&lt; = Map.empty.Add(1, 11).Add(2, 22)
    ///
    /// input |&gt; mapToMap2 (fun (k, v) -> k.ToString(), char v)
    /// // evaluates to map [("1", '\011'); ("2", '\022')]
    /// </code></example>
    let toMap f map =
        map
        |> Map.fold
            (fun acc k v ->
                let (k', v') = f (k, v)
                acc |> Map.add k' v')
            Map.empty

    ///<summary>Merges two maps applying <c>f key (existingValue, newValue)</c> on conflicting keys.</summary>
    let merge f (oldMap: Map<'a, 'b>) newMap =
        newMap
        |> Map.fold
            (fun acc k nv ->
                match Map.tryFind k acc with
                | Some ov -> acc |> Map.add k (f k (ov, nv))
                | None -> acc |> Map.add k nv)
            oldMap

[<AutoOpen>]
module MapPatterns =
    let (|ContainsKey|_|) key map =
        if map |> Map.containsKey key then
            Some()
        else
            None

    let (|NotContainsKey|_|) key map =
        if map |> Map.containsKey key then
            None
        else
            Some()
