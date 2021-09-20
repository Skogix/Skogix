module Main.DebugPage

open Core.State
open Elmish

let update message (state:ShellState) =
  state.DebugManager, Cmd.none

