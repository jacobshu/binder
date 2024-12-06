import * as rl from 'node:readline/promises';
import { fileURLToPath } from 'node:url';
import * as path from 'node:path';
import * as fs from 'node:fs'
import kleur from 'kleur'

async function processLineByLine() {
  let inputPath = path.join(path.dirname(fileURLToPath(import.meta.url)), '../data/day03.txt')
  const fileStream = fs.createReadStream(inputPath);

  const reader = rl.createInterface({
    input: fileStream,
    crlfDelay: Infinity,
  });

  let count = 0
  let withMeta = 0
  let agg = ''
  for await (const line of reader) {
    agg += line
    count += scan(line)
    let ranges = metaCommand(line)
    ranges.forEach(r => {
      let slice = line.slice(r.from, r.to)
      if (r.do === 'do()') {
        withMeta += scan(slice, true)
      }
    })
  }

  console.log(kleur.bold().magenta('mul: ' + count))
  console.log(kleur.bold().magenta('mul with commands: ' + withMeta))

  count = scan(agg)
  withMeta = metaCommand(agg).reduce((acc, curr) => {
    if (curr.do) {
      let s = agg.slice(curr.from, curr.to)
      acc += scan(s, true)
    }
    console.log(curr, acc)
    return acc
  }, 0)
  console.log(kleur.bold().magenta('mul: ' + count))
  console.log(kleur.bold().magenta('mul with commands: ' + withMeta))
  console.log(kleur.bold().red('working: ' + (withMeta === 59097164)))


  // 75466465 too high
  reader.close()
}

processLineByLine();

function metaCommand(txt) {
  const regex = /(do\(\)|don't\(\))/g
  const toggles = txt.matchAll(regex)
  const commands = [{ from: 0, do: true, to: -Infinity }]
  for (const match of toggles) {
    commands.push({ from: match.index, do: match[0] === 'do()', to: -Infinity })
  }

  commands.sort((a, b) => a.from - b.from)
  let lastFrom = Infinity
  for (let i = commands.length - 1; i >= 0; i--) {
    commands[i].to = lastFrom
    lastFrom = commands[i].from
  }
  return commands
}

function scan(txt, debug) {
  const mulregex = /mul\((\d+,\d+)\)/g
  const groups = [...txt.matchAll(mulregex)]
  const logs = []
  const ints = groups.map(g => {
    logs.push([g[1]])
    return g[1].split(',').map(s => parseInt(s))
  })
  const products = ints.map((tuple, i) => {
    const product = tuple[0] * tuple[1]
    logs[i].push(`${tuple[0]} * ${tuple[1]} = ${product}`)
    return product
  })
  if (debug) console.log(logs)
  return products.reduce((acc, curr) => acc += curr)
}
