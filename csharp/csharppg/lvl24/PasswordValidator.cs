class PasswordValidator
{
  public static bool Validate(string password)
  {
    bool isCorrectLength = password.Length >= 6 && password.Length <= 13;
    bool hasUppercase = false;
    bool hasLowercase = false;
    bool hasNumber = false;
    bool hasUppercaseT = false;
    bool hasAmpersand = false;

    foreach (char c in password) {
      if (char.IsUpper(c)) {
        hasUppercase = true;
        if (c == 'T') hasUppercaseT = true;
      }
      if (char.IsLower(c)) hasLowercase = true;
      if (char.IsDigit(c)) hasNumber = true;
      if (c == '&') hasAmpersand = true;
    }

    return isCorrectLength && hasUppercase && hasLowercase && hasNumber && !hasUppercaseT && !hasAmpersand;
  }
}
