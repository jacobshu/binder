// Copyright © 2016 Alan A. A. Donovan & Brian W. Kernighan.
// License: https://creativecommons.org/licenses/by-nc-sa/4.0/

// Adapted to accept arbitrary palettes and to colorize the result
//!+main

// Lissajous generates GIF animations of random Lissajous figures.
package main

import (
  "fmt"
	"image"
	"image/color"
	"image/gif"
	"io"
  "log"
	"math"
	"math/rand"
  "os"
	"time"
)

type point struct {
  x int 
  y int
  color int
}

// !-main
func lissajous(out io.Writer, palette color.Palette) {
	const (
		cycles  = 2     // number of complete x oscillator revolutions
		res     = 0.001 // angular resolution
		size    = 64    // image canvas covers [-size..+size]
		nframes = 64    // number of animation frames
		delay   = 8     // delay between frames in 10ms units
	)

  f, err := os.OpenFile("test.log", os.O_APPEND | os.O_CREATE | os.O_RDWR, 0666)
  if err != nil {
      fmt.Printf("error opening file: %v", err)
  }

  // don't forget to close it
  defer f.Close()
  log.SetOutput(f)

	rand.Seed(time.Now().UTC().UnixNano())
	freq := rand.Float64() * 3.0 // relative frequency of y oscillator
	anim := gif.GIF{LoopCount: nframes}
	phase := 0.0 // phase difference

  // a slice of random points encompassing the frame and points slightly above
  var d [512]point
  for i, _ := range d {
    d[i] = createPoint()     
  }

	for i := 0; i < nframes; i++ {
    log.Printf("i: %d, point: %+v", i, d[0])
		rect := image.Rect(0, 0, 2*size+1, 2*size+1)
		img := image.NewPaletted(rect, palette)


    // background fill
		for y := 0; y < size*2+1; y++ {
      bg := byte(10 - (math.Floor(float64(y) / 13.0)))
			for x := 0; x < size*2+1; x++ {
        img.SetColorIndex(x, y, bg)
			}
		}

    // texturing
    for j, p := range d {
     if p.y > 0 {
       img.SetColorIndex(p.x, p.y, byte(p.color))
       for k := 0; k < 7; k++ {
         c := byte(p.color - 1)
         if c > 6 {
           img.SetColorIndex(p.x, p.y-k, c)
         }
       }
     }

     d[j].y++
     d[j].color = p.color - 1
     if d[j].color < 7 {
       d[j] = createPoint()
     }
    }

		var color byte = 11
    var count = 0
		for t := 0.0; t < cycles*2*math.Pi; t += res {
			x := math.Sin(t)
			y := math.Sin(t*freq + phase)
			img.SetColorIndex(size+int(x*size+0.5), size+int(y*size+0.5),
				color)
			if count > 499 {
				if color == 15 {
					color = 11
				} else {
					color++
				}
				count = 0
			} else {
				count++
			}
		}

		phase += 0.1
		anim.Delay = append(anim.Delay, delay)
		anim.Image = append(anim.Image, img)
	}

	gif.EncodeAll(out, &anim) // NOTE: ignoring encoding errors
}

func createPoint() point {
  lx := rand.Intn(129)
  ly := rand.Intn(129)
  brightestColor := 10 - rand.Intn(2)
  return point{x: lx, y: ly, color: brightestColor} 
}

//!-main
