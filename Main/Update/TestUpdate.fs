module Main.TestPage

open Elmish
open Core.Input
open Core.Test

  
let update (message:TestInput) state =
  match message with
  | DoNothing -> Update.doNothing state, Cmd.none
  
  