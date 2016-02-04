module Elements
    open System

    type Rectangle =
        { Top: int
          Left: int
          Width: int
          Height: int }

    let intersect (first:Rectangle) (second:Rectangle) = 
        if (first.Left + first.Width < second.Left) ||
            (second.Left + second.Width < first.Left) ||
            (first.Top + first.Height < second.Top) ||
            (second.Top + second.Height < first.Top) then None
        else
            let left = Math.Max(first.Left, second.Left)
            let top = Math.Max(first.Top, second.Top)
            Some(
                { Left=left
                  Top=top
                  Width=Math.Min(first.Left+first.Width, second.Left+second.Width)-left
                  Height=Math.Min(first.Top+first.Height, second.Top+second.Height)-top
                })
