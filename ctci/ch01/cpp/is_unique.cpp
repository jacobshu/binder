#include <iostream>
#include <unordered_map>
#include <utility>

int main(int argc, char *argv[]) {
  std::unordered_map<char, int> found;

  if (argc < 2)
    return 1;

  for (int i = 0; i < std::strlen(argv[1]); i++) {
    if (auto search = found.find(argv[1][i]); search != found.end()) {
      std::cout << "false\n";
      return 0;
    } else {
      found.insert(std::make_pair(argv[1][i], 1));
    }
  }
  std::cout << "true\n";
  return 0;
}
