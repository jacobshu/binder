import { isUnique } from './isUnique.js';

const testTable = [
  { fn: isUnique, args: ['abcde'], expected: true, },
  { fn: isUnique, args: ['ada'], expected: false, },
  { fn: isUnique, args: ['definistrate'], expected: false, },
  { fn: isUnique, args: ['with space'], expected: true, },
  { fn: isUnique, args: ['symbol!@#$%^&('], expected: true, },
  { fn: isUnique, args: ['symbol!@###$%^&(&'], expected: false, },
]

function test(table) {
  table.forEach(t => {
    const result = t.fn(...t.args)
    if (result !== t.expected) {
      console.log('FAILED', t.fn, t.args, t.expected)
    } else {
      console.log('PASSED')
    }
  })
}

test(testTable)
