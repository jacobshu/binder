package main

import (
  "fmt"
  "os"
  "strings"
)

func slowPrint() {
  s, sep := "", ""
  for _, arg := range os.Args {
    s += sep + arg
    sep = " "
  }
  fmt.Println(s)
}

func fastPrint() {
  fmt.Println(strings.Join(os.Args[1:], " "))
}
