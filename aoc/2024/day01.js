const split = input.split('\n')
const l1 = []
const l2 = []
split.forEach(r => {
  const v = r.split(' ')
  l1.push(parseInt(v[0]))
  l2.push(parseInt(v[3]))
})

l1.sort()
l2.sort()

let diff = 0
for (let i = 0; i < l1.length; i++) {
  let ndiff = Math.abs(l1[i] - l2[i])
  diff = diff + ndiff
}

console.log('total difference: ', diff)

let smap = {}
l1.forEach((n, i) => {
  let asStr = n.toString()
  if (!smap[asStr]) {
    smap[n] = 0
  }
})

l2.forEach(num => {
  let str = num.toString()
  if (smap[str] !== undefined) {
    smap[str] = smap[str] + 1
  }
})

let score = 0
for (let num in smap) {
  score += parseInt(num) * smap[num]
}

console.log('similarity score: ', score)
