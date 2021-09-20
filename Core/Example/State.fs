module Core.Example.State

open Elmish
type State = { count: int; running: bool  }
let init =
  let initState = { count = 0; running = false }
  initState, Cmd.none
