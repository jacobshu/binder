package main

import (
	"fmt"
	"os"
)

func main() {
	s, sep := "", "\n"
	for i, arg := range os.Args {
		s += fmt.Sprint(i, " ", arg, sep)
	}
	fmt.Println(s)
}
