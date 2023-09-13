// Copyright © 2016 Alan A. A. Donovan & Brian W. Kernighan.
// License: https://creativecommons.org/licenses/by-nc-sa/4.0/

// Run with "web" command-line argument for web server.
// See page 13.
//!+main

// Lissajous generates GIF animations of random Lissajous figures.
package main

import (
	"image"
	"image/color"
	"image/gif"
	"io"
	"math"
	"math/rand"
	"os"
)

//!-main
// Packages not needed by version in book.
import (
	"log"
	"net/http"
	"time"
)

//!+main
var palette = []color.Color{
  color.White,
  color.RGBA{0x23, 0x2b, 0x31, 0xff},
  color.RGBA{0x28, 0x30, 0x38, 0xff},
  color.RGBA{0x2e, 0x37, 0x3f, 0xff},
  color.RGBA{0x37, 0x41, 0x49, 0xff},
  color.RGBA{0x40, 0x4b, 0x54, 0xff},
  color.RGBA{0x47, 0x52, 0x5c, 0xff},
  color.RGBA{0x52, 0x65, 0x64, 0xff},
  color.RGBA{0x51, 0x3c, 0x40, 0xff},
  color.RGBA{0x56, 0x39, 0x45, 0xff},
  color.RGBA{0x37, 0x4b, 0x5f, 0xff},
  color.RGBA{0x3c, 0x4c, 0x44, 0xff},
  color.RGBA{0x4f, 0x4f, 0x41, 0xff},
  color.RGBA{0xd0, 0xc9, 0xa1, 0xff},
  color.RGBA{0xda, 0x72, 0x80, 0xff},
  color.RGBA{0xed, 0xa1, 0x73, 0xff},
  color.RGBA{0xdb, 0xba, 0x79, 0xff},
  color.RGBA{0xa8, 0xc1, 0x80, 0xff},
  color.RGBA{0x7b, 0xc2, 0x9a, 0xff},
  color.RGBA{0x78, 0xb1, 0xbd, 0xff},
  color.RGBA{0xd4, 0x91, 0xb5, 0xff},
  color.RGBA{0x73, 0x84, 0x78, 0xff},
  color.RGBA{0x7f, 0x92, 0x8d, 0xff},
  color.RGBA{0x95, 0xa8, 0xa2, 0xff},
  color.RGBA{0x7e, 0xb3, 0x65, 0xff},
  color.RGBA{0x78, 0x84, 0x92, 0xff},
  color.RGBA{0xe6, 0x82, 0x74, 0xff},
}

const (
  bg_dim        = 1
  bg0           = 2
  bg1           = 3
  bg2           = 4
  bg3           = 5
  bg4           = 6
  bg5           = 7
  bg_red        = 8
  bg_visual     = 9
  bg_blue       = 10
  bg_green      = 11
  bg_yellow     = 12
  fg            = 13
  red           = 14
  orange        = 15
  yellow        = 16
  green         = 17
  aqua          = 18
  blue          = 19
  purple        = 20
  grey0         = 21
  grey1         = 22
  grey2         = 23
  statusline1   = 24
  statusline2   = 25
  statusline3   = 26
)

func main() {
	//!-main
	// The sequence of images is deterministic unless we seed
	// the pseudo-random number generator using the current time.
	// Thanks to Randall McPherson for pointing out the omission.
	rand.Seed(time.Now().UTC().UnixNano())

	if len(os.Args) > 1 && os.Args[1] == "web" {
		//!+http
		handler := func(w http.ResponseWriter, r *http.Request) {
			lissajous(w)
		}
		http.HandleFunc("/", handler)
		//!-http
		log.Fatal(http.ListenAndServe("localhost:8000", nil))
		return
	}
	//!+main
	lissajous(os.Stdout)
}

func lissajous(out io.Writer) {
	const (
		cycles  = 2     // number of complete x oscillator revolutions
		res     = 0.001 // angular resolution
		size    = 64   // image canvas covers [-size..+size]
		nframes = 64    // number of animation frames
		delay   = 8     // delay between frames in 10ms units
	)
	freq := rand.Float64() * 3.0 // relative frequency of y oscillator

  var disposal = make([]byte, nframes, nframes)
  disposal[0] = gif.DisposalBackground
  for j := 1; j < len(disposal); j *= 2 {
    copy(disposal[j:], disposal[:j])
  }

  anim := gif.GIF{
    LoopCount: nframes,
    Disposal: disposal,
    BackgroundIndex: 18,
  }
	phase := 0.0 // phase difference
	for i := 0; i < nframes; i++ {
		rect := image.Rect(0, 0, 2*size+1, 2*size+1)
		img := image.NewPaletted(rect, palette)
    var color byte = 14
		for t := 0.0; t < cycles*2*math.Pi; t += res {
			x := math.Sin(t)
			y := math.Sin(t*freq + phase)
			img.SetColorIndex(size+int(x*size+0.5), size+int(y*size+0.5),
				color)
      //color++
		}
		phase += 0.1
		anim.Delay = append(anim.Delay, delay)
		anim.Image = append(anim.Image, img)
	}
	gif.EncodeAll(out, &anim) // NOTE: ignoring encoding errors
}

//!-main
