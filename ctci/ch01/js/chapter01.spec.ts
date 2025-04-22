import { describe, expect, test } from "vitest";
import { isUnique } from "./isUnique";
import { isPermutation } from "./isPermutation";
import { urlify, urlifyConvenient } from "./urlify";

describe("chapter 1", () => {
  test('1.1 test if string contains only unique characters', () => {
    expect(isUnique("abcde")).toEqual(true)
    expect(isUnique("with space")).toEqual(true)
    expect(isUnique("symbol!@#$%^&(")).toEqual(true)
    expect(isUnique("ada")).toEqual(false)
    expect(isUnique("definistrate")).toEqual(false)
    expect(isUnique("symbol!@###$%^&(&")).toEqual(false)
  })

  test('1.2 test if string is permutation of another string', () => {
    expect(isPermutation("abc", "cba")).toEqual(true)
    expect(isPermutation("aaa ", "a aa")).toEqual(true)
    expect(isPermutation("##fe", "#ef#")).toEqual(true)
    expect(isPermutation("definistrate", "")).toEqual(false)
    expect(isPermutation("symbol", "symnol")).toEqual(false)
    expect(isPermutation("23", "14")).toEqual(false)
  })

  test('1.3 test replace spaces with %20 in string', () => {
    expect(urlify("abc ", 3)).toEqual("abc")
    expect(urlify("ab c ", 3)).toEqual("ab%20c")
    expect(urlify(" qp98 ru1-  5713l", 16)).toEqual("qp98%20ru1-%20%205713l")
    expect(urlify("", 0)).toEqual("")
    expect(urlify(" 2 3 ", 3)).toEqual("2%203")
  })

  test('1.3 test replace spaces with %20 in string conveniently', () => {
    expect(urlifyConvenient("abc ", 3)).toEqual("abc")
    expect(urlifyConvenient("ab c ", 3)).toEqual("ab%20c")
    expect(urlifyConvenient(" qp98 ru1-  5713l", 16)).toEqual("qp98%20ru1-%20%205713l")
    expect(urlifyConvenient("", 0)).toEqual("")
    expect(urlifyConvenient(" 2 3 ", 3)).toEqual("2%203")
  })

})
