export function isUnique(str) {
  let letters = {}
  for (let i = 0; i < str.length; i++) {
    if (letters[str[i]] !== undefined) {
      return false
    }
    letters[str[i]] = 1
  }
  return true
}

export function isUniqueStr(str) {
  for (let i = 0; i < str.length; i++) {
    // TODO 
  }
}
