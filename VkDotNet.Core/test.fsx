type Groups = string
type Messages = string

type Method(name: string) = 
    member this.Name = name

type Query<'T> = { Method: 'T;}

let Execute (q: Query<Groups> ) = 12

let message:Messages = "Hey"

let group: Groups = "hey"


printfn "%A" (Execute {Method = message})