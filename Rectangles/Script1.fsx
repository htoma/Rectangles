#I @"../packages/" 

#r "NUnit.3.0.1/lib/net45/nunit.framework.dll"
#r "Fuchu.1.0.3.0/lib/Fuchu.dll"
#r "FsCheck.2.2.4/lib/net45/FsCheck.dll"
#r "Fuchu.FsCheck.1.0.3.0/lib/Fuchu.FsCheck.dll"

#r @"bin/debug/rectangles.dll"

open System
open NUnit.Framework
open Fuchu
open FsCheck
open Elements
open ImageDrawing

let chooseRectangle widthMax heightMax offset =
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

let folder = IO.Path.Combine(IO.Path.GetTempPath(), DateTime.Now.Ticks.ToString())
IO.Directory.CreateDirectory folder

let width = 400
let height = 200
Gen.sample 0 1000 (chooseRectangle width height 10)
|> List.pairwise 
|> List.iteri (fun i (r1, r2) -> 
        use image = createImage width height r1 r2
        image.Save(IO.Path.Combine(folder, sprintf "%i.gif" i),Drawing.Imaging.ImageFormat.Gif))

IO.Directory.Delete(folder,true)