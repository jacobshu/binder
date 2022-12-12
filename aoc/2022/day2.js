import { input } from './input.js';

let elves = input.split(/\n\n/g);
let topThree = [0,0,0];

for (let i = 0; i < elves.length; i++) {
  const value = elves[i].split(/\n/g).reduce((acc, x) => acc + parseInt(x), 0);
  topThree.sort((a,b) => a - b)
  const indexToChange = topThree.findIndex((item) => item < value);
  console.log(`value: ${value}, index: ${indexToChange}, curret top: ${topThree}`)
  if (indexToChange >= 0) {
      topThree[indexToChange] = value
  }
}

console.log(topThree[0] + topThree[1] + topThree[2])