#include <iostream>
#include <unordered_map>

int main(int argc, char *argv[]) {
  if (argc < 3)
    return 1;

  if (std::strlen(argv[1]) != std::strlen(argv[2])) {
    std::cout << "false\n";
    return 0;
  }

  std::unordered_map<char, int> char_map;

  for (int i = 0; i < std::strlen(argv[1]); i++) {
    char first_char = argv[1][i];
    char second_char = argv[2][i];

    auto first_search = char_map.find(first_char);
    if (first_search == char_map.end()) {
      char_map[first_char] = 1;
    } else {
      char_map[first_char] = first_search->second + 1;
    }

    auto second_search = char_map.find(second_char);
    if (second_search == char_map.end()) {
      char_map[second_char] = -1;
    } else {
      char_map[second_char] = second_search->second - 1;
    }
  }

  for (const auto &pair : char_map) {
    if (pair.second != 0) {
      std::cout << "false\n";
      return 0;
    }
  }
  std::cout << "true\n";
  return 0;
}
