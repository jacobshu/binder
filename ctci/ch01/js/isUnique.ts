export function isUnique(str: string) {
  interface Letters {
    [key: string]: number
  }

  let found: Letters = {}
  for (let i = 0; i < str.length; i++) {
    if (found[str[i]] !== undefined) {
      return false
    }
    found[str[i]] = 1
  }
  return true
}

export function isUniqueStr(str: string) {
  for (let i = 0; i < str.length; i++) {
    // TODO 
  }
}
