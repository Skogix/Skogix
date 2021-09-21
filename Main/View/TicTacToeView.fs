module Main.View.TicTacToeView

open System.ComponentModel
open Avalonia.Controls
open Avalonia.Controls.Primitives
open Avalonia.Controls.Shapes
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Avalonia.Media
open Core.Input
open Core.State

let view (shellState:ShellState) dispatch =
  DockPanel.create [
    DockPanel.children [
      Rectangle.create [
        Rectangle.fill "gray"
        Rectangle.height 100.
        Rectangle.dock Dock.Top
      ]
      Rectangle.create [
        Rectangle.fill "gray"
        Rectangle.height 100.
        Rectangle.dock Dock.Bottom
      ]
      Rectangle.create [
        Rectangle.fill "gray"
        Rectangle.width 100.
        Rectangle.dock Dock.Left
      ]
      Rectangle.create [
        Rectangle.fill "gray"
        Rectangle.width 100.
        Rectangle.dock Dock.Right
      ]
      StackPanel.create [
        StackPanel.children [
          UniformGrid.create [
            UniformGrid.columns 3
            UniformGrid.rows 3
            UniformGrid.horizontalAlignment HorizontalAlignment.Center
            UniformGrid.verticalAlignment VerticalAlignment.Center
            UniformGrid.children [
              for map in shellState.ticTacToe.squareMap do
                let id, value = map.Deconstruct()
                let text =
                  match value with
                  | Some v ->
                    match v with
                    | Core.Domain.TicTacToe.Circle -> "O"
                    | Core.Domain.TicTacToe.Cross -> "X"
                  | None -> ""
                Button.create [
                  Button.width 200.
                  Button.height 200.
                  Button.margin 10.
                  Button.content (string text)
                  Button.onClick (fun _ -> dispatch (TryPlaceMove id))
                ]
            ]
          ]
        ]
      ]
    ]
  ]
