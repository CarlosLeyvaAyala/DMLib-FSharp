module DMLib.List

let getDuplicates l =
    l
    |> List.groupBy id
    |> List.choose (fun (key, set) ->
        if set.Length > 1 then
            Some key
        else
            None)
