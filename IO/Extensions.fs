namespace DMLib

open System.IO.Compression
open DMLib.IO

[<AutoOpen>]
module IOExtenions =

    type System.IO.Compression.ZipFile with

        static member createFromFiles overwrite filename files =
            let arch, fss, entries =
                match filename with
                | FileExists _ ->
                    let r = ZipFile.Open(filename, ZipArchiveMode.Update)
                    let e = r.Entries |> Seq.toArray
                    r, e |> Array.mapi (fun i e -> e.Name, i) |> Map.ofSeq, e
                | _ -> ZipFile.Open(filename, ZipArchiveMode.Create), Map.empty, [||]

            files
            |> Array.iter (fun fn ->
                let k = Path.getFileName fn

                let add () =
                    arch.CreateEntryFromFile(fn, k) |> ignore

                match fss |> Map.tryFind k, overwrite with
                | Some i, true ->
                    entries[i].Delete()
                    add ()
                | Some _, false -> ()
                | None, _ -> add ())

            arch.Dispose()
