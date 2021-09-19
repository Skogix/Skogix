namespace Main

open Avalonia.FuncUI.DSL

module Counter =
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    open Elmish
    
    
    type State = { count : int
                   running: bool }
    let init() = { count = 0
                   running = true }, Cmd.none

    type Msg =
      | IncrementIfRunning 
      | Increment
      | IncrementDelayed
      | Decrement
      | Reset
      | RunningTrue
      | RunningFalse

    let update (msg: Msg) (state: State) : State * Cmd<_> =
        match msg with
        | Increment -> { state with count = state.count + 1 }, Cmd.none
        | Decrement -> { state with count = state.count - 1 }, Cmd.none
        | IncrementIfRunning ->
          match state.running with
          | true -> {state with count = state.count + 1}, Cmd.none
          | false -> state, Cmd.none
        | Reset ->
          {state with count = 0}, Cmd.none
        | IncrementDelayed ->
          let incrementDelayedCmd (dispatch: Msg -> unit) : unit =
            let delayedDispatch = async {
              do! Async.Sleep 1000
              dispatch Increment
            }
            Async.StartImmediate delayedDispatch
          state, Cmd.ofSub incrementDelayedCmd
        | RunningTrue -> {state with running = true}, Cmd.none
        | RunningFalse -> {state with running = false}, Cmd.none
    
    let view (state: State) (dispatch: Msg -> unit) =
        DockPanel.create [
            DockPanel.children [
                Button.create [
                    Button.dock Dock.Bottom
                    Button.height 50.
                    Button.onClick (fun _ -> dispatch IncrementDelayed)
                    Button.content "incrementDelayed"
                ]                
                Button.create [
                    Button.isVisible (not state.running)
                    Button.dock Dock.Bottom
                    Button.height 50.
                    Button.onClick (fun _ -> dispatch RunningTrue)
                    Button.content "runningTrue"
                ]                
                Button.create [
                    Button.isVisible (state.running)
                    Button.dock Dock.Bottom
                    Button.height 50.
                    Button.onClick (fun _ -> dispatch RunningFalse)
                    Button.content "runningFalse"
                ]                
                Button.create [
                    Button.height 50.
                    Button.dock Dock.Bottom
                    Button.onClick (fun _ -> dispatch Reset)
                    Button.content "reset"
                ]
                Button.create [
                    Button.height 50.
                    Button.dock Dock.Bottom
                    Button.onClick (fun _ -> dispatch Decrement)
                    Button.content "-"
                ]
                Button.create [
                    Button.dock Dock.Bottom
                    Button.onClick (fun _ -> dispatch Increment)
                    Button.content "+"
                ]
                TextBlock.create [
                    TextBlock.dock Dock.Top
                    TextBlock.fontSize 48.0
                    TextBlock.verticalAlignment VerticalAlignment.Center
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                    TextBlock.text (string state.running)
                ]
                TextBlock.create [
                    TextBlock.dock Dock.Top
                    TextBlock.fontSize 48.0
                    TextBlock.verticalAlignment VerticalAlignment.Center
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                    TextBlock.text (string state.count)
                ]
            ]
        ]