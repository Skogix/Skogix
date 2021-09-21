module Core.TicTacToe.Update

open Core.State
open Core.Domain.TicTacToe

let switchTurn (state: TicTacToeOutput) =
  match state.currentTurn with
  | X -> { state with currentTurn = O }
  | O -> { state with currentTurn = X }

let addMove move state =
  { state with
      squareMap = state.squareMap.Add(move, Some state.currentTurn)
  }

let tryAddMove move state =
  //  printfn $"tryaddmove {move}: {state.squareMap}"
  match state.squareMap.TryFind(move) with
  | Some square ->
    match square.IsSome with
    | true -> // squaren har redan en player
      Core.Skogix.Result.Nay state
    | false -> Core.Skogix.Result.Yay(addMove move state)
  | None -> // ska aldrig hända, får ett id som inte finns
    Core.Skogix.Result.Nay state











let checkWinner (state: TicTacToeOutput) =
  let tryGetWinner (winCombinations: int list) =
    let squaresWithPlayer =
      winCombinations
      |> List.map (fun i -> state.squareMap.[i])
      |> List.filter (fun x -> x.IsSome)

    let amountCircle =
      squaresWithPlayer
      |> List.filter (fun x -> x.Value = O)
      |> List.length

    let amountCross =
      squaresWithPlayer
      |> List.filter (fun x -> x.Value = X)
      |> List.length

    match amountCross = 3, amountCircle = 3 with
    | true, _ -> (Some O)
    | _, true -> (Some X)
    | _, _ -> None

  let rec recTryGetWinner posLists =
    match posLists with
    | [] -> None
    | head :: tail ->
      match tryGetWinner head with
      | Some winner -> Some winner
      | None -> recTryGetWinner tail

  let winner = winCombinations |> recTryGetWinner

  match winner with
  | Some X -> { state with winner = Some X }
  | Some O -> { state with winner = Some O }
  | None -> state
