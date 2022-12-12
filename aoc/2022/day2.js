import {day2} from './input.js';

// rock       a   x
// paper      b   y
// scissors   c   z
const p1 = {
    A: {
        equalTo: 'X',
        losesTo: 'Y',
    },
    B: {
        equalTo: 'Y',
        losesTo: 'Z',
    },
    C: {
        equalTo: 'Z',
        losesTo: 'X',
    },
    X: {
        value: 1,
    },
    Y: {
        value: 2,
    },
    Z: {
        value: 3,
    },
    score: {
        lose: 0,
        draw: 3,
        win: 6
    },
    total: 0
}

const rounds = day2.split(/\n/g);

for (let i = 0; i < rounds.length; i++) {
    let [o, m] = rounds[i].split(' ');
    if (p1[o].losesTo === m) {
        p1.total = p1.total + p1[m].value + p1.score.win
    } else if (p1[o].equalTo === m) {
        p1.total = p1.total + p1[m].value + p1.score.draw
    } else {
        p1.total = p1.total + p1[m].value + p1.score.lose;
    }
}

console.log(p1.total)

const p2 = {
    A: {
      value: 1,
      losesTo: 'B',
      losesToValue: 2,
      winsAgainst: 'C',
      winsAgainstValue: 3
    },
    B: {
      value: 2,
      losesTo: 'C',
      losesToValue: 3,
      winsAgainst: 'A',
      winsAgainstValue: 1
    },
    C: {
      value: 3,
      losesTo: 'A',
      losesToValue: 1,
      winsAgainst: 'B',
      winsAgainstValue: 2
    },
    X: {
      name: 'lose',
    },
    Y: {
      name: 'draw',
    },
    Z: {
      value: 'win',
    },
    score: {
      lose: 0,
      draw: 3,
      win: 6
    },
    total: 0
}

for (let i = 0; i < rounds.length; i++) {
    let [o, e] = rounds[i].split(' ');
    if (p2[e].name === 'win') {
        p2.total = p2.total + p2[o].losesToValue + p2.score.win
    } else if (p2[e].name === 'draw') {
        p2.total = p2.total + p2[o].value + p2.score.draw
    } else {
        p2.total = p2.total + p2[o].winsAgainstValue + p2.score.lose;
    }
}

console.log(p2.total)
