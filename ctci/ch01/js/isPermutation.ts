export function isPermutation(first: string, second: string): boolean {
  if (first.length != second.length) return false;

  const map: Map<string, number> = new Map();
  for (let i = 0; i < first.length; i++) {
    const firstChar = first[i];
    const secondChar = second[i];

    let firstFound = map.get(firstChar);
    if (firstFound !== undefined) {
      map.set(firstChar, firstFound += 1);
    } else {
      map.set(firstChar, 1);
    }

    let secondFound = map.get(secondChar);
    if (secondFound !== undefined) {
      map.set(secondChar, secondFound -= 1);
    } else {
      map.set(secondChar, -1);
    }
  }

  let isPermutation: boolean = true;
  map.forEach((count) => {
    if (count !== 0) isPermutation = false;
  })

  return isPermutation;
}
