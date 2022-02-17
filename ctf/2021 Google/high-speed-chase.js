/* Challenge
 * https://high-speed-chase-web.2021.ctfcompetition.com/
 */

function controlCar(scanArray) {
  let myLane = 8;
  let clearLane = scanArray.indexOf(Math.max(...scanArray)) + 1;

  let turn = myLane === clearLane ? 0 : myLane < clearLane ? 1 : -1;
  console.log(
    `array: ${scanArray}\nmyLane: ${myLane}\nclearLane: ${clearLane}`
  );

  return turn;
}
