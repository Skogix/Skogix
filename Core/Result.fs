module Core.Result

type SkogixOption<'t> =
  | Yay of 't
  | Nay of 't
let SkogixOptionBind<'t> (f: 't -> 't) (result:SkogixOption<'t>) =
  match result with
  | Yay x -> Yay (f x)
  | Nay x -> Nay x