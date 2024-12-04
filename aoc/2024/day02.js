import { argv0, stdin as input, stdout as output } from 'node:process';
import * as rl from 'node:readline/promises';
import { fileURLToPath } from 'node:url';
import * as path from 'node:path';
import * as fs from 'node:fs'
import kleur from 'kleur'
import { clearLine, cursorTo, moveCursor } from 'node:readline';

let safeLevels = 0
let withDampener = 0

async function processLineByLine() {
  let inputPath = path.join(path.dirname(fileURLToPath(import.meta.url)), 'day02.txt')
  const fileStream = fs.createReadStream(inputPath);

  const reader = rl.createInterface({
    input: fileStream,
    crlfDelay: Infinity,
  });

  const writer = rl.createInterface({
    input: input,
    crlfDelay: Infinity,
    output: output,
    tabSize: 4,
  });

  let count = 0
  for await (const line of reader) {
    if (count < 5000) {
      processLine(writer, line)
    }
    count += 1
  }

  writer.write(kleur.bold('safe levels: ') + kleur.bold().green(safeLevels) + '\n')
  writer.write(kleur.bold('safe levels withDampener: ') + kleur.bold().green(withDampener) + '\n')
  writer.close()
  reader.close()
}

processLineByLine();

function processLine(writer, line) {
  const parsed = line.split(' ').map(l => parseInt(l))
  const isSafe = isLevelSafe(writer, parsed, 1) ? 1 : 0

  let safeWithDampener = false
  for (let i = 0; i < parsed.length; i++) {
    const filtered = parsed.filter((_, k) => k !== i);
    if (isLevelSafe(writer, filtered)) {
      safeWithDampener = true;
      break;
    }
  }

  if (isSafe) safeLevels += 1
  if (isSafe || safeWithDampener) withDampener += 1
}

function isLevelSafe(w, lvl, dampeners) {
  let isSafe = true
  let dataLine = ''
  let diffLine = ''
  let diffs = []
  for (let i = 0; i < lvl.length; i++) {
    let curr = lvl[i]
    let next = lvl[i + 1]
    let diff = next - curr
    let isPositive = curr < next
    let isNoChange = next === curr
    if (!Number.isNaN(diff)) diffs.push(diff)
    dataLine += isSafe ? kleur.bold(curr < 10 ? ' ' + curr : curr) + '   ' : kleur.yellow(curr < 10 ? ' ' + curr : curr) + '   '
    if (Number.isNaN(diff)) {
      diffLine += kleur.gray(curr < 10 ? ' •' : '•')
    } else {
      diffLine += isPositive || isNoChange ? kleur.green(`+${diff}`) : kleur.red(`${diff}`)
    }
    diffLine += '   '
  }

  if (!isSafe && dampeners > 0) {
    dampeners -= 1
    isSafe = isLevelSafe(w, lvl.filter((_, k) => k !== i), dampeners)
  }

  const inc = diffs.every(d => d >= 1 && d <= 3)
  const dec = diffs.every(d => d <= -1 && d >= -3)
  isSafe = inc || dec

  w.write(isSafe ? kleur.bold().green(' ✓') : kleur.bold().red(' ✘'))
  w.write('   ' + dataLine + '\n')
  w.write('     ' + diffLine + '\n')
  w.write('-'.repeat(45) + '\n')
  return isSafe
}

function clearLinesUp(w, num) {
  for (let i = 0; i < num; i++) {
    moveCursor(w, 0, -1)
    clearLine(w, 0)
  }
}

