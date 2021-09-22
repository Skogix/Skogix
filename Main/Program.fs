namespace Main

open Avalonia.FuncUI.Components.Hosts
open Elmish
open Avalonia.FuncUI.Elmish
open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI
open Main.Shell
/// ToDo
/// flytta subscriptions till Core for separation

/// Program.fs
/// avalonia-haxx for att skapa window och andra automagiska saker.
type MainWindow() as this =
  inherit HostWindow()

  do
    base.Title <- "Main"
    base.Width <- 400.0
    base.Height <- 400.0

    this.VisualRoot.VisualRoot.Renderer.DrawFps <- true
    this.VisualRoot.VisualRoot.Renderer.DrawDirtyRects <- true

    Elmish.Program.mkProgram (fun _ -> Core.Init.shellInit) shellUpdate shellView
    |> Program.withHost this
    //        |> Program.withSubscription ExampleUpdate.timer
    //        |> Program.withSubscription DebugUpdate.timer
    |> Program.withConsoleTrace
    |> Program.run

type App() =
  inherit Application()

  override this.Initialize() =
    this.Styles.Load "avares://Avalonia.Themes.Default/DefaultTheme.xaml"
    this.Styles.Load "avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"

  override this.OnFrameworkInitializationCompleted() =
    match this.ApplicationLifetime with
    | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime -> desktopLifetime.MainWindow <- MainWindow()
    | _ -> ()


module Program =
  [<EntryPoint>]
  let main (args: string []) =
    AppBuilder
      .Configure<App>()
      .UsePlatformDetect()
      .UseSkia()
      .StartWithClassicDesktopLifetime(args)
