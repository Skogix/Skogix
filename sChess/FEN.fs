module sChess.FEN
  open sChess.Entities
  open System
  type Glyph = char
  let starting = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
  let game1 = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1"
  let game2 = "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2"
  let game3 = "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2"
  let glyphToY (glyph:Glyph) :Y= 
    match glyph with
    | '1' -> 1
    | '2' -> 2
    | '3' -> 3
    | '4' -> 4
    | '5' -> 5
    | '6' -> 6
    | '7' -> 7
    | '8' -> 8
    | _ -> failwith "parsern bör ge rätt värde"
  let glyphToX (glyph:Glyph) :X= 
    match glyph |> System.Char.ToLower with
    | 'a' -> 1
    | 'b' -> 2
    | 'c' -> 3
    | 'd' -> 4
    | 'e' -> 5
    | 'f' -> 6
    | 'g' -> 7
    | 'h' -> 8
    | _ -> failwith "parsern bör ge rätt värde"
  let glyphToPiece (glyph:Glyph)= 
    match glyph with
    | 'k' -> Some {color=Black;rank = King}
    | 'q' -> Some {color=Black;rank = Queen}
    | 'r' -> Some {color=Black;rank = Rook}
    | 'b' -> Some {color=Black;rank = Bishop}
    | 'n' -> Some {color=Black;rank = Knight}
    | 'p' -> Some {color=Black;rank = Pawn}
    | 'K' -> Some {color=White;rank = King}
    | 'Q' -> Some {color=White;rank = Queen}
    | 'R' -> Some {color=White;rank = Rook}
    | 'B' -> Some {color=White;rank = Bishop}
    | 'N' -> Some {color=White;rank = Knight}
    | 'P' -> Some {color=White;rank = Pawn}
    | '.' -> None
    | _ -> failwith "parsern bör ge rätt värde"
  let getGlyphFromPiece (piece:Piece option) =
    match piece with
    | Some x -> 
      match (x.color, x.rank) with
      | White, King -> "K"
      | White, Queen -> "Q"
      | White, Rook -> "R"
      | White, Bishop -> "B"
      | White, Knight -> "N"
      | White, Pawn -> "P"
      | Black, King -> "k"
      | Black, Queen -> "q"
      | Black, Rook -> "r"
      | Black, Bishop -> "b"
      | Black, Knight -> "n"
      | Black, Pawn -> "p"
    | None -> "."
  let rankGlyphs : list<Glyph>= 
    let black = ['k';'q';'r';'b';'n';'p']
    let white = 
      black 
      |> List.map(fun x -> x |> System.Char.ToUpper)
    black 
    |> List.append white
    |> List.append ['.']
  let positions = 
    [for i in [1..8] do [for j in [1..8] do i, j]] 
    |> List.concat
    |> List.map(fun (x,y) -> Position.create x y)

  let printBoard (board:Board) = 
    Console.Clear()
    board.squares
    |> List.iter(fun square -> 
      let x = square.position.x
      let y = square.position.y
      let piece = square.piece
      Console.SetCursorPosition(y,x)
      piece
      |> getGlyphFromPiece
      |> printfn "%s" 
    )