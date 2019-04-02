module Tests

open System

open SqlBits
open Xunit

[<Fact>]
let ``SELECT 1`` () =
    let q = select ["1"]
    Assert.Equal("SELECT 1", build q)

[<Fact>]
let ``SELECT user_id FROM user`` () =
    let q = 
      select ["user_id"] >> 
      from "user"
      
    Assert.Equal("SELECT user_id FROM user", build q)

[<Fact>]
let ``SELECT user_id FROM user WHERE user_id = 1`` () =
    let q = 
      select ["user_id"] >> 
      from "user" >> 
      whereAnd ["user_id = 1"]
    Assert.Equal("SELECT user_id FROM user WHERE user_id = 1", build q)