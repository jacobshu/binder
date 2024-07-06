package main

import (
  "testing"
)

func BenchmarkSlowPrint(b *testing.B) {
  for i := 0; i < b.N; i++ {
    slowPrint()
  }
}

func BenchmarkFastPrint(b *testing.B) {
  for i := 0; i < b.N; i++ {
    fastPrint()
  }
}
