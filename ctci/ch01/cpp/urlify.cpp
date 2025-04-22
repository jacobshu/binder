#include <iostream>
#include <sstream>
#include <string>

using namespace std;

string urlify(string str, int length) {
  string result = "";

  int count = 0;
  bool startCount = false;

  for (char c : str) {
    if (c == ' ') {
      if (!startCount)
        continue;
      if (count >= length)
        break;
      count += 1;
      result += "%20";
    } else {
      startCount = true;
      count += 1;
      result += c;
    }
  }

  return result;
}

int main(int argc, char *argv[]) {
  if (argc != 3)
    return 1;

  istringstream ss(argv[2]);
  int length;
  if (!(ss >> length)) {
    cerr << "invalid number: " << argv[2] << endl;
  } else if (!ss.eof()) {
    cerr << "trailing character: " << argv[2] << endl;
  }

  cout << urlify(argv[1], length) << endl;
}
