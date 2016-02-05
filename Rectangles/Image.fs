module ImageDrawing
    open System
    open System.Drawing    
    open Elements
    open ImageMagick

    let fillRectangle (rect:Rectangle) (image:Bitmap) (color:Color) = 
        for i in [rect.Left..rect.Left+rect.Width-1] do
            for j in [rect.Top..rect.Top+rect.Height-1] do
                image.SetPixel(i,j,color)

    let createImage (width:int) (height:int) (first:Rectangle) (second:Rectangle) =
        let image = new Bitmap(width, height)
        fillRectangle {Left=0;Top=0;Width=width;Height=height} image Color.Red
        fillRectangle first image Color.BurlyWood
        fillRectangle second image Color.BurlyWood
        match intersect first second with
        | Some rect -> fillRectangle rect image Color.Red
        | _ -> ()        
        image

    let createGif width height (rectangles:(Rectangle*Rectangle) list) (filePath:string) =
        let folder = IO.Path.Combine(IO.Path.GetTempPath(), DateTime.Now.Ticks.ToString())
        let folderInfo = IO.Directory.CreateDirectory folder
        use collection = new MagickImageCollection()
        rectangles
        |> List.iteri (fun i (r1, r2) -> 
                use image = createImage width height r1 r2
                let name = IO.Path.Combine(folder, sprintf "%i.gif" i)
                image.Save(name,Drawing.Imaging.ImageFormat.Gif)
                collection.Add(name)
                collection.[i].AnimationDelay<-50
                )
        collection.Write(filePath)
        IO.Directory.Delete(folder,true)