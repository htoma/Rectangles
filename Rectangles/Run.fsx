#I @"../packages/" 

#r "NUnit.3.0.1/lib/net45/nunit.framework.dll"
#r "Fuchu.1.0.3.0/lib/Fuchu.dll"
#r "FsCheck.2.2.4/lib/net45/FsCheck.dll"
#r "Fuchu.FsCheck.1.0.3.0/lib/Fuchu.FsCheck.dll"
#r "Magick.NET-Q16-AnyCPU.7.0.0.0101/lib/net40-client/Magick.NET-Q16-AnyCPU.dll"

#r @"bin/debug/rectangles.dll"

open System
open NUnit.Framework
open Fuchu
open FsCheck
open Elements
open ImageDrawing

let generator widthMax heightMax offset =
    gen {
        let! left = Gen.choose(0, widthMax-offset)
        let! top = Gen.choose(0, heightMax-offset)
        let! width = Gen.choose(offset, widthMax-left)
        let! height = Gen.choose(offset, heightMax-top)
        return { Left=left
                 Top=top
                 Width=width
                 Height=height
                }
    }

let width = 400
let height = 200

let rectangles = Gen.sample 0 100 (generator width height 10) |> List.pairwise
createGif width height rectangles @"c:\temp\result.gif"