import kleur from 'kleur';
import fs from 'node:fs/promises';
import path from 'node:path';
import { fileURLToPath } from 'node:url';


async function solve() {
  let input
  try {
    let inputPath = path.join(path.dirname(fileURLToPath(import.meta.url)), '../data/day04.txt')
    input = await fs.readFile(inputPath, { encoding: 'utf8' });
  } catch (err) {
    console.log(kleur.bold().red(err));
  }

  const LINE_LENGTH = 141
  // const LINE_LENGTH = 11
  // let test = `XX---SAMX\n-M----S-S\n-AA--A--A\n-S-SM---M\n---X----X\nXMAS-X---\n--S-M----\n---A-----\n--S-M----\n-----X---`

  let count = 0
  const dirs = {
    u: LINE_LENGTH * -1,
    d: LINE_LENGTH,
    b: -1,
    f: 1,
    dfu: (LINE_LENGTH * -1) + 1,
    dbu: (LINE_LENGTH * -1) - 1,
    ddf: LINE_LENGTH + 1,
    ddb: LINE_LENGTH - 1,
  }

  let xcount = 0
  const cross = {
    ur: (LINE_LENGTH * -1) + 1,
    ul: (LINE_LENGTH * -1) - 1,
    lr: LINE_LENGTH + 1,
    ll: LINE_LENGTH - 1,
  }


  function valid(index, dir, len, debug) {
    if (!dir) throw new Error('provide the direction dumb-dumb')
    const str = 'XMAS'
    return [...str].every((c, d) => {
      const nc = index + (d * dirs[dir])
      if (debug) console.log(
        kleur.magenta(`at index ${index}`),
        kleur.bold().yellow('matching'),
        kleur.green('character ' + input[nc] + ' at index ' + nc),
        kleur.cyan(' to expected ' + c),
      )
      return 0 <= nc && nc < len && input[nc] === c
    })
  }

  function xvalid(index, debug) {
    let diag = [
      [input[index + cross.ll], input[index + cross.ur]],
      [input[index + cross.ul], input[index + cross.lr]],
    ]
    if (diag.some(d => d[0] === undefined || d[1] === undefined || d[0] === '\n' || d[1] === '\n')) return false

    let valid = diag.every(d => d[0] !== 'X' &&
      d[1] !== 'X' &&
      d[0] !== 'A' &&
      d[1] !== 'A' &&
      d[0] !== d[1])
    if (debug) console.log(
      kleur.yellow(diag[0][0] + '-' + diag[0][1] + ' '),
      kleur.cyan(diag[1][0] + '-' + diag[1][1] + ' '),
      ' is ' + (valid ? kleur.bold().green('VALID') : kleur.bold().red('NOT VALID')),
    )
    return valid
  }


  console.time('partOne')
  for (let i = 0; i < input.length; i++) {
    if (input[i] === 'X') {
      for (const dir in dirs) {
        count += valid(i, dir, input.length) ? 1 : 0
      }
    }
  }
  console.log(kleur.bold().green('XMASs: ' + count), kleur.bold().yellow('in '))
  console.timeEnd('partOne')

  console.time('partTwo')
  for (let i = 0; i < input.length; i++) {
    if (input[i] === 'A') {
      xcount += xvalid(i, false) ? 1 : 0
    }
  }

  console.log(kleur.bold().green('MASs in X: ' + xcount), kleur.bold().yellow('in '))
  console.timeEnd('partTwo')
}

solve()
