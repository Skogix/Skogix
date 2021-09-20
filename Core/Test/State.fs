module Core.Test.State

open Elmish
open Core.State

let init = { stringValue = "Test"; intValue = 0; debugOutput = [] }, Cmd.none
