module Core.Result

type SkogixResult<'t> =
  | Yay of 't
  | Nay of 't
let SkogixResultBind (f: 't -> 't) (result:SkogixResult<'t>) =
  match result with
  | Yay x -> Yay (f x)
  | Nay x -> Nay x
let (|>>) s f = SkogixResultBind f s