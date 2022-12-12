import { day1 } from './input.js';

let elves = input.split(/\n\n/g);
let mostCarried = {
  amount: 0,
  position: 0
};

for (let i = 0; i < elves.length; i++) {
    const value = elves[i].split(/\n/g).reduce((acc, x) => acc + parseInt(x), 0);
    const shouldChange = value > mostCarried.amount;
    console.log(`shouldChange: ${shouldChange}, value: ${value}, current: ${mostCarried.amount} @ ${mostCarried.position}`)
    mostCarried.amount = shouldChange ? value : mostCarried.amount;
    mostCarried.position = shouldChange ? i : mostCarried.position;
}

console.log(`amount: ${mostCarried.amount}, position: ${mostCarried.position}`)

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