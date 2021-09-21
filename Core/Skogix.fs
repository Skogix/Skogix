module Core.Skogix

/// Skogix.fs
/// helpers och omskrivna libraryfunktioner
module Result = 
  type Result<'t> =
    | Yay of 't
    | Nay of 't
  let Bind (f: 't -> 't) (result:Result<'t>) =
    match result with
    | Yay x -> Yay (f x)
    | Nay x -> Nay x
  let Apply result =
    match result with
    | Yay x -> x
    | Nay x -> x
    
let (|>>) s f = Result.Bind f s
