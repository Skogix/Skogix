module sCommon.Position

type X = int
type Y = int
type Position = {x:X;y:Y}
module Position =
    let upFrom pos = {x=pos.x;y=pos.y+1}
    let downFrom pos = {x=pos.x;y=pos.y-1}
    let leftFrom pos = {x=pos.x-1;y=pos.y}
    let rightFrom pos = {x=pos.x+1;y=pos.y}
    let getStraightNeighbors pos =
        [ pos |> upFrom
          pos |> rightFrom
          pos |> downFrom
          pos |> leftFrom ]
    let getDiagonalNeighbors pos =
        [ pos |> upFrom |> rightFrom
          pos |> downFrom |> rightFrom
          pos |> downFrom |> leftFrom
          pos |> upFrom |> leftFrom ]
    let getNeighbors pos = (getStraightNeighbors pos) |> List.append (getDiagonalNeighbors pos)
    let create x y = {x=x;y=y}
