import * as rl from 'node:readline/promises';
import { fileURLToPath } from 'node:url';
import * as path from 'node:path';
import * as fs from 'node:fs'
import kleur from 'kleur'

async function processLineByLine() {
  let inputPath = path.join(path.dirname(fileURLToPath(import.meta.url)), '../day03.txt')
  const fileStream = fs.createReadStream(inputPath);

  const reader = rl.createInterface({
    input: fileStream,
    crlfDelay: Infinity,
  });

  let count = 0
  let withMeta = 0
  for await (const line of reader) {
    count += scan(line)
    let ranges = metaCommand(line)
    let lastIndex = 0
    ranges.forEach(r => {
      let slice = line.slice(lastIndex, r.index)
      const matching = r.do === 'do()' ? slice.slice(0, 4) : slice.slice(0, 7)
      console.log(r.do === matching ? kleur.bold().green(r.do) : kleur.bold().red(r.do), 'from: ', lastIndex, ' to ', r.index, '\n', slice.slice(0, 6), '\n')
      if (r.do) {
        withMeta += scan(slice, false)
      }
      lastIndex = r.index
    })
  }

  console.log(kleur.bold().magenta('mul: ' + count))
  console.log(kleur.bold().magenta('mul with commands: ' + withMeta))
  // 75466465 too high
  reader.close()
}

processLineByLine();

function metaCommand(txt) {
  const regex = /(do\(\)|don't\(\))/g
  const toggles = txt.matchAll(regex)
  const commands = []
  for (const match of toggles) {
    commands.push({ index: match.index, do: match[0] })
  }
  // for (const match of dos) {
  //   console.log(match)
  //   commands.push({ index: match.index, do: true })
  // }
  // for (const m of donts) {
  //   commands.push({ index: m.index, do: false })
  // }

  return commands.sort((a, b) => a.index - b.index)
  let enabled = true
  const filtered = commands.filter(c => {
    let keep = c.do !== enabled
    enabled = c.do
    return keep
  })

  return filtered
  const enabledRanges = []
  for (let i = 0; i < filtered.length; i += 2) {
    if (i === 0) {
      enabledRanges.push([0, filtered[i].index])
    } else if (i === filtered.length - 2) {
      enabledRanges.push([filtered[i - 1].index, filtered[i].index])
      enabledRanges.push([filtered[i + 1].index, Infinity])
    } else {
      enabledRanges.push([filtered[i - 1].index, filtered[i].index])
    }
  }

  return enabledRanges
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

