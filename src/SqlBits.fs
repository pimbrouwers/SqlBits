module SqlBits 
  open System 
  
  type Query = string list -> string list
  type Keywords = 
    | Select
    | From
    | Where    
  
  let private prefixWhitespace keyword = [keyword; " "]
  let private postfixWhitespace = [" "]    
  
  let private addSeparators sep = List.collect(fun frag -> [sep;frag]) >> List.tail
  let private join sep (items : string list) = String.Join(sep, items)

  let private composePredicate (predicates : (string * string) list) = predicates |> List.collect(fun (op, pred) -> [op;pred]) |> List.tail
  let private compose pre bits = prefixWhitespace pre @ bits @ postfixWhitespace
  let private append keyword (bits : string list) (sql : string list) =    
    sql @ match keyword with
          | Select -> compose "SELECT" (bits |> addSeparators ",")
          | From -> compose "FROM" bits
          | Where -> compose "WHERE" bits   
      
  let build (qry : Query) = [] |> qry |> join "" |> fun s -> s.TrimEnd()
  
  let select =  append Select
  let from table = append From [table]  
  let where = composePredicate >> append Where 
  let whereAnd = addSeparators " AND " >> append Where
  let whereOr = addSeparators " OR " >> append Where
 