enum DoorState
{
  Opened,
  Closed,
  Locked,
}

class Door
{
  public DoorState state { get; set; }
  private string _passcode;

  public Door(string passcode) {
    state = DoorState.Closed;
    _passcode = passcode;
  }

  public void NewPasscode(string passcode, string newPasscode) {
    if (passcode == _passcode) {
      _passcode = newPasscode;
    }
  }

  public void Open() {
    if (state == DoorState.Closed) {
      state = DoorState.Opened;
    }
  }

  public void Close() {
    if (state == DoorState.Opened) {
      state = DoorState.Closed;
    }
  }

  public void Lock() {
    if (state == DoorState.Closed) {
      state = DoorState.Locked;
    }
  }

  public void Unlock(string passcode) {
    if (state == DoorState.Locked && passcode == _passcode) {
      state = DoorState.Closed;
    }
  }
}
