#lang sicp
(define (p) (p))

(define (test x y)
  (if (= x 0)
      0
      y))

(test 0 (p))

; Normal order (read: as-needed) evaluation produces:
(test 0 (p))
(if (= 0 0) 0 (p))
; 0

; Appliative order evaluation however produces an infinite regression
(test 0 (p))
; becomes
(test 0 (p))
; since (p) evaluates to (p) infinitely