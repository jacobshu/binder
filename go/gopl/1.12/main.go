package main

import (
	"image"
	"image/color"
	"image/gif"
	"io"
	"log"
	"math"
	"math/rand"
	"net/http"
	"strconv"
)

var palette = []color.Color{
  color.RGBA{0x28, 0x30, 0x38, 0xff}, // bg
  // color.RGBA{0x37, 0x41, 0x49, 0xff}, // black
  // color.RGBA{0xda, 0x72, 0x80, 0xff}, // red
  // color.RGBA{0xa8, 0xc1, 0x80, 0xff}, // green
  // color.RGBA{0xdb, 0xba, 0x79, 0xff}, // yellow
  // color.RGBA{0x78, 0xb1, 0xbd, 0xff}, // blue
  // color.RGBA{0xd4, 0x91, 0xb5, 0xff}, // magenta
  // color.RGBA{0x7b, 0xc2, 0x9a, 0xff}, // cyan
  // color.RGBA{0xd0, 0xc9, 0xa1, 0xff}, // white
  // color.RGBA{0x52, 0x65, 0x64, 0xff}, // brightBlack
  color.RGBA{0xda, 0x72, 0x80, 0xff}, // brightRed
  color.RGBA{0xa8, 0xc1, 0x80, 0xff}, // brightGreen
  color.RGBA{0xdb, 0xba, 0x79, 0xff}, // brightYellow
  color.RGBA{0x78, 0xb1, 0xbd, 0xff}, // brightBlue
  color.RGBA{0xd4, 0x91, 0xb5, 0xff}, // brightMagenta
  color.RGBA{0x7b, 0xc2, 0x9a, 0xff}, // brightCyan
  // color.RGBA{0xf8, 0xf7, 0xf2, 0xff}, // brightWhite
}

const (
  whiteIndex = 0
  blackIndex = 1
)

func main() {
	http.HandleFunc("/", handler)
	log.Fatal(http.ListenAndServe("localhost:8000", nil))
}

func handler(w http.ResponseWriter, r *http.Request) {
  q := r.URL.Query()
  cycles, err := strconv.ParseFloat(q.Get("cycles"), 64)
  if err != nil {
    cycles = 5
  }
  res, err := strconv.ParseFloat(q.Get("res"), 64)
  if err != nil {
    res = 0.001
  }
	lissajous(w, cycles, res)
}


func lissajous(out io.Writer, cycles float64, res float64) {
	const (
		size    = 100
		nframes = 64
		delay   = 8
	)

	freq := rand.Float64() * 3.0
	anim := gif.GIF{LoopCount: nframes}
	phase := 0.0
	for i := 0; i < nframes; i++ {
		rect := image.Rect(0, 0, 2*size+1, 2*size+1)
		img := image.NewPaletted(rect, palette)
		for t := 0.0; t < cycles*2*math.Pi; t += res {
			x := math.Sin(t)
			y := math.Sin(t*freq + phase)
			img.SetColorIndex(
				size+int(x*size+0.5),
				size+int(y*size+0.5),
        uint8(4),
				// uint8(int(t)%len(palette)-1),
			)
		}
		phase += 0.1
		anim.Delay = append(anim.Delay, delay)
		anim.Image = append(anim.Image, img)
	}
	gif.EncodeAll(out, &anim)
}
