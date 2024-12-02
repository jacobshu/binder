package main

import "fmt"

type Celsius float64
type Fahrenheit float64
type Kelvin float64
type Pound float64
type Kilogram float64
type Foot float64
type Meter float64

const (
	AbsoluteZeroK Kelvin  = 0
	AbsoluteZeroC Celsius = -273.5
	FreezingC     Celsius = 0
	BoilingC      Celsius = 100
	LbPerKg       Pound   = 0.45359237
	MPerFt        Meter   = 0.3048
)

func (c Celsius) String() string    { return fmt.Sprintf("%g°C", c) }
func (f Fahrenheit) String() string { return fmt.Sprintf("%g°F", f) }
func (k Kelvin) String() string     { return fmt.Sprintf("%g°K", k) }
func (l Pound) String() string      { return fmt.Sprintf("%glbs", l) }
func (k Kilogram) String() string   { return fmt.Sprintf("%gkgs", k) }
func (f Foot) String() string       { return fmt.Sprintf("%gft", f) }
func (m Meter) String() string      { return fmt.Sprintf("%gm", m) }

func CtoF(c Celsius) Fahrenheit { return Fahrenheit(c*9/5 + 32) }

func FtoC(f Fahrenheit) Celsius { return Celsius((f - 32) * 5 / 9) }

func CtoK(c Celsius) Kelvin { return Kelvin(c + (-1 * AbsoluteZeroC)) }

func FtoK(f Fahrenheit) Kelvin { return CtoK(FtoC(f)) }

func KtoC(k Kelvin) Celsius { return Celsius(k + AbsoluteZeroK) }

func KtoF(k Kelvin) Fahrenheit { return CtoF(KtoC(k)) }

func LbToKg(l Pound) Kilogram { return Kilogram(l * LbPerKg) }

func KgToLb(k Kilogram) Pound { return Pound(k / Kilogram(LbPerKg)) }

func MToFt(m Meter) Foot { return Foot(m / MPerFt) }

func FtToM(f Foot) Meter { return Meter(f * Foot(MPerFt)) }
