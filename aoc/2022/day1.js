import { input } from './input.js';

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