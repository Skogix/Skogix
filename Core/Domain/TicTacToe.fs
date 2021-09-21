module Core.Domain.TicTacToe

type Player =
  | O
  | X

type Id = int
type SquareMap = Map<Id, Player option>

let winCombinations =
  [ // kanske lite fult att hårdkoda, men är så få kombinationer
    [ 0; 1; 2 ]
    [ 3; 4; 5 ]
    [ 6; 7; 8 ]

    [ 0; 3; 6 ]
    [ 1; 4; 7 ]
    [ 2; 5; 8 ]

    [ 0; 4; 8 ]
    [ 2; 4; 6 ]
  ]
