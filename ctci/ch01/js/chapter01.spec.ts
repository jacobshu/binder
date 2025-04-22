import { describe, expect, test } from "vitest";
import { isUnique } from "./isUnique";
import { isPermutation } from "./isPermutation";

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
})
