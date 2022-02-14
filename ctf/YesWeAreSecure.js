/*
 * Challenge URL
 * https://cctv-web.2021.ctfcompetition.com/
 *  */

// From the challenge website
const checkPassword = () => {
  const v = document.getElementById("password").value;
  const p = Array.from(v).map((a) => 0xcafe + a.charCodeAt(0));

  if (
    p[0] === 52037 &&
    p[6] === 52081 &&
    p[5] === 52063 &&
    p[1] === 52077 &&
    p[9] === 52077 &&
    p[10] === 52080 &&
    p[4] === 52046 &&
    p[3] === 52066 &&
    p[8] === 52085 &&
    p[7] === 52081 &&
    p[2] === 52077 &&
    p[11] === 52066
  ) {
    window.location.replace(v + ".html");
  } else {
    alert("Wrong password!");
  }
};

window.addEventListener(
  "DOMContentLoaded",
  () => {
    document.getElementById("go").addEventListener("click", checkPassword);
    document.getElementById("password").addEventListener("keydown", (e) => {
      if (e.keyCode === 13) {
        checkPassword();
      }
    });
  },
  false
);

// Solution:
rawCodes = [
  52037, 52077, 52077, 52066, 52046, 52063, 52081, 52081, 52085, 52077, 52080,
  52066,
];
function solve(array) {
  return array.map(code => String.fromCharCode(code - 0xCafe)).join('')
}
