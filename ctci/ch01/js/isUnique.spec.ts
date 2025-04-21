import { describe, expect, test } from "vitest";
import { isUnique } from "./isUnique";
import { readTestData } from "../../test.helper";

console.log(readTestData)

interface TestData {
  problem: string
  input: any
  output: any
}

describe("chapter 1", () => {
  test('1.1 test if string contains only unique characters', () => {
    expect(isUnique("abcde")).toEqual(true)
    expect(isUnique("ada")).toEqual(false)
    expect(isUnique("definistrate")).toEqual(false)
    expect(isUnique("with space")).toEqual(true)
    expect(isUnique("symbol!@#$%^&(")).toEqual(true)
    expect(isUnique("symbol!@###$%^&(&")).toEqual(false)

  })
})
