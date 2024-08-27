package main

import (
	"fmt"
	"os"
	"strconv"
)

func main() {
	if len(os.Args) < 3 {
		fmt.Fprintf(os.Stderr, "provide type and value")
    os.Exit(1)
	}
	n := os.Args[2]
	t := os.Args[1]
	f, err := strconv.ParseFloat(n, 64)
	if err != nil {
		fmt.Fprintf(os.Stderr, "convert: %v\n", err)
		os.Exit(1)
	}

	switch t {
	case "kg":
		fmt.Fprintf(os.Stdout, "%v == %v", Kilogram(f).String(), KgToLb(Kilogram(f)))
	case "lb":
		fmt.Fprintf(os.Stdout, "%v == %v", Pound(f).String(), LbToKg(Pound(f)))
	case "m":
		fmt.Fprintf(os.Stdout, "%v == %v", Meter(f).String(), MToFt(Meter(f)))
	case "ft":
		fmt.Fprintf(os.Stdout, "%v == %v", Foot(f).String(), FtToM(Foot(f)))
	case "C":
		fmt.Fprintf(os.Stdout, "%v == %v == %v", Celsius(f).String(), CtoF(Celsius(f)).String(), CtoK(Celsius(f)).String())
	case "K":
		fmt.Fprintf(os.Stdout, "%v == %v == %v", Kelvin(f).String(), KtoF(Kelvin(f)).String(), KtoF(Kelvin(f)).String())
	case "F":
		fmt.Fprintf(os.Stdout, "%v == %v == %v", Fahrenheit(f).String(), FtoC(Fahrenheit(f)).String(), FtoK(Fahrenheit(f)).String())
	}
}
