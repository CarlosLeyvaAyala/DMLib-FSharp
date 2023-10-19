#r "nuget: TextCopy"

let contents = "selected journey stage"

let w = 80
let len = contents.Length
let pad = (w - len - 4) / 2
let sep = "".PadRight(w, '/')
let l = "".PadLeft(pad)
let r = if len % 2 = 0 then l else l + " "
let c = $"//{l}{contents}{r}//".ToUpper()
let result = $"{sep}\n{c}\n{sep}"

TextCopy.ClipboardService.SetText(result)
