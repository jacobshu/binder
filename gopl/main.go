// Copyright © 2016 Alan A. A. Donovan & Brian W. Kernighan.
// License: https://creativecommons.org/licenses/by-nc-sa/4.0/

// Run with "web" command-line argument for web server.
// See page 13.
//!+main

// Lissajous generates GIF animations of random Lissajous figures.
package main

import (
	"image/color"
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

// !+main
var palette = []color.Color{
	color.White,
	color.RGBA{0x23, 0x2b, 0x31, 0xff}, // bg_dim
	color.RGBA{0x28, 0x30, 0x38, 0xff}, // bg0
	color.RGBA{0x2e, 0x37, 0x3f, 0xff}, // bg1
	color.RGBA{0x37, 0x41, 0x49, 0xff}, // bg2
	color.RGBA{0x40, 0x4b, 0x54, 0xff}, // bg3
	color.RGBA{0x47, 0x52, 0x5c, 0xff}, // bg4
	color.RGBA{0x52, 0x65, 0x64, 0xff}, // bg5
	color.RGBA{0x51, 0x3c, 0x40, 0xff}, // bg_red
	color.RGBA{0x56, 0x39, 0x45, 0xff}, // bg_visual
	color.RGBA{0x37, 0x4b, 0x5f, 0xff}, // bg_blue
	color.RGBA{0x3c, 0x4c, 0x44, 0xff}, // bg_green
	color.RGBA{0x4f, 0x4f, 0x41, 0xff}, // bg_yellow
	color.RGBA{0xd0, 0xc9, 0xa1, 0xff}, // fg
	color.RGBA{0xda, 0x72, 0x80, 0xff}, // red
	color.RGBA{0xed, 0xa1, 0x73, 0xff}, // orange
	color.RGBA{0xdb, 0xba, 0x79, 0xff}, // yellow
	color.RGBA{0xa8, 0xc1, 0x80, 0xff}, // green
	color.RGBA{0x7b, 0xc2, 0x9a, 0xff}, // aqua
	color.RGBA{0x78, 0xb1, 0xbd, 0xff}, // blue
	color.RGBA{0xd4, 0x91, 0xb5, 0xff}, // purple
	color.RGBA{0x73, 0x84, 0x78, 0xff}, // grey0
	color.RGBA{0x7f, 0x92, 0x8d, 0xff}, // grey1
	color.RGBA{0x95, 0xa8, 0xa2, 0xff}, // grey2
	color.RGBA{0x7e, 0xb3, 0x65, 0xff}, // statusline1
	color.RGBA{0x78, 0x84, 0x92, 0xff}, // statusline2
	color.RGBA{0xe6, 0x82, 0x74, 0xff}, // statusline3
}

func main() {
	//!-main
	// The sequence of images is deterministic unless we seed
	// the pseudo-random number generator using the current time.
	// Thanks to Randall McPherson for pointing out the omission.
	rand.Seed(time.Now().UTC().UnixNano())

	if len(os.Args) > 1 && os.Args[1] == "web" {
		//!+http
		handler := func(w http.ResponseWriter, r *http.Request) {
			lissajous(w, palette)
		}
		http.HandleFunc("/", handler)
		//!-http
		log.Fatal(http.ListenAndServe("localhost:8000", nil))
		return
	}
	//!+main
	lissajous(os.Stdout, palette)
}

//!-main
