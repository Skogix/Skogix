module Main.View.TicTacToeView

open Avalonia.Controls.Primitives
open Avalonia.Controls.Shapes
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Avalonia.Media

let view state dispatch =
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
              for x in [0..2] do
                for y in [0..2] do
                TextBlock.create [
                  TextBlock.onPointerPressed (fun _ -> Core.Debug.debug $"Klickade {x}, {y}")
                  TextBlock.fontSize 50.
                  TextBlock.textAlignment TextAlignment.Center
                  TextBlock.verticalAlignment VerticalAlignment.Center
                  TextBlock.horizontalAlignment HorizontalAlignment.Center
                  TextBlock.margin 10.
                  TextBlock.background "brownde"
                  TextBlock.height 200.
                  TextBlock.width 200.
                  TextBlock.text $"{x},{y}"
                ]
            ]
          ]
        ]
      ]
    ]
  ]
