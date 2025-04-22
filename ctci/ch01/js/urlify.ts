export function urlify(str: string, length: number): string {
  let url = ""
  if (length == 0) return url;

  let startCounting = false;
  let count = 0;

  for (let i = 0; i < str.length; i++) {
    if (str[i] == " ") {
      if (!startCounting) continue;
      if (count >= length) break;
      count += 1;
      url += "%20";
    } else {
      startCounting = true;
      count += 1;
      url += str[i];
    }
  }
  return url;
}

export function urlifyConvenient(str: string, length: number): string {
  return str.trim().replaceAll(" ", "%20");
}
