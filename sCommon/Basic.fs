module sCommon.Basic

module Map =
    /// Wrapper type, Monadic type
    /// map, lift, select
    /// <$> <!>
    /// lifts a function
    /// (a-b) -> X<a> -> X<b>
    let mapOption f opt =
        match opt with
        | None -> None
        | Some x -> Some(f x)

    let rec mapList f list =
        match list with
        | [] -> []
        | first :: rest -> (f first) :: (mapList f rest)

module Return =
    /// return, pure, unit, yield, point
    /// lifts a single value
    /// a -> X<a>
    let returnOption x = Some x

    let returnList x = [ x ]

module Apply =
    /// Applicative Functor
    /// apply, ap
    /// <*>
    /// unpacks a lifted function from a lifted value to a lifted function
    /// X<(a->b> -> X<a> -> X<b>
    let applyOption fOpt xOpt =
        match fOpt, xOpt with
        | Some f, Some x -> Some(f x)
        | _ -> None

    let applyList (fList: ('a -> 'b) list) (xList: 'a list) =
        [ for f in fList do
              for x in xList do
                  yield f x ]

    let add x y = x + y

    let applyListTest1 =
        let (<*>) = applyList
        [ add ] <*> [ 1; 2 ] <*> [ 10; 20 ]
    // [11;21;12;22]
    let applyResultOption1 =
        let (<*>) = applyOption
        (Some add) <*> (Some 2) <*> (Some 3)
    // Some 5
    let applyResultOption2 =
        let (<!>) = Map.mapOption
        let (<*>) = applyOption
        add <!> (Some 2) <*> (Some 3)

    let applyListTest2 =
        let (<!>) = Map.mapList
        let (<*>) = applyList
        add <!> [ 1; 2 ] <*> [ 10; 20 ]

    let testStringConcat =
        let (<!>) = Map.mapList
        let (<*>) = applyList
        (+) <!> [ "foo"; "bar"; "baz" ] <*> [ "!"; "!?!" ]
// foo!; foo!?"; bar!; bar!?!; baz!; baz!?!
module Lift =
    /// lift2, lift2... (lift1 = map)
    /// combine x lifter values using a function
    /// lift2: (a->b->c) -> X<a> -> X<b> -> X<c>
    let (<*>) = Apply.applyList
    let (<!>) = List.map
    let lift2 f x y = f <!> x <*> y
    let lift3 f x y z = f <!> x <*> y <*> z
    let lift4 f x y z w = f <!> x <*> y <*> z <*> w
    let add x y = x + y
    let addPairList = lift2 add
    //  addPairOption (Some 1) (Some 2) |> ignore
    // Some 3
    let lift2FromScratch f xOpt yOpt =
        match xOpt, yOpt with
        | Some x, Some y -> Some(f x y)
        | _ -> None
    let applyFromLift2 fOpt xOpt = lift2FromScratch id fOpt xOpt
    let (<*) x y = lift2 (fun left right -> left) x y
    let ( *> ) x y = lift2 (fun left right -> left) x y
    [ 1; 2 ] <* [ 3; 4; 5 ] |> ignore
    // [1;1;1;2;2;2]
    [ 1; 2 ] *> [ 3; 4; 5 ] |> ignore
    // [3;4;5;3;4;5]
    let repeat n pattern = [ 1..n ] *> pattern
    let replicate n x = [ 1..n ] *> [ x ]

module Zip =
    /// zip,zip3 map2
    /// <*>
    /// combine two enumerables with a function
    /// E<(a->b->c)> -> E<a> -> E<b> -> E<c> // when E is enumerable
    /// E<a> -> E<b> -> E<a,b> // when E is a tuple
    let rec zipList fList xList =
        match fList, xList with
        | [], _ -> []
        | _, [] -> []
        | (f :: fTail), (x :: xTail) -> (f x) (zipList fTail xTail)

module Bind =
    /// like a bind but monadic instead of applicative
    /// bind, flatMap, andThen, collect, selectMany
    /// >>= (left to right) =<< (right to left)
    /// normal to monadic functions
    /// (a -> X<b>) -> X<a> -> X<B>
    let optionBind f xOpt =
        match xOpt with
        | Some x -> f x
        | _ -> None

    let listBind (f: 'a -> 'b list) (xList: 'a list) =
        [ for x in xList do
              for y in f x do
                  yield y ]

// applicative vs monadic
// applicative;
//   apply, lift, combine
// monadic;
//   bind
//
// do something in paralell and independent data; applicative
// one at a time, skip the next if this fails; monadic
// if dependendencies then you need to be monadic
//
// static vs dynamic
// if applicative then we can define all actions up front / statically
// if monadic then we have more flexibility but cant do things like paralellelization or optimize

type Result<'a> =
    | Success of 'a
    | Failure of string list

module Result =
    // map    (a -> b) -> result<a> -> result<b>
    // ret    a -> result<a>
    // apply  result<(a->b)> -> result<a> -> result<b>
    // bind   (a -> result<b>) -> result<a> -> result<b>

    let map f x =
        match x with
        | Success x -> Success(f x)
        | Failure errs -> Failure errs

    let ret x = Success x

    let apply f x =
        match f, x with
        | Success f, Success x -> Success(f x)
        | Failure errs, Success x -> Failure errs
        | Success x, Failure errs -> Failure errs
        | Failure errs1, Failure errs2 -> Failure(List.concat [ errs1; errs2 ])

    let bind f x =
        match x with
        | Success x -> f x
        | Failure errs -> Failure errs

    let (<!>) = map
    let (<*>) = apply
    let (>>=) x f = bind f x

/// monadic vs applicative
/// applicative
///   all validations up front and combine results
///   pro; didnt lose any validation errors
///   con; may do work that we dont need (still validate second when first fail)
/// monadic
///   chained together
///   pro; short circuit when we get an error
///   con; only gets the first error message
/// computation expressions
/// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/computation-expressions
/// let! M<'T> * ('T -> M<'U>) -> M<'U>
/// do! unit -> M<'T>
/// yield 'T -> M<'T>
/// yield! M<'T> -> M<'T>
/// return 'T -> M<'T>
/// return! M<'T> -> M<'T>
/// match! (let! with pattern match)
///
/// let! binds the result of a call to a name
/// let binds the value of an unrealized call
///   bind; bind(x ,f)
///
/// do! call a computation expression that return unit
///   zero; bind(x, f<*->unit>)
///
/// yield returns a whole value to ienumerable
///   yield(type)
///   most often used with ->
///
/// yield! returns a flattened collection one by one
///   yieldfrom(type)
///
/// return wraps a value to the expressions type
///   return(type)
///   let! x = ...
///   async return -> x
///   // async<x>
///
/// return! realizes the value of the expression
///   returnfrom(type)
///   return! x
///   // async<x>
///
/// match! matches a realized expression
///   match! asyncTryGet x with
///
type ResultBuilder() =
    member this.Return x = Result.ret x
    member this.Bind(x, f) = Result.bind f x

type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) = Some x
// or else
type OrElseBuilder() =
    member this.ReturnFrom(x) = x

    member this.Combine(a, b) =
        match a with
        | Some _ -> a
        | None -> b

    member this.Delay(f) = f ()

module Result2 =
    // map    (a -> b) -> result<a> -> result<b>
    // ret    a -> result<a>
    // apply  result<(a->b)> -> result<a> -> result<b>
    // bind   (a -> result<b>) -> result<a> -> result<b>

    let map f x =
        match x with
        | Success x -> Success(f x)
        | Failure errs -> Failure errs

    let ret x = Success x

    let apply f x =
        match f, x with
        | Success f, Success x -> Success(f x)
        | Failure errs, Success x -> Failure errs
        | Success x, Failure errs -> Failure errs
        | Failure errs1, Failure errs2 -> Failure(List.concat [ errs1; errs2 ])

    let bind f x =
        match x with
        | Success x -> f x
        | Failure errs -> Failure errs

    let (<!>) = map
    let (<*>) = apply

module RailWay =
    type ResultMonad<'result, 'data> =
        | Result of 'result
        | Data of 'data

    let bind func monad =
        match monad with
        | Result result -> Result result
        | Data data -> func data

    let (>>=) monad func =
        match monad with
        | Result result -> Result result
        | Data data -> func data

    let (=<<) monad string =
        match monad with
        | Result result -> result
        | Data _ -> string

