import { isUnique } from './isUnique.js';
import { readTestData } from '../../test.helper.js';

const testTable = readTestData("../test.json");

function test(table) {
  const fns = {
    "is-unique": isUnique,
  }

  table.forEach(t => {
    const result = fns[t.fn](...t.args)
    if (result !== t.expected) {
      console.log('FAILED', fns[t.fn], t.args, t.expected)
    } else {
      console.log('PASSED')
    }
  })
}

test(testTable)
