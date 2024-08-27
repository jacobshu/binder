package main

import "fmt"

type Celsius float64
type Fahrenheit float64
type Kelvin float64

const (
	AbsoluteZeroK Kelvin  = 0
	AbsoluteZeroC Celsius = -273.5
	FreezingC     Celsius = 0
	BoilingC      Celsius = 100
)

func (c Celsius) String() string    { return fmt.Sprintf("%g°C", c) }
func (f Fahrenheit) String() string { return fmt.Sprintf("%g°F", f) }
func (k Kelvin) String() string     { return fmt.Sprintf("%g°K", k) }

func CtoF(c Celsius) Fahrenheit { return Fahrenheit(c*9/5 + 32) }

func FtoC(f Fahrenheit) Celsius { return Celsius((f - 32) * 5 / 9) }

func CtoK(c Celsius) Kelvin { return Kelvin(c + (-1 * AbsoluteZeroC)) }

func FtoK(f Fahrenheit) Kelvin { return CtoK(FtoC(f)) }

func KtoC(k Kelvin) Celsius { return Celsius(k + AbsoluteZeroK) }

func KtoF(k Kelvin) Fahrenheit { return CtoF(KtoC(k)) }
