import * as rl from 'node:readline/promises';
import { fileURLToPath } from 'node:url';
import * as path from 'node:path';
import * as fs from 'node:fs'
import kleur from 'kleur'

async function processLineByLine() {
  let inputPath = path.join(path.dirname(fileURLToPath(import.meta.url)), 'day03.txt')
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
    ranges.forEach(r => {
      withMeta += scan(line.slice(r[0], r[1]))
    })
  }

  console.log(kleur.bold().magenta('mul: ' + count))
  console.log(kleur.bold().magenta('mul with commands: ' + withMeta))
  // 75466465 too high
  reader.close()
}

processLineByLine();

function metaCommand(txt) {
  const doRegex = /do\(\)/g
  const dontRegex = /don't\(\)/g
  const dos = txt.matchAll(doRegex)
  const donts = txt.matchAll(dontRegex)
  const commands = []
  for (const match of dos) {
    commands.push({ index: match.index, do: true })
  }
  for (const m of donts) {
    commands.push({ index: m.index, do: false })
  }

  commands.sort((a, b) => a.index - b.index)
  let enabled = true
  const filtered = commands.filter(c => {
    let keep = c.do !== enabled
    enabled = c.do
    return keep
  })

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

function scan(txt) {
  const mulregex = /mul\((\d+,\d+)\)/g
  const groups = [...txt.matchAll(mulregex)]
  const ints = groups.map(g => (g[1]).split(',').map(s => parseInt(s)))
  const products = ints.map(tuple => tuple[0] * tuple[1])
  return products.reduce((acc, curr) => acc += curr)
}
