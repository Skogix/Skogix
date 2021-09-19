namespace Main

open System
open Avalonia.Threading
open Elmish
open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Input
open Avalonia.FuncUI
open Avalonia.FuncUI.Elmish
open Avalonia.FuncUI.Components.Hosts
open Main.Counter

type MainWindow() as this =
    inherit HostWindow()
    do
        base.Title <- "Main"
        base.Width <- 400.0
        base.Height <- 400.0
        
        let timer (state: State) = // exempel på timer som kors från ui
          let sub (dispatch: Msg -> unit) = // subscriber
            let invoke() =
              IncrementIfRunning |> dispatch // vad som invokeas
              true // fortsätta koras
            
            DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 1000.) |> ignore // i princip en async.sleep 
          Cmd.ofSub sub // skickar command med en subrutin
        this.VisualRoot.VisualRoot.Renderer.DrawFps <- true
        this.VisualRoot.VisualRoot.Renderer.DrawDirtyRects <- true


        Elmish.Program.mkProgram (fun () -> init()) update view
        |> Program.withHost this
        |> Program.withSubscription timer 
        |> Program.withConsoleTrace
        |> Program.run

        
type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Load "avares://Avalonia.Themes.Default/DefaultTheme.xaml"
        this.Styles.Load "avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)
