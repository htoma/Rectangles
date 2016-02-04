module ImageDrawing
    open System
    open System.Drawing
    open Elements

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
