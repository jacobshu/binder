import kleur from 'kleur'
import fs from 'node:fs/promises';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

async function solve() {
  let input
  try {
    let inputPath = path.join(path.dirname(fileURLToPath(import.meta.url)), '../data/day05.txt')
    input = await fs.readFile(inputPath, { encoding: 'utf8' });
  } catch (err) {
    console.log(kleur.bold().red(err));
  }

  let [rulesInput, printsInput] = input.split('\n\n')
  console.log(kleur.magenta(rulesInput.split('\n').length))
  console.log(kleur.yellow(printsInput.split('\n').length))

  let rulesTree = {
    value: null,
    left: null,
    right: null,
  }

  function insert(node, tree) { }
  function find(node, tree) { }

}

solve()
