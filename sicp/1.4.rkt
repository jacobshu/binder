#lang sicp
(define (a-plus-abs-b a b)
  ((if (> b 0) + -) a b))

; the operator is conditionally add or subtract based on whether b is greater than zero
